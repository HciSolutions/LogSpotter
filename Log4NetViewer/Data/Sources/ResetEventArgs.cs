using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triamun.Log4NetViewer.Data.Sources
{
    /// <summary>
    /// Event arguments for the <see cref="ILogDataSource.Reset"/> event.
    /// </summary>
    public class ResetEventArgs : EventArgs
    {
        #region Private Members
        private LogEvent[] _events;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NewLogEventArgs"/> class.
        /// </summary>
        /// <param name="ev">The list of <see cref="LogEvent"/> available after the reset.</param>
        public ResetEventArgs(params LogEvent[] ev)
        {
            _events = ev;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the list of <see cref="LogEvent"/> available after the reset.
        /// </summary>
        /// <value>The list of <see cref="LogEvent"/> available after the reset.</value>
        public LogEvent[] Events
        {
            get { return _events; }
        }
        #endregion
    }
}
