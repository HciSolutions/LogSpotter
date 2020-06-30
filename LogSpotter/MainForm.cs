using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HciSolutions.LogSpotter.Controls;
using HciSolutions.LogSpotter.Data.Config;
using HciSolutions.LogSpotter.Data.Sources;
using HciSolutions.LogSpotter.Dialogs;
using HciSolutions.LogSpotter.Properties;

namespace HciSolutions.LogSpotter
{
    /// <summary>
    /// Main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constants
        private const int MAX_RECENT = 20;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            BuildRecentMenu();
            UpdateUI();

            Config.Current.WindowPositions.LoadWindow(this);
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <value>The current page.</value>
        private TabPage CurrentPage
        {
            get
            {
                if (tcLogs.TabPages.Count == 0)
                    return null;
                return tcLogs.SelectedTab;
            }
        }

        /// <summary>
        /// Gets the current log control.
        /// </summary>
        /// <value>The current log control.</value>
        private LogTab CurrentPageLogControl
        {
            get
            {
                if (CurrentPage == null)
                    return null;
                return CurrentPage.Controls[0] as LogTab;
            }
        }
        #endregion

        #region Protected methods
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

        #region Private Methods
        /// <summary>
        /// Adds a new tab for the specified log data source.
        /// </summary>
        /// <param name="dataSource">The <see cref="LogDataSource"/> for which to add a new page.</param>
        private void AddLogTab(LogDataSource dataSource)
        {
            TabPage pageCtrl = null;
            LogTab logCtrl = null;

            try
            {
                // Creates the page
                pageCtrl = new TabPage(dataSource.ToString(true));
                pageCtrl.ToolTipText = dataSource.ToString(false);

                // Create and add the log control to the tab page
                logCtrl = new LogTab();
                pageCtrl.Controls.Add(logCtrl);
                logCtrl.Dock = DockStyle.Fill;

                // Add the tab to the tab page
                tcLogs.SelectedTab = pageCtrl;

                // Add the tab page
                tcLogs.TabPages.Add(pageCtrl);

                // Apply the data source
                logCtrl.LogSource = dataSource;

                // Updates the history
                for (int i = Config.Current.RecentLogs.Count - 1; i >= 0; i--)
                {
                    if (Config.Current.RecentLogs[i].Type == dataSource.Name && Config.Current.RecentLogs[i].ConnectionString == dataSource.ConnectionString)
                    {
                        Config.Current.RecentLogs.RemoveAt(i);
                        break;
                    }
                }
                Config.Current.RecentLogs.Insert(0, new RecentLog(dataSource.ConnectionString, dataSource.Name));
                if (Config.Current.RecentLogs.Count > MAX_RECENT)
                    Config.Current.RecentLogs.RemoveRange(MAX_RECENT, Config.Current.RecentLogs.Count - MAX_RECENT);

                // Saves the configuration
                Config.Save();

                // Rebuilds the recent menu
                BuildRecentMenu();
            }
            catch
            {
                if (pageCtrl != null)
                    tcLogs.TabPages.Remove(pageCtrl);
            }
        }

