using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using Triamun.Log4NetViewer.Data.Reader;
using Triamun.Log4NetViewer.Properties;

namespace Triamun.Log4NetViewer.Data.Sources
{
    /// <summary>
    /// Loads logs from an xml file.
    /// </summary>
    [LogDataSourceType("File")]
    public class FileLogDataSource : LogDataSource
    {
        #region Private Constants

        private const int EVENTS_GROUP_DELAY = 100;
        private const int PERIODIC_WATCH_INTERVAL = 1000;

        #endregion

        #region Private Fields

        private readonly Timer _fileSmallerTimer;
        private readonly FileSystemWatcher _fsWatcher;
        private readonly LogReader _logReader;
        private readonly Timer _periodicCheckTimer;
        private readonly Mutex _readLogMutex;
        private long _lastFileLength;
        private string _nextLogFileName;
        private string _previousLogFileName;
        private bool _timerRunning;
        private MemoryStream _tmpData;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogDataSource" /> class.
        /// </summary>
        /// <param name="logFileName">The name of the file to use as log data source.</param>
        public FileLogDataSource(string logFileName)
            : base(logFileName)
        {
            _logReader = new XmlLogReader();
            _lastFileLength = 0;
            _tmpData = new MemoryStream();

            try
            {
                _fsWatcher = new FileSystemWatcher(Path.GetDirectoryName(logFileName), Path.GetFileName(logFileName));
                _fsWatcher.NotifyFilter = NotifyFilters.LastWrite;
                _fsWatcher.Changed += new FileSystemEventHandler(HandleFileChanged);
            }
            catch
            {
                _fsWatcher = null;
            }

            _fileSmallerTimer = new Timer(new TimerCallback(HandleFileSmallerTimerExpired));
            _periodicCheckTimer = new Timer(new TimerCallback(HandlePeriodicCheckTimerExpired));
            _readLogMutex = new Mutex();
            _timerRunning = false;
            BuildPreviousAndNextLogFileName(ConnectionString);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this log source supports the refresh functionality that force checking for new logs.
        /// </summary>
        /// <value>
        /// <c>true</c> if this log source supports the refresh functionality that force checking for new logs; otherwise,
        /// <c>false</c>.
        /// </value>
        public override bool CanRefresh
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the icon to show next to the data source in the UI.
        /// </summary>
        /// <value>The icon to show next to the data source in the UI.</value>
        public override Image Icon
        {
            get { return Resources.LogSourceFile; }
        }

        /// <summary>
        /// Gets the log file history thread.
        /// </summary>
        public List<string> LogFileHistoryThread
        {
            get
            {
                List<string> history = new List<string>();
                // Add current file.
                history.Add(ConnectionString);
                // Get all previous file.
                string fileName = GetSequenceLogFileName(ConnectionString, -1);
                while (fileName != null)
                {
                    history.Insert(0, fileName);
                    fileName = GetSequenceLogFileName(fileName, -1);
                }
                // Get all following file.
                fileName = GetSequenceLogFileName(ConnectionString, 1);
                while (fileName != null)
                {
                    history.Add(fileName);
                    fileName = GetSequenceLogFileName(fileName, 1);
                }
                return history;
            }
        }

        /// <summary>
        /// Gets the name of the next log file.
        /// </summary>
        public string NextLogFileName
        {
            get { return _nextLogFileName; }
        }

        /// <summary>
        /// Gets the name of the previous log file.
        /// </summary>
        public string PreviousLogFileName
        {
            get { return _previousLogFileName; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks that the log data source can be opened without errors.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the data source can be opened without errors; otherwise, <c>false</c>.
        /// </returns>
        public override bool Check()
        {
            if (!File.Exists(ConnectionString))
                return false;

            // Just open/close the file
            try
            {
                using (FileStream input = new FileStream(ConnectionString, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Closes this the data source.
        /// </summary>
        public override void Close()
        {
            try
            {
                _fsWatcher.EnableRaisingEvents = false;
                _periodicCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _lastFileLength = 0;
            }
            finally
            {
                // Offline !
                IsOnline = false;
            }
        }

        /// <summary>
        /// Opens the data source and returns the list of initial log events available.
        /// </summary>
        /// <returns>
        /// The list of <see cref="LogEvent" /> initially available.
        /// </returns>
        public override void Open()
        {
            List<LogEvent> events = null;

            using (FileStream input = new FileStream(ConnectionString, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                long fileLength = input.Length;

                _lastFileLength = input.Length;
                events = _logReader.Read(input, (c) => { OnOpenProgress(new OpenProgressEventArgs(c, (int)Math.Round((double)input.Position / (double)fileLength * 100.0))); });
            }

            // Initialize the log file watching
            _fsWatcher.EnableRaisingEvents = true;

            // Starts the periodic watch timer
            _periodicCheckTimer.Change(PERIODIC_WATCH_INTERVAL, PERIODIC_WATCH_INTERVAL);

            // Online !
            IsOnline = true;

            // Pushes the initial events
            OnNewLog(new NewLogEventArgs(events.ToArray()));
        }

        /// <summary>
        /// Force checking for new logs.
        /// </summary>
        public override void Refresh()
        {
            // Asynchronously refreshes to avoid deadlocks with the UI thread.
            new Action(ProcessLogFileChanges).BeginInvoke(null, null);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="maxLength">The maxium number of characters that may be returned.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </returns>
        /// <remarks>It's up to the implementation to determine how to shrink the returned string representation if required.</remarks>
        public override string ToString(int maxLength)
        {
            if (maxLength > 0 && maxLength < Int32.MaxValue)
                return ShortenPath(ConnectionString, maxLength);

            return ConnectionString;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the previous log data source.
        /// </summary>
        /// <returns></returns>
        private void BuildPreviousAndNextLogFileName(string referenceFileName)
        {
            _nextLogFileName = GetSequenceLogFileName(referenceFileName, 1);
            _previousLogFileName = GetSequenceLogFileName(referenceFileName, -1);
        }

        /// <summary>
        /// Gets the name of the sequence log file.
        /// </summary>
        /// <param name="referenceFileName">Name of the reference file.</param>
        /// <param name="relativePosition">The relative position. Number of rolling file from this file.</param>
        /// <returns>Built file name.</returns>
        private string GetSequenceLogFileName(string referenceFileName, int relativePosition)
        {
            string fileName = Path.GetFileNameWithoutExtension(referenceFileName);
            string rootFileName = Path.GetFileNameWithoutExtension(fileName);
            int logNumber = 0;

            int.TryParse(Path.GetExtension(fileName).Trim('.'), out logNumber);
            string folder = Path.GetDirectoryName(referenceFileName);
            string ext = Path.GetExtension(ConnectionString);
            string relativeFileName;
            if (logNumber + relativePosition == 0)
                relativeFileName = Path.Combine(folder, rootFileName + ext);
            else if (logNumber == 0)
                relativeFileName = Path.Combine(folder, fileName + "." + (logNumber + relativePosition) + ext);
            else
                relativeFileName = Path.Combine(folder, rootFileName + "." + (logNumber + relativePosition) + ext);
            if (File.Exists(relativeFileName))
                return relativeFileName;

            return null;
        }

        /// <summary>
        /// Handles a change in the log file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs" /> instance containing the event data.</param>
        private void HandleFileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (!_timerRunning)
                {
                    // Starts the timer only if the size of the file has decreased
                    if (new FileInfo(ConnectionString).Length < _lastFileLength)
                    {
                        _timerRunning = true;
                        _fileSmallerTimer.Change(EVENTS_GROUP_DELAY, Timeout.Infinite);
                    }
                    else
                        ProcessLogFileChanges();
                }
            }
            catch (Exception ex)
            {
                // Sets the error
                Error = ex.Message;
            }
        }

        /// <summary>
        /// Handles the end of the timer fired if the file has gone smaller.
        /// </summary>
        /// <param name="data">The unused data argument.</param>
        private void HandleFileSmallerTimerExpired(object data)
        {
            try
            {
                ProcessLogFileChanges();
            }
            catch
            {
            }
            finally { _timerRunning = false; }
        }

        /// <summary>
        /// Handles the end of the timer fired if the file has gone smaller.
        /// </summary>
        /// <param name="data">The unused data argument.</param>
        private void HandlePeriodicCheckTimerExpired(object data)
        {
            try
            {
                ProcessLogFileChanges();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Processes the changes that occured in the monitored log file.
        /// </summary>
        private void ProcessLogFileChanges()
        {
            _readLogMutex.WaitOne();
            try
            {
                using (FileStream input = new FileStream(ConnectionString, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                {
                    if (input.Length < _lastFileLength)
                    {
                        _lastFileLength = 0;
                        OnReset(new ResetEventArgs(_logReader.Read(input).ToArray()));
                    }
                    else if (input.Length > _lastFileLength)
                    {
                        input.Seek(_lastFileLength, SeekOrigin.Begin);
                        _lastFileLength = input.Length;
                        OnNewLog(new NewLogEventArgs(_logReader.Read(input).ToArray()));
                    }
                }
                // Resets the error
                Error = null;
            }
            catch (Exception ex)
            {
                // Sets the error
                Error = ex.Message;
            }
            finally { _readLogMutex.ReleaseMutex(); }
        }

        /// <summary>
        /// Shortens the specified <paramref name="path" />.
        /// </summary>
        /// <param name="path">The path to shorten.</param>
        /// <param name="maxLength">Maximum number of characters allowed in the path.</param>
        /// <returns>
        /// The specified <paramref name="path" /> eventually shortened with the removed content replaced by an ellipsis.
        /// The root and the file name are always kept event if the resulting string is longer than <paramref name="maxLength" />.
        /// </returns>
        private string ShortenPath(string path, int maxLength)
        {
            StringBuilder leftPath = new StringBuilder();
            StringBuilder rightPath = new StringBuilder();
            List<string> levels = null;
            bool takeLeft = false;

            // Nothing to do !
            if (String.IsNullOrEmpty(path) || path.Length < maxLength)
                return path;

            // Split every level of the path
            levels = new List<string>(path.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries));

            // We cannot split if we have less than three levels
            if (levels.Count < 3)
                return path;

            // Always keep the beginning
            if (path[0] == Path.DirectorySeparatorChar)
            {
                // No disk. Either UNC or disk relative path
                leftPath.Append(Path.DirectorySeparatorChar);
                if (path.Length > 1 && path[1] == Path.DirectorySeparatorChar)
                {
                    leftPath.Append(Path.DirectorySeparatorChar);
                    leftPath.Append(levels[0]);
                    levels.RemoveAt(0);
                }
                leftPath.Append(Path.DirectorySeparatorChar);
            }
            else
            {
                // Disk information, also take the first directory
                leftPath.Append(levels[0]);
                leftPath.Append(Path.DirectorySeparatorChar);
                leftPath.Append(levels[1]);
                leftPath.Append(Path.DirectorySeparatorChar);
                levels.RemoveRange(0, 2);
            }

            // Always keeps the end
            rightPath.Append(Path.DirectorySeparatorChar);
            rightPath.Append(levels[levels.Count - 1]);
            levels.RemoveAt(levels.Count - 1);

            // Process what's kept
            while (levels.Count > 0)
            {
                int currentIndex = 0;

                if (takeLeft)
                    currentIndex = 0;
                else
                    currentIndex = levels.Count - 1;

                // Stop if the maximum length will be reached
                if (leftPath.Length + rightPath.Length + 3 + levels[currentIndex].Length > maxLength)
                    break;

                if (takeLeft)
                {
                    leftPath.Append(levels[currentIndex]);
                    leftPath.Append(Path.DirectorySeparatorChar);
                }
                else
                {
                    rightPath.Insert(0, levels[currentIndex]);
                    rightPath.Insert(0, Path.DirectorySeparatorChar);
                }

                levels.RemoveAt(currentIndex);
                takeLeft = !takeLeft;
            }

            // Returns the shorened path
            if (levels.Count > 0)
                return leftPath.ToString() + "..." + rightPath.ToString();
            return Path.Combine(leftPath.ToString(), rightPath.ToString().TrimStart(Path.DirectorySeparatorChar));
        }

        #endregion
    }
}
