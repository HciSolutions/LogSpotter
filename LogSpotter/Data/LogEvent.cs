using System;

namespace HciSolutions.LogSpotter.Data
{
    /// <summary>
    /// Contains the data of a log event.
    /// </summary>
    public class LogEvent
    {
        
        #region Private Members

        #endregion


        #region Public Properties
        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <value>The name of the class.</value>
        public string ClassName { get; set; } = String.Empty;

        /// <summary>
        /// Gets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain { get; set; } = String.Empty;

        /// <summary>
        /// Gets the event number.
        /// </summary>
        /// <value>The event number.</value>
        public int EventNumber { get; set; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public string Exception { get; set; } = String.Empty;

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; } = String.Empty;

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>The level.</value>
        public LogLevels Level { get; set; } = LogLevels.Debug;

        /// <summary>
        /// Gets the line number.
        /// </summary>
        /// <value>The line number.</value>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public string Logger { get; set; } = String.Empty;

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; } = String.Empty;

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; set; } = String.Empty;

        /// <summary>
        /// Gets the thread.
        /// </summary>
        /// <value>The thread.</value>
        public string Thread { get; set; } = String.Empty;

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; } = String.Empty;

        #endregion
    }
}
