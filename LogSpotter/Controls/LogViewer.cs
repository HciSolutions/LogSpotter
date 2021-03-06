﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HciSolutions.LogSpotter.Data;
using HciSolutions.LogSpotter.Data.Config;
using HciSolutions.LogSpotter.Properties;

namespace HciSolutions.LogSpotter.Controls
{
    /// <summary>
    /// Displays a collection of <see cref="LogEvent" />.
    /// </summary>
    public partial class LogViewer : UserControl
    {
        #region Private Constants

        private const string DATETIME_WITH_MILLISECONDS = "dd.MM.yyyyHH:mm:ss.fff";
        private const int MILLISECONDS_TEXTWIDTH = 30;

        #endregion

        #region Private Fields

        private bool _showMilliseconds;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogViewer" /> class.
        /// </summary>
        public LogViewer()
        {
            InitializeComponent();
            FollowLastLog = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the selected item list is always the last inserted log.
        /// </summary>
        /// <value><c>true</c> if the selected item list is always the last inserted log; otherwise, <c>false</c>.</value>
        public bool FollowLastLog { get; set; }

        /// <summary>
        /// Gets or sets the log collection that is displayed.
        /// </summary>
        /// <value>The log collection that is displayed.</value>
        public LogEventCollection LogCollection
        {
            get => bsLogs.DataSource as LogEventCollection;
            set => bsLogs.DataSource = (object)value ?? (object)new LogEvent[0];
        }

        /// <summary>
        /// Gets or sets a value indicating whether time is shown with milliseconds.
        /// </summary>
        /// <value><c>true</c> if time is shown with milliseconds; otherwise, <c>false</c>.</value>
        public bool ShowMilliseconds
        {
            get { return _showMilliseconds; }
            set
            {
                if (_showMilliseconds != value)
                {
                    _showMilliseconds = value;
                    DataGridViewColumn column = dgvLogs.Columns[timeStampDataGridViewTextBoxColumn.Index];
                    if (_showMilliseconds)
                    {
                        column.DefaultCellStyle.Format = DATETIME_WITH_MILLISECONDS;
                        column.Width += MILLISECONDS_TEXTWIDTH;
                    }
                    else
                    {
                        column.DefaultCellStyle.Format = "G";
                        column.Width -= MILLISECONDS_TEXTWIDTH;
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the ListChanged event of the bsLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ListChangedEventArgs" /> instance containing the event data.</param>
        private void bsLogs_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                // Follows the last log ?
                if (FollowLastLog)
                {
                    if (e.ListChangedType == ListChangedType.ItemAdded)
                    {
                        bsLogs.Position = e.NewIndex;
                        Application.DoEvents();
                    }
                    else if (e.ListChangedType == ListChangedType.Reset)
                    {
                        bsLogs.Position = bsLogs.Count - 1;
                        Application.DoEvents();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles the CellPainting event of the dgvLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellPaintingEventArgs" /> instance containing the event data.</param>
        private void dgvLogs_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            LogEvent ev = null;
            Color foreColor = Color.Empty;
            Color backColor = Color.Empty;

            if (grid != null
                && e.RowIndex >= 0
                && e.RowIndex < grid.Rows.Count
                && (ev = grid.Rows[e.RowIndex].DataBoundItem as LogEvent) != null)
            {
                // Select the right colors
                switch (ev.Level)
                {
                    case LogLevels.Trace:
                        backColor = Config.Current.EventColors.Trace.BackgroundColor;
                        foreColor = Config.Current.EventColors.Trace.ForegroundColor;
                        break;
                    case LogLevels.Debug:
                        backColor = Config.Current.EventColors.Debug.BackgroundColor;
                        foreColor = Config.Current.EventColors.Debug.ForegroundColor;
                        break;
                    case LogLevels.Info:
                        backColor = Config.Current.EventColors.Info.BackgroundColor;
                        foreColor = Config.Current.EventColors.Info.ForegroundColor;
                        break;
                    case LogLevels.Warn:
                        backColor = Config.Current.EventColors.Warning.BackgroundColor;
                        foreColor = Config.Current.EventColors.Warning.ForegroundColor;
                        break;
                    case LogLevels.Error:
                        backColor = Config.Current.EventColors.Error.BackgroundColor;
                        foreColor = Config.Current.EventColors.Error.ForegroundColor;
                        break;
                    case LogLevels.Fatal:
                        backColor = Config.Current.EventColors.Fatal.BackgroundColor;
                        foreColor = Config.Current.EventColors.Fatal.ForegroundColor;
                        break;
                }

                e.CellStyle.ForeColor = foreColor;
                e.CellStyle.BackColor = backColor;
                e.CellStyle.SelectionForeColor = backColor;
                e.CellStyle.SelectionBackColor = foreColor;
            }

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            e.Handled = true;
        }

        /// <summary>
        /// Handles the DoubleClick event of the tbDetail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void tbDetail_DoubleClick(object sender, EventArgs e)
        {
            TextBox ctrl = sender as TextBox;
            try
            {
                if (ctrl == tbDetailLevel)
                    TextViewerForm.ShowTextViewer(this, lblDetailLevelTitle.Text.TrimEnd(' ', ':'), tbDetailLevel.Text);
                else if (ctrl == tbDetailTimeStamp)
                    TextViewerForm.ShowTextViewer(this, lblDetailTimeStampTitle.Text.TrimEnd(' ', ':'), tbDetailTimeStamp.Text);
                else if (ctrl == tbDetailThread)
                    TextViewerForm.ShowTextViewer(this, lblDetailThreadTitle.Text.TrimEnd(' ', ':'), tbDetailThread.Text);
                else if (ctrl == tbDetailDomain)
                    TextViewerForm.ShowTextViewer(this, lblDetailDomainTitle.Text.TrimEnd(' ', ':'), tbDetailDomain.Text);
                else if (ctrl == tbDetailLogger)
                    TextViewerForm.ShowTextViewer(this, lblDetailLoggerTitle.Text.TrimEnd(' ', ':'), tbDetailLogger.Text);
                else if (ctrl == tbDetailClass)
                    TextViewerForm.ShowTextViewer(this, lblDetailClassTitle.Text.TrimEnd(' ', ':'), tbDetailClass.Text);
                else if (ctrl == tbDetailMethod)
                    TextViewerForm.ShowTextViewer(this, lblDetailMethodTitle.Text.TrimEnd(' ', ':'), tbDetailMethod.Text);
                else if (ctrl == tbDetailFileName)
                    TextViewerForm.ShowTextViewer(this, lblDetailFileNameTitle.Text.TrimEnd(' ', ':'), tbDetailFileName.Text);
                else if (ctrl == tbDetailMessage)
                    TextViewerForm.ShowTextViewer(this, lblDetailMessageTitle.Text.TrimEnd(' ', ':'), tbDetailMessage.Text);
                else if (ctrl == tbDetailException)
                    TextViewerForm.ShowTextViewer(this, lblDetailExceptionTitle.Text.TrimEnd(' ', ':'), tbDetailException.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToOpenDetailView, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