        /// <summary>
        /// Builds the menu that contains the recent items.
        /// </summary>
        private void BuildRecentMenu()
        {
            ToolStripItem item = null;
            LogDataSource source = null;
            List<RecentLog> invalidSources = null;
            try
            {
                tsmiRecent.DropDownItems.Clear();

                foreach(RecentLog recent in Config.Current.RecentLogs)
                {
                    try
                    {
                        source = LogDataSourceFactory.Create(recent.Type, recent.ConnectionString);
                        
                        item = tsmiRecent.DropDownItems.Add(
                            source.ToString(true),
                            source.Icon,
                            new EventHandler(tsmiFileRecentItem_Click));
                        item.ToolTipText = source.ToString(false);
                        item.Tag = recent;
                    }
                    catch 
                    { 
                        if (invalidSources == null)
                            invalidSources = new List<RecentLog>();
                        invalidSources.Add(recent); 
                    }
                }

                if (invalidSources != null)
                {
                    foreach(RecentLog invalidSource in invalidSources)
                    {
                        Config.Current.RecentLogs.Remove(invalidSource);
                    }
                    Config.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToRebuildRecentMenu, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }

        }

        /// <summary>
        /// Closes the current log.
        /// </summary>
        private void CloseLog()
        {
            TabPage page = null;
            try
            {
                page = CurrentPage;
                if (page != null)
                {
                    tcLogs.TabPages.Remove(page);
                    page.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToCloseLog, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Toggles the "follow last log" function for the current log tab.
        /// </summary>
        private void CurrentToggleFollowLastLog()
        {
            LogTab current = null;
            try
            {
                current = CurrentPageLogControl;
                if (current != null)
                    current.FollowLastLog = !current.FollowLastLog;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToToggleFollowLastLog, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Toggles the "Pause capture" function for the current log tab.
        /// </summary>
        private void CurrentTogglePauseCapture()
        {
            LogTab current = null;
            try
            {
                current = CurrentPageLogControl;
                if (current != null)
                    current.IsPaused = !current.IsPaused;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToTogglePause, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Applies the filter for the current log tab.
        /// </summary>
        private void CurrentApplyFilter()
        {
            LogTab current = null;
            try
            {
                current = CurrentPageLogControl;
                if (current != null)
                    current.ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToApplyFilter, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Reset the filter for the current log tab.
        /// </summary>
        private void CurrentResetFilter()
        {
            LogTab current = null;
            try
            {
                current = CurrentPageLogControl;
                if (current != null)
                    current.ResetFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToResetFilter, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Edits the settings.
        /// </summary>
        private void EditSettings()
        {
            try
            {
                new SettingsForm().ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToEditSettings, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
                this.Invalidate(true);
            }
        }

        /// <summary>
        /// Opens an XML log file.
        /// </summary>
        private void OpenLogFromFile()
        {
            try
            {
                if (ofdXmlLogFile.ShowDialog(this) == DialogResult.OK)
                    AddLogTab(new FileLogDataSource(ofdXmlLogFile.FileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToOpenLog, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Opens the log from telnet.
        /// </summary>
        private void OpenLogFromTelnet()
        {
            OpenTelnetDialog dlg = null;
            try
            {
                dlg = new OpenTelnetDialog();

                if (dlg.ShowDialog(this) == DialogResult.OK)
                    AddLogTab(new TelnetLogDataSource(dlg.HostName, dlg.PortNumber));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToOpenLog, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Opens the log from SqlServer database.
        /// </summary>
        private void OpenLogFromSqlServerDatabase()
        {
            OpenSqlServerDatabaseDialog dlg = null;
            try
            {
                dlg = new OpenSqlServerDatabaseDialog();

                if (dlg.ShowDialog(this) == DialogResult.OK)
                    AddLogTab(new SqlServerDataSource(SqlServerDataSource.BuildConnectionString(dlg.ConnectionString, dlg.TableName, dlg.PrimaryKey, dlg.OrderingColumn, dlg.Mapping)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToOpenLog, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Opens the recent.
        /// </summary>
        /// <param name="recentEntry">The recent entry that indicates the log to open.</param>
        private void OpenRecent(RecentLog recentEntry)
        {
            LogDataSource source = null;
            try
            {
                source = LogDataSourceFactory.Create(recentEntry.Type, recentEntry.ConnectionString);

                AddLogTab(source);
            }
            catch (Exception ex)
            {
                if (Config.Current.RecentLogs.Remove(recentEntry))
                    Config.Save();
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToOpenRecentLog, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Updates the UI controls so that they reflect the current state.
        /// </summary>
        private void UpdateUI()
        {
            LogTab current = null;
            try
            {
                current = CurrentPageLogControl;

                // Menus
                tsmiFileCloseLog.Enabled = current != null;
                tsmiFilter.Enabled = current != null;
                tsmiMode.Enabled = current != null;
                tsmiModeFollow.Checked = current != null && current.FollowLastLog;
                tsmiModePaused.Checked = current != null && current.IsPaused;

                // Toolbar
                tsbFilterApply.Enabled = current != null;
                tsbFilterReset.Enabled = current != null;
                tsbModeFollow.Enabled = current != null;
                tsbModeFollow.Checked = current != null && current.FollowLastLog;
                tsbModePaused.Enabled = current != null;
                tsbModePaused.Checked = current != null && current.IsPaused;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToUpdateControlsState, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Event methods
        /// <summary>
        /// Handles the SelectedIndexChanged event of the tcLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tcLogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of the tsbLogClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbLogClose_Click(object sender, EventArgs e)
        {
            CloseLog();
        }

        /// <summary>
        /// Handles the Click event of the tsbFilterApply control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbFilterApply_Click(object sender, EventArgs e)
        {
            CurrentApplyFilter();
        }

        /// <summary>
        /// Handles the Click event of the tsbFilterReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbFilterReset_Click(object sender, EventArgs e)
        {
            CurrentResetFilter();
        }

        /// <summary>
        /// Handles the Click event of the tsbModeFollow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbModeFollow_Click(object sender, EventArgs e)
        {
            CurrentToggleFollowLastLog();
        }

        /// <summary>
        /// Handles the ButtonClick event of the tsbLogOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbLogOpen_ButtonClick(object sender, EventArgs e)
        {
            tsbLogOpen.ShowDropDown();
        }

        /// <summary>
        /// Handles the Click event of the tsbLogOpenFromFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbLogOpenFromFile_Click(object sender, EventArgs e)
        {
            OpenLogFromFile();
        }

        /// <summary>
        /// Handles the Click event of the tsbLogOpenFromTelnet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbLogOpenFromTelnet_Click(object sender, EventArgs e)
        {
            OpenLogFromTelnet();
        }

        /// <summary>
        /// Handles the Click event of the tsbModePaused control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsbModePaused_Click(object sender, EventArgs e)
        {
            CurrentTogglePauseCapture();
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileCloseLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiFileCloseLog_Click(object sender, EventArgs e)
        {
            CloseLog();
        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileOpenLogFromFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiFileOpenLogFromFile_Click(object sender, EventArgs e)
        {
            OpenLogFromFile();
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileOpenLogFromTelnet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiFileOpenLogFromTelnet_Click(object sender, EventArgs e)
        {
            OpenLogFromTelnet();
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileRecentItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiFileRecentItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = null;
            RecentLog log = null;

            // Obtains the RecentLog instance from the item
            if ((item = sender as ToolStripItem) == null)
                return;
            if ((log = item.Tag as RecentLog) == null)
                return;

            OpenRecent(log);
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiFileSettings_Click(object sender, EventArgs e)
        {
            EditSettings();
        }

        /// <summary>
        /// Handles the Click event of the tsmiFilterApply control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiFilterApply_Click(object sender, EventArgs e)
        {
            CurrentApplyFilter();
        }

        /// <summary>
        /// Handles the Click event of the tsmiFilterReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiFilterReset_Click(object sender, EventArgs e)
        {
            CurrentResetFilter();
        }

        /// <summary>
        /// Handles the Click event of the tsmiModeFollow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiModeFollow_Click(object sender, EventArgs e)
        {
            CurrentToggleFollowLastLog();
        }

        /// <summary>
        /// Handles the Click event of the tsmiModePaused control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsmiModePaused_Click(object sender, EventArgs e)
        {
            CurrentTogglePauseCapture();
        }
        #endregion

        private void tsmiDatabaseSqlServer_Click(object sender, EventArgs e)
        {
            OpenLogFromSqlServerDatabase();
        }

        private void tsbLogOpenFromSqlServerDatabase_Click(object sender, EventArgs e)
        {
            OpenLogFromSqlServerDatabase();
        }
    }
}
