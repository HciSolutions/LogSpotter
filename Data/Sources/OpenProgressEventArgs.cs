using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triamun.Log4NetViewer.Data.Sources
{
    /// <summary>
    /// Event arguments for the <see cref="LogDataSource.OpenProgress"/> event.
    /// </summary>
    public class OpenProgressEventArgs : EventArgs
    {
		#region Private Members 

        private int _eventCount;
        private int? _percentDone;

		#endregion Private Members 

		#region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenProgressEventArgs"/> class.
        /// </summary>
        /// <param name="eventCount">The number of events loaded so far.</param>
        /// <param name="percentDone">The percentage done; <c>null</c> if the value is unknown.</param>
        public OpenProgressEventArgs(int eventCount, int? percentDone)
        {
            _eventCount = Math.Max(0, eventCount);

            if (percentDone.HasValue)
                _percentDone = Math.Min(100, Math.Max(0, percentDone.Value));
            else
                _percentDone = null;
        }

		#endregion Constructor 

		#region Public Properties 

        /// <summary>
        /// Gets the number of events loaded so far.
        /// </summary>
        /// <value>The number of events loaded so far.</value>
        public int EventCount
        {
            get { return _eventCount; }
        }

        /// <summary>
        /// Gets the percent done.
        /// </summary>
        /// <value>The percentage done; <c>null</c> if the value is not available.</value>
        public int? PercentDone
        {
            get { return _percentDone; }
        }

		#endregion Public Properties 
    }
}
