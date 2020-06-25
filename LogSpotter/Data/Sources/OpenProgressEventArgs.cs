using System;

namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Event arguments for the <see cref="LogDataSource.OpenProgress"/> event.
    /// </summary>
    public class OpenProgressEventArgs : EventArgs
    {
        #region Private Members

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenProgressEventArgs"/> class.
        /// </summary>
        /// <param name="eventCount">The number of events loaded so far.</param>
        public OpenProgressEventArgs(int eventCount)
        {
            EventCount = Math.Max(0, eventCount);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the number of events loaded so far.
        /// </summary>
        /// <value>The number of events loaded so far.</value>
        public int EventCount { get; }

        #endregion
    }
}
