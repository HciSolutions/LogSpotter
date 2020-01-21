using System;
using System.Windows.Forms;
using Triamun.Log4NetViewer.Data;

namespace Triamun.Log4NetViewer.Controls
{
    /// <summary>
    /// Allows to edit the filter.
    /// </summary>
    public partial class DisplayFilter : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayFilter"/> class.
        /// </summary>
        public DisplayFilter()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Public Properties

        /// <summary>
        /// Gets the current filter string.
        /// </summary>
        /// <value>The current filter string.</value>
        public string FilterString
        {
            get
            {
                LogLevels levelFilter = LogLevels.None;

                if (cbFilterLevelTrace.Checked)
                    levelFilter |= LogLevels.Trace;
                if (cbFilterLevelDebug.Checked)
                    levelFilter |= LogLevels.Debug;
                if (cbFilterLevelInfo.Checked)
                    levelFilter |= LogLevels.Info;
                if (cbFilterLevelWarning.Checked)
                    levelFilter |= LogLevels.Warn;
                if (cbFilterLevelError.Checked)
                    levelFilter |= LogLevels.Error;
                if (cbFilterLevelFatal.Checked)
                    levelFilter |= LogLevels.Fatal;


                return new LogEventFilter(
                    tbFilterLogger.Text,
                    tbFilterThread.Text,
                    cbFilterTimeStampEnabled.Checked ? dtpFilterTimeStampFrom.Value : (DateTime?)null,
                    cbFilterTimeStampEnabled.Checked ? dtpFilterTimeStampTo.Value : (DateTime?)null,
                    tbFilterDomain.Text,
                    tbFilterClassName.Text,
                    tbFilterFileName.Text,
                    tbFilterMessage.Text,
                    tbFilterException.Text,
                    levelFilter).ToString();
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Resets the filter values.
        /// </summary>
        public void Reset()
        {
            tbFilterLogger.Text = String.Empty;
            tbFilterThread.Text = String.Empty;
            cbFilterTimeStampEnabled.Checked = false;
            tbFilterDomain.Text = String.Empty;
            tbFilterClassName.Text = String.Empty;
            tbFilterFileName.Text = String.Empty;
            tbFilterMessage.Text = String.Empty;
            tbFilterException.Text = String.Empty;
            cbFilterLevelDebug.Checked = true;
            cbFilterLevelInfo.Checked = true;
            cbFilterLevelWarning.Checked = true;
            cbFilterLevelError.Checked = true;
            cbFilterLevelFatal.Checked = true;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Handles the CheckedChanged event of the cbFilterTimeStampEnabled control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbFilterTimeStampEnabled_CheckedChanged(object sender, EventArgs e)
        {
            dtpFilterTimeStampFrom.Enabled = cbFilterTimeStampEnabled.Checked;
            dtpFilterTimeStampTo.Enabled = cbFilterTimeStampEnabled.Checked;
        }

        #endregion Private Methods
    }
}
