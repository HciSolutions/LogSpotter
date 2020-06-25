using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using HciSolutions.LogSpotter.Data.Reader;
using HciSolutions.LogSpotter.Properties;

namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Loads logs from an xml file.
    /// </summary>
    [LogDataSourceType("File")]
    public class FileLogDataSource : LogDataSource
    {
        #region Constants
        private const int EVENTS_GROUP_DELAY = 100; // 100 ms
        #endregion

        #region Private Members
        private LogReader _logReader;
        private FileSystemWatcher _fsWatcher;
        private long _lastFileLength;
        private MemoryStream _tmpData;
        private Timer _timer;
        private bool _timerRunning;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogDataSource"/> class.
        /// </summary>
        /// <param name="logFileName">The name of the file to use as log data source.</param>
        public FileLogDataSource(string logFileName)
            : base(logFileName)
        {
            _logReader = new XmlLogReader();
            _lastFileLength = 0;
            _tmpData = new MemoryStream();
            _fsWatcher = new FileSystemWatcher(Path.GetDirectoryName(logFileName), Path.GetFileName(logFileName));
            _fsWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _fsWatcher.Changed += HandleFileChanged;
            _timer = new Timer(HandleTimerExpired);
            _timerRunning = false;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the icon to show next to the data source in the UI.
        /// </summary>
        /// <value>The icon to show next to the data source in the UI.</value>
        public override Image Icon => Resources.LogSourceFile;

        #endregion

        #region Private Methods
        /// <summary>
        /// Handles a change in the log file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        private void HandleFileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                lock (_timer)
                {
                    if (!_timerRunning)
                    {
                        // Starts the timer only if the size of the file has decreased
                        if (new FileInfo(ConnectionString).Length < _lastFileLength)
                        {
                            _timerRunning = true;
                            _timer.Change(EVENTS_GROUP_DELAY, Timeout.Infinite);
                        }
                        else
                            ProcessLogFileChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // Sets the error
                Error = ex.Message;
            }
        }

        /// <summary>
        /// Handles the end of the notification timer.
        /// </summary>
        /// <param name="data">The unused data argument.</param>
        private void HandleTimerExpired(object data)
        {
            try
            {
                lock (_timer)
                {
                    _timerRunning = false;
                    ProcessLogFileChanges();
                }
            }
            catch { }
        }

        /// <summary>
        /// Processes the changes that occured in the monitored log file.
        /// </summary>
        private void ProcessLogFileChanges()
        {
            lock (_fsWatcher)
            {
                try
                {
                    using (FileStream input = new FileStream(ConnectionString, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Closes this the data source.
        /// </summary>
        public override void Close()
        {
            try
            {
                _fsWatcher.EnableRaisingEvents = false;
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
        /// The list of <see cref="LogEvent"/> initially available.
        /// </returns>
        public override LogEvent[] Open()
        {
            List<LogEvent> events = null;

            using (FileStream input = new FileStream(ConnectionString, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                _lastFileLength = input.Length;
                events = _logReader.Read(input, (c) => OnOpenProgress(new OpenProgressEventArgs(c)));
            }

            // Initialize the log file watching
            _fsWatcher.EnableRaisingEvents = true;

            // Online !
            IsOnline = true;

            // Returns the events
            return events.ToArray();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="shortForm">if set to <c>true</c> returns a short representation of the object; otherwise, returns the long representation as <see cref="ToString()"/> does.</param>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString(bool shortForm)
        {
            if (shortForm)
                return Path.GetFileName(ConnectionString);

            return ConnectionString;
        }
        #endregion
    }
}
