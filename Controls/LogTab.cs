using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Triamun.Log4NetViewer.Data;
using Triamun.Log4NetViewer.Data.Sources;
using Triamun.Log4NetViewer.Properties;
using System.Threading;

namespace Triamun.Log4NetViewer.Controls
{
    /// <summary>
    /// Represents the content of a log tab.
    /// </summary>
    public partial class LogTab : UserControl
    {
		#region Private Members 

        private LogDataSource _source;
        private LogEventCollection _logs;
        private Action<LogDataSource> _asyncLoadDelegate;

		#endregion Private Members 

		#region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="LogTab"/> class.
        /// </summary>
        public LogTab()
        {
            InitializeComponent();
            _source = null;
            _logs = new LogEventCollection();
            _asyncLoadDelegate = null;
            UpdateStatusIcons();
        }

		#endregion Constructor 

		#region Public Events 

        /// <summary>
        /// Occurs when a loading operation has failed.
        /// </summary>
        public event EventHandler LoadFailed;

        /// <summary>
        /// Occurs when a loading operation has succeeded.
        /// </summary>
        public event EventHandler LoadSucceeded;

		#endregion Public Events 

		#region Public Properties 

        /// <summary>
        /// Gets a value indicating whether the log source managed by the tab supports the refresh functionality that force checking for new logs.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the log source managed by the tab supports the refresh functionality that force checking for new logs; otherwise, <c>false</c>.
        /// </value>
        public bool CanRefreshLogEntries
        {
            get { return _source != null && _source.CanRefresh; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the selected item list is always the last inserted log.
        /// </summary>
        /// <value><c>true</c> if the selected item list is always the last inserted log; otherwise, <c>false</c>.</value>
        public bool FollowLastLog
        {
            get { return ucLogView.FollowLastLog; }
            set
            {
                ucLogView.FollowLastLog = value;
                UpdateStatusIcons();
            }
        }

        /// <summary>
        /// Gets or value indicating whether a filter is active.
        /// </summary>
        /// <value><c>true</c> if a filter is active; otherwise, <c>false</c>.</value>
        public bool IsFiltered
        {
            get { return _logs != null && !String.IsNullOrEmpty(_logs.Filter); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is paused.
        /// </summary>
        /// <value><c>true</c> if this instance is paused; otherwise, <c>false</c>.</value>
        public bool IsPaused
        {
            get { return _source == null ? false : _source.IsPaused; }
            set
            {
                if (_source != null)
                {
                    _source.IsPaused = value;
                    UpdateStatusIcons();
                }
            }
        }

        /// <summary>
        /// Gets or sets the log source.
        /// </summary>
        /// <value>The log source.</value>
        public LogDataSource LogSource
        {
            get { return _source; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether time is shown with milliseconds.
        /// </summary>
        /// <value><c>true</c> if time is shown with milliseconds; otherwise, <c>false</c>.</value>
        public bool ShowMilliseconds
        {
            get { return ucLogView.ShowMilliseconds; }
            set { ucLogView.ShowMilliseconds = value; }
        }

        /// <summary>
        /// Gets or Sets the text associated with this control.
        /// </summary>
        /// <returns>
        /// The text associated with this control.
        /// </returns>
        public override string Text
        {
            get { return _source == null ? String.Empty : _source.ToString(); }
            set { }
        }

		#endregion Public Properties 

		#region Public Methods 

        /// <summary>
        /// Applies the current filter.
        /// </summary>
        public void ApplyFilter()
        {
            bool oldIsPaused = false;
            if (_logs != null)
            {
                if (_source != null)
                {
                    oldIsPaused = _source.IsPaused;
                    _source.IsPaused = true;
                }
                try
                {
                    _logs.Filter = ucDisplayFilter.FilterString;
                    UpdateStatusIcons();
                    UpdateEventCount();
                }
                finally
                {
                    if (_source != null)
                        _source.IsPaused = oldIsPaused;
                }
            }
        }

        /// <summary>
        /// Closes the source.
        /// </summary>
        public void CloseSource()
        {
            if (_source != null)
            {
                _source.Close();
                _source = null;
            }
        }

        /// <summary>
        /// Goes to the specified event.
        /// </summary>
        /// <param name="eventNumber">The event number to go to.</param>
        /// <returns><c>true</c> if the specified event could be found; otherwise, <c>false</c>.</returns>
        public bool GoToEvent(int eventNumber)
        {
            return ucLogView.GoToEvent(eventNumber);
        }

        /// <summary>
        /// Loads the specified data source.
        /// </summary>
        /// <param name="dataSource">The <see cref="LogDataSource"/> to load.</param>
        public void LoadSource(LogDataSource dataSource)
        {
            if (_asyncLoadDelegate != null)
                throw new InvalidOperationException();

            _asyncLoadDelegate = new Action<LogDataSource>(LoadSourceCore);

            SetProgressMode(true);
            ucLogView.LogCollection = null;
            _asyncLoadDelegate.BeginInvoke(dataSource, new AsyncCallback(LoadSourceFinished), null);
        }

        /// <summary>
        /// Force checking for new logs.
        /// </summary>
        public void RefreshLogEntries()
        {
            if (_source != null)
                _source.Refresh();
        }

        /// <summary>
        /// Resets the current filter.
        /// </summary>
        public void ResetFilter()
        {
            try
            {
                ucDisplayFilter.Reset();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Failed to reset the filter.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		#endregion Public Methods 

		#region Protected Methods 

        /// <summary>
        /// Raises the <see cref="E:LoadFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnLoadFailed(EventArgs e)
        {
            if (LoadFailed != null)
                LoadFailed(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:LoadSucceeded"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnLoadSucceeded(EventArgs e)
        {
            if (LoadSucceeded != null)
                LoadSucceeded(this, e);
        }

		#endregion Protected Methods 

		#region Private Methods 

        /// <summary>
        /// Adds new events log events
        /// </summary>
        /// <param name="item">The list of events to add.</param>
        private void AddLogEvents(LogEvent[] items)
        {
            try
            {
                _logs.AddEvents(items);
                UpdateEventCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Failed to add new log events.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the source error changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleSourceErrorChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action(UpdateStatusIcons));
            else
                UpdateStatusIcons();
        }

        /// <summary>
        /// Handles the source is online changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleSourceIsOnlineChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action(UpdateStatusIcons));
            else
                UpdateStatusIcons();
        }

        /// <summary>
        /// Handles the source new log.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NewLogEventArgs"/> instance containing the event data.</param>
        private void HandleSourceNewLog(object sender, NewLogEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action<LogEvent[]>(AddLogEvents), new object[] { e.Events });
            else
                AddLogEvents(e.Events);
        }

        /// <summary>
        /// Handles the source open progress.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="OpenProgressEventArgs"/> instance containing the event data.</param>
        private void HandleSourceOpenProgress(object sender, OpenProgressEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new OpenProgressEventHandler(HandleSourceOpenProgress), new object[] { sender, e });
            else
                SetLoadProgress(e.EventCount, e.PercentDone);
        }

        /// <summary>
        /// Handles the source reset.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ResetEventArgs"/> instance containing the event data.</param>
        private void HandleSourceReset(object sender, ResetEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action<LogEvent[]>(ResetLogEvents), new object[] { e.Events });
            else
                ResetLogEvents(e.Events);
        }

        /// <summary>
        /// Loads the core.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        private void LoadSourceCore(LogDataSource dataSource)
        {
            // Unregister events
            if (_source != null)
            {
                _source.ErrorChanged -= new EventHandler(HandleSourceErrorChanged);
                _source.IsOnlineChanged -= new EventHandler(HandleSourceIsOnlineChanged);
                _source.OpenProgress -= new OpenProgressEventHandler(HandleSourceOpenProgress);
                _source.Reset -= new ResetEventHandler(HandleSourceReset);
                _source.NewLog -= new NewLogEventHandler(HandleSourceNewLog);
            }

            // Updates the local source reference
            _source = dataSource;

            // Register events & setup the event sync object
            if (_source != null)
            {
                _source.ErrorChanged += new EventHandler(HandleSourceErrorChanged);
                _source.IsOnlineChanged += new EventHandler(HandleSourceIsOnlineChanged);
                _source.OpenProgress += new OpenProgressEventHandler(HandleSourceOpenProgress);
                _source.Reset += new ResetEventHandler(HandleSourceReset);
                _source.NewLog += new NewLogEventHandler(HandleSourceNewLog);
            }
            _logs.Reset(null);

            // Setup and load the logs.
            if (_source != null)
                _source.Open();
        }

        /// <summary>
        /// Finishes the loading
        /// </summary>
        /// <param name="asyncResult">The <see cref="IAsyncResult"/> that represents the state of the async call.</param>
        private void LoadSourceFinished(IAsyncResult asyncResult)
        {
            if (InvokeRequired)
                Invoke(new Action<IAsyncResult>(LoadSourceFinished), asyncResult);
            else
            {
                try
                {
                    _asyncLoadDelegate.EndInvoke(asyncResult);

                    tsslFullSourceName.Text = (_source == null) ? String.Empty : _source.ToString();
                    OnLoadSucceeded(EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    _logs.Reset(null);
                    tsslFullSourceName.Text = String.Empty;
                    MessageBox.Show(this, ex.Message, Resources.Err_FailedToOpenLog, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    OnLoadFailed(EventArgs.Empty);
                }
                finally
                {
                    _asyncLoadDelegate = null;

                    // Setup the child controls
                    ucDisplayFilter.Reset();
                    ucLogView.LogCollection = _logs;

                    // Reset
                    SetProgressMode(false);
                    UpdateStatusIcons();
                    UpdateEventCount();
                }
            }
        }

        /// <summary>
        /// Resets the current slog events.
        /// </summary>
        /// <param name="items">The items that the list must contain after the reset.</param>
        private void ResetLogEvents(LogEvent[] items)
        {
            try
            {
                _logs.Reset(items);
                UpdateEventCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Failed to reset the list of log events", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sets the load progress text.
        /// </summary>
        /// <param name="eventCount">The number of events loaded so far.</param>
        /// <param name="percentageDone">The optional percentage done.</param>
        private void SetLoadProgress(int eventCount, int? percentageDone)
        {
            try
            {
                lblLoad.Text = String.Format(Resources.Fmt_LoadingEvents, eventCount);

                if (percentageDone.HasValue)
                {
                    if (pbLoad.Style != ProgressBarStyle.Blocks)
                        pbLoad.Style = ProgressBarStyle.Blocks;
                    pbLoad.Value = Math.Min(100, Math.Max(0, percentageDone.Value));
                }
                else
                {
                    if (pbLoad.Style != ProgressBarStyle.Marquee)
                        pbLoad.Style = ProgressBarStyle.Marquee;
                }
                Application.DoEvents();
            }
            catch { }
        }

        /// <summary>
        /// Enters the progress mode.
        /// </summary>
        /// <param name="on">if set to <c>true</c> the progress mode is on; otherwise, it is off.</param>
        private void SetProgressMode(bool on)
        {
            if (!tlpLoad.Visible && on)
                SetLoadProgress(0, null);

            tlpLoad.Visible = on;
            tlpMain.Visible = !on;
            Application.DoEvents();
        }

        /// <summary>
        /// Handles the Tick event of the tmrBinkErrorStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrBinkErrorStatus_Tick(object sender, EventArgs e)
        {
            if (tsslStatusError.Image == null)
                tsslStatusError.Image = Resources.DataSourceError;
            else
                tsslStatusError.Image = null;
        }

        /// <summary>
        /// Updates the event counts.
        /// </summary>
        private void UpdateEventCount()
        {
            try
            {
                tsslShownEvents.Text = String.Format(Resources.Fmt_ShownEvents, _logs.Count);
                tsslTotalEvents.Text = String.Format(Resources.Fmt_TotalEvents, _logs.UnfilteredCount);
            }
            catch { }
        }

        /// <summary>
        /// Updates the status icons.
        /// </summary>
        private void UpdateStatusIcons()
        {
            try
            {
                if (IsPaused)
                    tsslStatusPause.Image = Resources.PauseCapture;
                else
                    tsslStatusPause.Image = null;

                if (FollowLastLog)
                    tsslStatusFollow.Image = Resources.FollowLastLog;
                else
                    tsslStatusFollow.Image = null;

                if (IsFiltered)
                    tsslStatusFilter.Image = Resources.FilterEnabled;
                else
                    tsslStatusFilter.Image = null;

                if (_source != null && _source.IsOnline)
                    tsslStatusOnline.Image = Resources.Online;
                else
                    tsslStatusOnline.Image = Resources.Offline;

                if (_source != null && !String.IsNullOrEmpty(_source.Error))
                {
                    tsslStatusError.Image = Resources.DataSourceError;
                    tsslStatusError.ToolTipText = _source.Error;
                    if (!tmrBinkErrorStatus.Enabled)
                        tmrBinkErrorStatus.Enabled = true;
                }
                else
                {
                    tmrBinkErrorStatus.Enabled = false;
                    tsslStatusError.Image = null;
                }

                Application.DoEvents();
            }
            catch { }
        }

		#endregion Private Methods 
    }
}
