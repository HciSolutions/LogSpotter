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

namespace Triamun.Log4NetViewer.Controls
{
    /// <summary>
    /// Represents the content of a log tab.
    /// </summary>
    public partial class LogTab : UserControl
    {
        #region Private Members
        private LogDataSource _source;
        private LogEventCollection _logs;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LogTab"/> class.
        /// </summary>
        public LogTab()
        {
            InitializeComponent();
            _source = null;
            _logs = null;
            UpdateStatusIcons();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or value indicating whether a filter is active.
        /// </summary>
        /// <value><c>true</c> if a filter is active; otherwise, <c>false</c>.</value>
        public bool IsFiltered
        {
            get { return _logs != null && !String.IsNullOrEmpty(_logs.Filter); }
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
            set
            {
                if (_source != value)
                {
                    // Unregister events
                    if (_source != null)
                    {
                        _source.ErrorChanged -= new EventHandler(HandleSourceErrorChanged);
                        _source.IsOnlineChanged -= new EventHandler(HandleSourceIsOnlineChanged);
                        _source.OpenProgress -= new OpenProgressEventHandler(HandleSourceOpenProgress);
                    }
                    if (_logs != null)
                        _logs.ListChanged -= new ListChangedEventHandler(HandleLogsListChanged);

                    // Updates the local source reference
                    _source = value;

                    // Register events & setup the event sync object
                    if (_source != null)
                    {
                        _source.EventSyncObject = this;
                        _source.ErrorChanged += new EventHandler(HandleSourceErrorChanged);
                        _source.IsOnlineChanged += new EventHandler(HandleSourceIsOnlineChanged);
                        _source.OpenProgress += new OpenProgressEventHandler(HandleSourceOpenProgress);
                    }

                    // Setup and load the logs.
                    if (_source == null)
                        _logs = null;
                    else
                    {
                        SetProgressMode(true);
                        try
                        {
                            _logs = new LogEventCollection(_source);
                        }
                        finally { SetProgressMode(false); }
                        _logs.ListChanged += new ListChangedEventHandler(HandleLogsListChanged);
                    }

                    // Setup the child controls
                    ucDisplayFilter.Reset();
                    ucLogView.LogCollection = _logs;

                    // Shows the full source name in the status bar
                    if (_source == null)
                        tsslFullSourceName.Text = String.Empty;
                    else
                        tsslFullSourceName.Text = _source.ToString(false);
                    tsslFullSourceName.ToolTipText = tsslFullSourceName.Text;

                    // Reset
                    UpdateStatusIcons();
                    UpdateEventCount();
                }
            }
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
        #endregion

        #region Public Methods
        /// <summary>
        /// Applies the current filter.
        /// </summary>
        public void ApplyFilter()
        {
            if (_logs != null)
            {
                _logs.Filter = ucDisplayFilter.FilterString;
                UpdateStatusIcons();
            }
        }

        /// <summary>
        /// Resets the current filter.
        /// </summary>
        public void ResetFilter()
        {
            ucDisplayFilter.Reset();
            ApplyFilter();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Enters the progress mode.
        /// </summary>
        /// <param name="on">if set to <c>true</c> the progress mode is on; otherwise, it is off.</param>
        private void SetProgressMode(bool on)
        {
            if (!tsslLoading.Visible && on)
                tsslLoading.Text = String.Format(Resources.Fmt_LoadingEvents, 0);

            tsslLoading.Visible = on;

            tsslTotalEvents.Visible = !on;
            tsslShownEvents.Visible = !on;
            tsslStatusFilter.Visible = !on;
            tsslStatusFollow.Visible = !on;
            tsslStatusOnline.Visible = !on;
            tsslStatusPause.Visible = !on;
        }

        /// <summary>
        /// Handles the logs list changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ListChangedEventArgs"/> instance containing the event data.</param>
        private void HandleLogsListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateEventCount();
        }

        /// <summary>
        /// Handles the source error changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleSourceErrorChanged(object sender, EventArgs e)
        {
            UpdateStatusIcons();
        }

        /// <summary>
        /// Handles the source is online changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleSourceIsOnlineChanged(object sender, EventArgs e)
        {
            UpdateStatusIcons();
        }

        /// <summary>
        /// Handles the source open progress.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="OpenProgressEventArgs"/> instance containing the event data.</param>
        private void HandleSourceOpenProgress(object sender, OpenProgressEventArgs e)
        {
            tsslLoading.Text = String.Format(Resources.Fmt_LoadingEvents, e.EventCount);
            Application.DoEvents();
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
        #endregion

        #region Event Methods
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
        #endregion
    }
}
