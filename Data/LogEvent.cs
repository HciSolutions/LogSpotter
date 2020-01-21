using System;

namespace Triamun.Log4NetViewer.Data
{
    /// <summary>
    /// Contains the data of a log event.
    /// </summary>
    public class LogEvent
    {
        #region Private Members

        private int _eventNumber;
        private string _logger;
        private DateTime _timeStamp;
        private LogLevels _level;
        private string _thread;
        private string _domain;
        private string _userName;
        private string _message;
        private string _exception;
        private string _className;
        private string _methodName;
        private string _fileName;
        private int _lineNumber;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEvent"/> class.
        /// </summary>
        public LogEvent()
        {
            _className = null;
            _domain = null;
            _fileName = null;
            _level = LogLevels.None;
            _lineNumber = 0;
            _logger = null;
            _message = null;
            _methodName = null;
            _thread = null;
            _timeStamp = DateTime.Now;
            _userName = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEvent"/> class.
        /// </summary>
        /// <param name="eventNumber">The event number.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="level">The level.</param>
        /// <param name="thread">The thread.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="lineNumber">The line number.</param>
        public LogEvent(int eventNumber, string logger, DateTime timeStamp, LogLevels level, string thread, string domain, string userName, string message, string exception, string className, string methodName, string fileName, int lineNumber)
        {
            _eventNumber = eventNumber;
            _className = className ?? String.Empty;
            _domain = domain ?? String.Empty;
            _fileName = fileName ?? String.Empty;
            _level = level;
            _lineNumber = lineNumber;
            _logger = logger ?? String.Empty;
            _message = message ?? String.Empty;
            _exception = exception ?? String.Empty;
            _methodName = methodName ?? String.Empty;
            _thread = thread ?? String.Empty;
            _timeStamp = timeStamp;
            _userName = userName ?? String.Empty;
        }

        #endregion Constructor

        #region Public Properties

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <value>The name of the class.</value>
        public string ClassName
        {
            get { return _className; }
        }

        /// <summary>
        /// Gets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain
        {
            get { return _domain; }
        }

        /// <summary>
        /// Gets the event number.
        /// </summary>
        /// <value>The event number.</value>
        public int EventNumber
        {
            get { return _eventNumber; }
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public string Exception
        {
            get { return _exception; }
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get { return _fileName; }
        }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>The level.</value>
        public LogLevels Level
        {
            get { return _level; }
        }

        /// <summary>
        /// Gets the line number.
        /// </summary>
        /// <value>The line number.</value>
        public int LineNumber
        {
            get { return _lineNumber; }
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public string Logger
        {
            get { return _logger; }
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return _message; }
        }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName
        {
            get { return _methodName; }
        }

        /// <summary>
        /// Gets the thread.
        /// </summary>
        /// <value>The thread.</value>
        public string Thread
        {
            get { return _thread; }
        }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get { return _userName; }
        }

        #endregion Public Properties
    }
}
