using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using System.Threading;
using Triamun.Log4NetViewer.Data.Reader;

namespace Triamun.Log4NetViewer.Data.Sources
{
    /// <summary>
    /// Defines the base functionalities of a class that can provide log from a source.
    /// </summary>
    public abstract class LogDataSource : IDisposable
    {
        #region Private Members

        private string _connectionString;
        private bool _holdedReset;
        private List<LogEvent> _holdedEvents;
        private string _name;
        private bool _isOnline;
        private string _error;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LogDataSource"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public LogDataSource(string connectionString)
        {
            _connectionString = connectionString;
            _holdedReset = false;
            _holdedEvents = null;
            _isOnline = false;
            _error = null;

            if (this.GetType().IsDefined(typeof(LogDataSourceTypeAttribute), true))
                _name = ((LogDataSourceTypeAttribute)this.GetType().GetCustomAttributes(typeof(LogDataSourceTypeAttribute), true)[0]).Name;
            else
                _name = this.GetType().Name;
        }

        #endregion Constructor

        #region Public Events

        /// <summary>
        /// Occurs when value of the <see cref="Error"/> property has changed.
        /// </summary>
        public event EventHandler ErrorChanged;

        /// <summary>
        /// Occurs when the value of the <see cref="IsOnline"/> property has changed.
        /// </summary>
        public event EventHandler IsOnlineChanged;

        /// <summary>
        /// Occurs when a new log event is available.
        /// </summary>
        public event NewLogEventHandler NewLog;

        /// <summary>
        /// Occurs when the data source is opened to indicate the progression while loading the initial events.
        /// </summary>
        public event OpenProgressEventHandler OpenProgress;

        /// <summary>
        /// Occurs when the data source is resetted and log events must be reloaded.
        /// </summary>
        public event ResetEventHandler Reset;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this log source supports the refresh functionality that force checking for new logs.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this log source supports the refresh functionality that force checking for new logs; otherwise, <c>false</c>.
        /// </value>
        public abstract bool CanRefresh { get; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public virtual string ConnectionString
        {
            get { return _connectionString; }
        }

        /// <summary>
        /// Gets or sets the error message that indicates an error internal to the data source.
        /// </summary>
        /// <value>The error message if an error occured; <c>null</c> to indicate no error.</value>
        public string Error
        {
            get
            {
                return _error;
            }
            protected set
            {
                if (value != null && value.Length == 0)
                    value = null;
                if (_error != value)
                {
                    _error = value;
                    OnErrorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets the icon to show next to the data source in the UI.
        /// </summary>
        /// <value>The icon to show next to the data source in the UI.</value>
        public abstract Image Icon { get; }

        /// <summary>
        /// Gets a value indicating whether this data source is able to provide log informations.
        /// </summary>
        /// <value><c>true</c> if this instance is opened and can provide events; otherwise, <c>false</c>.</value>
        public virtual bool IsOnline
        {
            get { return _isOnline; }
            protected set
            {
                if (_isOnline != value)
                {
                    _isOnline = value;
                    OnIsOnlineChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or Sets a value indicating whether new events are hold.
        /// </summary>
        /// <value><c>true</c> if new events are hold; otherwise, <c>false</c>.</value>
        public bool IsPaused
        {
            get
            {
                return _holdedEvents != null;
            }
            set
            {
                List<LogEvent> pendingEvents = null;
                bool pendingReset = false;

                lock (this)
                {
                    if (_holdedEvents == null && value)
                        _holdedEvents = new List<LogEvent>();
                    else if (_holdedEvents != null && !value)
                    {
                        pendingEvents = _holdedEvents;
                        pendingReset = _holdedReset;
                        _holdedEvents = null;
                    }
                    _holdedReset = false;

                    if (pendingEvents != null)
                    {
                        if (pendingReset)
                            OnReset(new ResetEventArgs(pendingEvents.ToArray()));
                        else
                            OnNewLog(new NewLogEventArgs(pendingEvents.ToArray()));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the implementation name of the data source.
        /// </summary>
        /// <value>The implementation name of the data source.</value>
        public virtual string Name
        {
            get { return _name; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Closes this the data source.
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Opens the data source and returns the list of initial log events available.
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// Force checking for new logs.
        /// </summary>
        public abstract void Refresh();

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return ToString(Int32.MaxValue);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="maxLength">The maxium number of characters that may be returned.</param>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <remarks>It's up to the implementation to determine how to shrink the returned string representation if required.</remarks>
        public abstract string ToString(int maxLength);

        /// <summary>
        /// Checks that the log data source can be opened without errors.
        /// </summary>
        /// <returns><c>true</c> if the data source can be opened without errors; otherwise, <c>false</c>.</returns>
        public abstract bool Check();

        #endregion Public Methods

        #region Public Static Methods

        /// <summary>
        /// Gets the name of the current log data source implementation.
        /// </summary>
        /// <param name="t">The <see cref="LogDataSource"/> data type for which to return the name.</param>
        /// <returns>
        /// The name of the current log data source implementation.
        /// </returns>
        public static string GetName(Type t)
        {
            if (t.IsDefined(typeof(LogDataSourceTypeAttribute), true))
                return ((LogDataSourceTypeAttribute)t.GetCustomAttributes(typeof(LogDataSourceTypeAttribute), true)[0]).Name;

            return t.Name;
        }

        #endregion Public Static Methods

        #region Protected Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Close();
        }

        /// <summary>
        /// Raises the <see cref="E:ErrorChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnErrorChanged(EventArgs e)
        {
            if (ErrorChanged != null)
                ErrorChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:IsOnlineChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnIsOnlineChanged(EventArgs e)
        {
            if (IsOnlineChanged != null)
                IsOnlineChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:NewLog"/> event.
        /// </summary>
        /// <param name="e">The <see cref="NewLogEvent"/> instance containing the event data.</param>
        protected virtual void OnNewLog(NewLogEventArgs e)
        {
            if (NewLog != null)
            {
                lock (this)
                {
                    if (_holdedEvents != null)
                    {
                        _holdedEvents.AddRange(e.Events);
                        return;
                    }
                }

                NewLog(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:OpenProgress"/> event.
        /// </summary>
        /// <param name="e">The <see cref="OpenProgressEventArgs"/> instance containing the event data.</param>
        protected virtual void OnOpenProgress(OpenProgressEventArgs e)
        {
            if (OpenProgress != null)
                OpenProgress(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:Reset"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ResetEventArgs"/> instance containing the event data.</param>
        protected virtual void OnReset(ResetEventArgs e)
        {
            if (Reset != null)
            {
                lock (this)
                {
                    if (_holdedEvents != null)
                    {
                        _holdedReset = true;
                        _holdedEvents.Clear();
                        _holdedEvents.AddRange(e.Events);
                        return;
                    }
                }

                Reset(this, e);
            }
        }

        #endregion Protected Methods
    }
}
