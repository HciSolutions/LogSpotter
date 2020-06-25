using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace HciSolutions.LogSpotter.Data
{
    /// <summary>
    /// Represents a log event filter.
    /// </summary>
    public class LogEventFilter
    {
        #region Private Constants
        private const string LOGGER_KEY = "Logger";
        private const string THREAD_KEY = "Thread";
        private const string MINDATE_KEY = "MinDate";
        private const string MAXDATE_KEY = "MaxDate";
        private const string DOMAIN_KEY = "Domain";
        private const string CLASSNAME_KEY = "ClassName";
        private const string FILENAME_KEY = "FileName";
        private const string MESSAGE_KEY = "Message";
        private const string EXCEPTION_KEY = "Exception";
        private const string LOGLEVELS_KEY = "LogLevels";
        #endregion

        #region Private Members
        private Regex _loggerRegex;
        private Regex _threadRegex;
        private DateTime? _minDate;
        private DateTime? _maxDate;
        private Regex _domainRegex;
        private Regex _classNameRegex;
        private Regex _fileNameRegex;
        private Regex _messageRegex;
        private Regex _exceptionRegex;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventFilter"/> class.
        /// </summary>
        private LogEventFilter()
        {
            _classNameRegex = null;
            _domainRegex = null;
            _exceptionRegex = null;
            _fileNameRegex = null;
            _loggerRegex = null;
            LogLevels = LogLevels.Debug | LogLevels.Info | LogLevels.Warn | LogLevels.Error | LogLevels.Fatal;
            _maxDate = null;
            _messageRegex = null;
            _minDate = null;
            _threadRegex = null;
            FilterString = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventFilter"/> class.
        /// </summary>
        /// <param name="loggerRegex">The logger regex.</param>
        /// <param name="threadRegex">The thread regex.</param>
        /// <param name="minDate">The min date.</param>
        /// <param name="maxDate">The max date.</param>
        /// <param name="domainRegex">The domain regex.</param>
        /// <param name="classNameRegex">The class name regex.</param>
        /// <param name="fileNameRegex">The file name regex.</param>
        /// <param name="messageRegex">The message regex.</param>
        /// <param name="exceptionRegex">The exception regex.</param>
        /// <param name="logLevels">The log levels.</param>
        public LogEventFilter(string loggerRegex, string threadRegex, DateTime? minDate, DateTime? maxDate, string domainRegex, string classNameRegex, string fileNameRegex, string messageRegex, string exceptionRegex, LogLevels logLevels)
            : this()
        {
            _classNameRegex = String.IsNullOrEmpty(classNameRegex) ? null : new Regex(classNameRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _domainRegex = String.IsNullOrEmpty(domainRegex) ? null : new Regex(domainRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _exceptionRegex = String.IsNullOrEmpty(exceptionRegex) ? null : new Regex(exceptionRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _fileNameRegex = String.IsNullOrEmpty(fileNameRegex) ? null : new Regex(fileNameRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _messageRegex = String.IsNullOrEmpty(messageRegex) ? null : new Regex(messageRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _loggerRegex = String.IsNullOrEmpty(loggerRegex) ? null : new Regex(loggerRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            LogLevels = logLevels;
            _maxDate = maxDate;
            _minDate = minDate;
            _threadRegex = String.IsNullOrEmpty(threadRegex) ? null : new Regex(threadRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventFilter"/> class.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        public LogEventFilter(string filterExpression)
            : this()
        {
            string line = null;
            string[] parts = null;

            if (!String.IsNullOrEmpty(filterExpression))
            {
                using (StringReader reader = new StringReader(filterExpression))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        parts = line.TrimStart().Split(new char[] { '=' }, 2);

                        if (parts.Length != 2)
                            throw new ArgumentException("Invalid filter expression : '" + line + "'.", "filterExpression");

                        parts[0] = parts[0].Trim().ToLower();

                        if (parts[0] == LOGGER_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            _loggerRegex = new Regex(parts[1], RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        else if (parts[0] == THREAD_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            _threadRegex = new Regex(parts[1], RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        else if (parts[0] == MINDATE_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            _minDate = DateTime.Parse(parts[1]);
                        else if (parts[0] == MAXDATE_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            _maxDate = DateTime.Parse(parts[1]);
                        else if (parts[0] == DOMAIN_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            _domainRegex = new Regex(parts[1], RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        else if (parts[0] == CLASSNAME_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            _classNameRegex = new Regex(parts[1], RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        else if (parts[0] == FILENAME_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            _fileNameRegex = new Regex(parts[1], RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        else if (parts[0] == MESSAGE_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            _messageRegex = new Regex(parts[1], RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        else if (parts[0] == EXCEPTION_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            _exceptionRegex = new Regex(parts[1], RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        else if (parts[0] == LOGLEVELS_KEY.ToLower() && !String.IsNullOrEmpty(parts[1]))
                            LogLevels = (LogLevels)Enum.Parse(typeof(LogLevels), parts[1]);
                    }
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Determines whether the specified event matches the filter.
        /// </summary>
        /// <param name="ev">The <see cref="LogEvent"/> to check.</param>
        /// <returns>
        /// 	<c>true</c> if the specified event matches the filter; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMatch(LogEvent ev)
        {
            if (_loggerRegex != null && !_loggerRegex.IsMatch(ev.Logger))
                return false;
            if (_threadRegex != null && !_threadRegex.IsMatch(ev.Thread))
                return false;
            if (_minDate != null && ev.TimeStamp < _minDate.Value)
                return false;
            if (_maxDate != null && ev.TimeStamp > _maxDate.Value)
                return false;
            if (_domainRegex != null && !_domainRegex.IsMatch(ev.Domain))
                return false;
            if (_classNameRegex != null && !_classNameRegex.IsMatch(ev.ClassName))
                return false;
            if (_fileNameRegex != null && !_fileNameRegex.IsMatch(ev.FileName))
                return false;
            if (_messageRegex != null && !_messageRegex.IsMatch(ev.Message))
                return false;
            if (_exceptionRegex != null && !_exceptionRegex.IsMatch(ev.Exception))
                return false;
            if ((ev.Level & LogLevels) != ev.Level)
                return false;

            return true;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (FilterString == null)
            {
                StringBuilder sb = new StringBuilder();

                if (_loggerRegex != null)
                    sb.AppendLine(String.Format("{0}={1}", LOGGER_KEY, _loggerRegex));
                if (_threadRegex != null)
                    sb.AppendLine(String.Format("{0}={1}", THREAD_KEY, _threadRegex));
                if (_minDate != null)
                    sb.AppendLine(String.Format("{0}={1}", MINDATE_KEY, _minDate));
                if (_maxDate != null)
                    sb.AppendLine(String.Format("{0}={1}", MAXDATE_KEY, _maxDate));
                if (_domainRegex != null)
                    sb.AppendLine(String.Format("{0}={1}", DOMAIN_KEY, _domainRegex));
                if (_classNameRegex != null)
                    sb.AppendLine(String.Format("{0}={1}", CLASSNAME_KEY, _classNameRegex));
                if (_fileNameRegex != null)
                    sb.AppendLine(String.Format("{0}={1}", FILENAME_KEY, _fileNameRegex));
                if (_messageRegex != null)
                    sb.AppendLine(String.Format("{0}={1}", MESSAGE_KEY, _messageRegex));
                if (_exceptionRegex != null)
                    sb.AppendLine(String.Format("{0}={1}", EXCEPTION_KEY, _exceptionRegex));
                if (LogLevels != (LogLevels.Debug | LogLevels.Info | LogLevels.Warn | LogLevels.Error | LogLevels.Fatal))
                    sb.AppendLine(String.Format("{0}={1}", LOGLEVELS_KEY, LogLevels));

                FilterString = sb.ToString();
            }


            return FilterString;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the class name regex.
        /// </summary>
        /// <value>The class name regex.</value>
        public string ClassNameRegex
        {
            get => _classNameRegex == null ? String.Empty : _classNameRegex.ToString();
            set => _classNameRegex = String.IsNullOrEmpty(value) ? null : new Regex(value, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Gets or sets the domain regex.
        /// </summary>
        /// <value>The domain regex.</value>
        public string DomainRegex
        {
            get => _domainRegex == null ? String.Empty : _domainRegex.ToString();
            set => _domainRegex = String.IsNullOrEmpty(value) ? null : new Regex(value, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Gets or sets the exception regex.
        /// </summary>
        /// <value>The exception regex.</value>
        public string ExceptionRegex
        {
            get => _exceptionRegex == null ? String.Empty : _exceptionRegex.ToString();
            set => _exceptionRegex = String.IsNullOrEmpty(value) ? null : new Regex(value, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Gets or sets the file name regex.
        /// </summary>
        /// <value>The file name regex.</value>
        public string FileNameRegex
        {
            get => _fileNameRegex == null ? String.Empty : _fileNameRegex.ToString();
            set => _fileNameRegex = String.IsNullOrEmpty(value) ? null : new Regex(value, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Gets or sets the filter string.
        /// </summary>
        /// <value>The filter string.</value>
        public string FilterString { get; set; }

        /// <summary>
        /// Gets or sets the logger regex.
        /// </summary>
        /// <value>The logger regex.</value>
        public string LoggerRegex
        {
            get => _loggerRegex == null ? String.Empty : _loggerRegex.ToString();
            set => _loggerRegex = String.IsNullOrEmpty(value) ? null : new Regex(value, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Gets or sets the log levels.
        /// </summary>
        /// <value>The log levels.</value>
        public LogLevels LogLevels { get; set; }

        /// <summary>
        /// Gets or sets the max date.
        /// </summary>
        /// <value>The max date.</value>
        public DateTime? MaxDate
        {
            get => _maxDate;
            set => _maxDate = value;
        }

        /// <summary>
        /// Gets or sets the message regex.
        /// </summary>
        /// <value>The message regex.</value>
        public string MessageRegex
        {
            get => _messageRegex == null ? String.Empty : _messageRegex.ToString();
            set => _messageRegex = String.IsNullOrEmpty(value) ? null : new Regex(value, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Gets or sets the min date.
        /// </summary>
        /// <value>The min date.</value>
        public DateTime? MinDate
        {
            get => _minDate;
            set => _minDate = value;
        }

        /// <summary>
        /// Gets or sets the thread regex.
        /// </summary>
        /// <value>The thread regex.</value>
        public string ThreadRegex
        {
            get => _threadRegex == null ? String.Empty : _threadRegex.ToString();
            set => _threadRegex = String.IsNullOrEmpty(value) ? null : new Regex(value, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        #endregion
    }
}
