using System;
using System.Windows.Forms;
using HciSolutions.LogSpotter.Data.Config;
using HciSolutions.LogSpotter.Properties;

namespace HciSolutions.LogSpotter
{
    /// <summary>
    /// User settings form.
    /// </summary>
    public partial class SettingsForm : Form
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsForm"/> class.
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();

            // Initialize the data sources.
            bsConfig.DataSource = Config.Current;
            bsColorTrace.DataSource = Config.Current.EventColors.Trace;
            bsColorDebug.DataSource = Config.Current.EventColors.Debug;
            bsColorInfo.DataSource = Config.Current.EventColors.Info;
            bsColorWarning.DataSource = Config.Current.EventColors.Warning;
            bsColorError.DataSource = Config.Current.EventColors.Error;
            bsColorFatal.DataSource = Config.Current.EventColors.Fatal;

            Config.Current.WindowPositions.LoadWindow(this);
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Config.Load();
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToCancelConfigChanges, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Config.Save();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToSaveConfig, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the one of the pnlLog...Color control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pnlLogColor_Click(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;

            if (panel != null)
            {
                cdColor.Color = panel.BackColor;
                if (cdColor.ShowDialog() == DialogResult.OK)
                    panel.BackColor = cdColor.Color;
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            Config.Current.WindowPositions.SaveWindow(this);
            Config.Save();
        }
        #endregion
    }
}
