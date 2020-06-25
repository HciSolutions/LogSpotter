using System;

namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Event arguments for the <see cref="ILogDataSource.Reset"/> event.
    /// </summary>
    public class ResetEventArgs : EventArgs
    {
        #region Private Members

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NewLogEventArgs"/> class.
        /// </summary>
        /// <param name="ev">The list of <see cref="LogEvent"/> available after the reset.</param>
        public ResetEventArgs(params LogEvent[] ev)
        {
            Events = ev;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the list of <see cref="LogEvent"/> available after the reset.
        /// </summary>
        /// <value>The list of <see cref="LogEvent"/> available after the reset.</value>
        public LogEvent[] Events { get; }

        #endregion
    }
}
