using System;

namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Event arguments for the <see cref="ILogDataSource.NewLog"/> event.
    /// </summary>
    public class NewLogEventArgs : EventArgs
    {
        #region Private Members
        private LogEvent[] _events;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NewLogEventArgs"/> class.
        /// </summary>
        /// <param name="ev">The list of new <see cref="LogEvent"/>.</param>
        public NewLogEventArgs(params LogEvent[] ev)
        {
            _events = ev;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>The list of new <see cref="LogEvent"/>.</value>
        public LogEvent[] Events => _events;

        #endregion
    }
}
