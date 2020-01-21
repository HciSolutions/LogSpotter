using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Triamun.Log4NetViewer.Data;
using Triamun.Log4NetViewer.Data.Config;
using Triamun.Log4NetViewer.Properties;

namespace Triamun.Log4NetViewer.Controls
{
    /// <summary>
    /// Displays a collection of <see cref="LogEvent" />.
    /// </summary>
    public partial class LogViewer : UserControl
    {
        #region Private Constants

        private const string COPY_COLUMN_TEXT_FORMAT = "Copy selected rows of column '{0}' to clipboard";
        private const string DATETIME_WITH_MILLISECONDS = "dd.MM.yyyy HH:mm:ss.fff";
        private const int MILLISECONDS_TEXTWIDTH = 30;

        #endregion Private Constants

        #region Private Fields

        private bool _bindingEnabled;
        private bool _followLastLog;
        private DataGridViewColumn _selectedColumn;
        private bool _showMilliseconds;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogViewer"/> class.
        /// </summary>
        public LogViewer()
        {
            InitializeComponent();
            _followLastLog = true;
            _bindingEnabled = true;
            _selectedColumn = messageDataGridViewTextBoxColumn;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the selected item list is always the last inserted log.
        /// </summary>
        /// <value><c>true</c> if the selected item list is always the last inserted log; otherwise, <c>false</c>.</value>
        public bool FollowLastLog
        {
            get { return _followLastLog; }
            set { _followLastLog = value; }
        }

        /// <summary>
        /// Gets or sets the log collection that is displayed.
        /// </summary>
        /// <value>The log collection that is displayed.</value>
        public LogEventCollection LogCollection
        {
            get { return bsLogs.DataSource as LogEventCollection; }
            set { bsLogs.DataSource = (object)value ?? (object)new LogEvent[0]; }
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

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Goes to the specified event.
        /// </summary>
        /// <param name="eventNumber">The event number to go to.</param>
        /// <returns><c>true</c> if the specified event could be found; otherwise, <c>false</c>.</returns>
        public bool GoToEvent(int eventNumber)
        {
            LogEventCollection collection = LogCollection;

            if (collection != null)
            {
                int index = collection.FindEvent(eventNumber);

                if (index >= 0 && index < bsLogs.Count)
                {
                    bsLogs.Position = index;
                    dgvLogs.Focus();
                    return true;
                }
            }

            return false;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Handles the ListChanged event of the bsLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ListChangedEventArgs"/> instance containing the event data.</param>
        private void bsLogs_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                // Follows the last log ?
                if (_followLastLog && _bindingEnabled)
                {
                    if (e.ListChangedType == ListChangedType.ItemAdded)
                    {
                        bsLogs.Position = Math.Min(e.NewIndex, bsLogs.Count - 1);
                        Application.DoEvents();
                    }
                    else if (e.ListChangedType == ListChangedType.Reset)
                    {
                        bsLogs.Position = bsLogs.Count - 1;
                        Application.DoEvents();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Handles the Opening event of the cmsColumns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void cmsColumns_Opening(object sender, CancelEventArgs e)
        {
            tsmiCopy.Text = String.Format(COPY_COLUMN_TEXT_FORMAT, _selectedColumn.HeaderText);
        }

        /// <summary>
        /// Handles the CellMouseDown event of the dgvLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void dgvLogs_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            _selectedColumn = dgvLogs.Columns[e.ColumnIndex];
        }

        /// <summary>
        /// Handles the CellPainting event of the dgvLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellPaintingEventArgs"/> instance containing the event data.</param>
        private void dgvLogs_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            LogEvent ev = null;
            Color foreColor = Color.Empty;
            Color backColor = Color.Empty;

            try
            {
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
            catch
            {
                System.Diagnostics.Debug.WriteLine("Error");
            }
        }

        /// <summary>
        /// Handles the DataError event of the dgvLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void dgvLogs_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine(String.Format("Data error at ({0},{1}) ({2}/{3}) : {4}\r\n{5}", e.RowIndex, e.ColumnIndex, bsLogs.Position, bsLogs.Count, e.Exception.Message, e.Exception.StackTrace));

                //bsLogs.ResetBindings(false);
                bsLogs.Position = 0;

                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the dgvLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void dgvLogs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                SelectionToClipboard(messageDataGridViewTextBoxColumn, true);
            }
        }

        /// <summary>
        /// Selections to clipboard.
        /// </summary>
        /// <param name="dataGridViewColumn">The data grid view column.</param>
        /// <param name="removeNewLines">if set to <c>true</c> remove new lines (CrLf) for each selected lines.</param>
        private void SelectionToClipboard(DataGridViewColumn dataGridViewColumn, bool removeNewLines)
        {
            StringBuilder sb = new StringBuilder();
            if (dataGridViewColumn != null)
            {
                foreach (DataGridViewRow row in dgvLogs.SelectedRows.OfType<DataGridViewRow>().OrderBy(r => r.Index))
                {
                    string text = Convert.ToString(row.Cells[dataGridViewColumn.Index].Value);
                    if (removeNewLines) text = text.Replace(Environment.NewLine, " | ");
                    sb.AppendLine(text);
                }
            }
            Clipboard.Clear();
            Clipboard.SetText(sb.ToString());
        }

        /// <summary>
        /// Handles the DoubleClick event of the tbDetail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the tsmiCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            SelectionToClipboard(_selectedColumn, true);
        }

        #endregion Private Methods
    }
}
