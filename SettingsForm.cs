using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Triamun.Log4NetViewer.Data.Config;
using Triamun.Log4NetViewer.Properties;
using System.Diagnostics;
using Log4NetViewer;

namespace Triamun.Log4NetViewer
{
    /// <summary>
    /// User settings form.
    /// </summary>
    public partial class SettingsForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsForm"/> class.
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();

            // Initialize the data sources.
            bsConfig.DataSource = Config.Current;
            bsColorDebug.DataSource = Config.Current.EventColors.Debug;
            bsColorInfo.DataSource = Config.Current.EventColors.Info;
            bsColorWarning.DataSource = Config.Current.EventColors.Warning;
            bsColorError.DataSource = Config.Current.EventColors.Error;
            bsColorFatal.DataSource = Config.Current.EventColors.Fatal;
        }

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

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            Config.Save();
        }

        /// <summary>
        /// Handles the Click event of the btnRegisterExtension control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnRegisterExtension_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                Process p = null;

                psi.Verb = "runas";
                psi.FileName = Application.ExecutablePath;
                psi.Arguments = Program.REGISTER_ARG;
                p = Process.Start(psi);
                p.WaitForExit();
                if (p.ExitCode != 0)
                    throw new InvalidOperationException(Resources.Err_RegistationError);
                MessageBox.Show(Resources.Msg_RegistrationSuccess, Resources.DlgTitle_Registration, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.DlgTitle_Registration, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
