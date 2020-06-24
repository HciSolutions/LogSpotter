using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Defines the base functionalities of a class that can provide log from a source.
    /// </summary>
    public abstract class LogDataSource : IDisposable
    {
        #region Private Members
        private string _connectionString;
        private bool _holdedReset;
        private List<LogEvent> _holdedEvents;
        private ISynchronizeInvoke _eventSyncObject;
        private string _name;
        private bool _isOnline;
        private string _error;
        #endregion

        #region Constructor
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
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public virtual string ConnectionString => _connectionString;

        /// <summary>
        /// Gets a value indicating whether this data source is able to provide log informations.
        /// </summary>
        /// <value><c>true</c> if this instance is opened and can provide events; otherwise, <c>false</c>.</value>
        public virtual bool IsOnline
        {
            get => _isOnline;
            protected set
            {
                if (_isOnline != value)
                {
                    _isOnline = value;
                    OnIsOnlineChanged(EventArgs.Empty);
                }
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the error message that indicates an error internal to the data source.
        /// </summary>
        /// <value>The error message if an error occured; <c>null</c> to indicate no error.</value>
        public string Error
        {
            get => _error;
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
        /// Gets or sets the the objecte to use to synchronize events raised by this class.
        /// </summary>
        /// <value>A <see cref="ISynchronizeInvoke"/> instance to use to synchronize events raised by this class; <c>null</c> if no synchronization is required.</value>
        public ISynchronizeInvoke EventSyncObject
        {
            get => _eventSyncObject;
            set => _eventSyncObject = value;
        }

        /// <summary>
        /// Gets or Sets a value indicating whether new events are hold.
        /// </summary>
        /// <value><c>true</c> if new events are hold; otherwise, <c>false</c>.</value>
        public bool IsPaused
        {
            get => _holdedEvents != null;
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
        public virtual string Name => _name;

        /// <summary>
        /// Gets the icon to show next to the data source in the UI.
        /// </summary>
        /// <value>The icon to show next to the data source in the UI.</value>
        public abstract Image Icon { get; }
        #endregion

        #region Public Events
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
        #endregion

        #region Public Methods
        /// <summary>
        /// Closes this the data source.
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Opens the data source and returns the list of initial log events available.
        /// </summary>
        /// <returns>The list of <see cref="LogEvent"/> initially available.</returns>
        public abstract LogEvent[] Open();

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="shortForm">if set to <c>true</c> returns a short representation of the object; otherwise, returns the long representation as <see cref="ToString()"/> does.</param>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public abstract string ToString(bool shortForm);
        #endregion

        #region Public Static Extension Methods
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

        #endregion

        #region Protected Methods
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
            {
                if (_eventSyncObject != null && _eventSyncObject.InvokeRequired)
                    _eventSyncObject.Invoke(ErrorChanged, new object[] { this, e });
                else
                    ErrorChanged(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:IsOnlineChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnIsOnlineChanged(EventArgs e)
        {
            if (IsOnlineChanged != null)
            {
                if (_eventSyncObject != null && _eventSyncObject.InvokeRequired)
                    _eventSyncObject.Invoke(IsOnlineChanged, new object[] { this, e });
                else
                    IsOnlineChanged(this, e);
            }
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

                if (_eventSyncObject != null && _eventSyncObject.InvokeRequired)
                    _eventSyncObject.Invoke(NewLog, new object[] { this, e });
                else
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

                if (_eventSyncObject != null && _eventSyncObject.InvokeRequired)
                    _eventSyncObject.Invoke(Reset, new object[] { this, e });
                else
                    Reset(this, e);
            }
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
