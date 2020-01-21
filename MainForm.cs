using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Log4NetViewer.Utils;
using Triamun.Log4NetViewer;
using Triamun.Log4NetViewer.Controls;
using Triamun.Log4NetViewer.Data.Config;
using Triamun.Log4NetViewer.Data.Sources;
using Triamun.Log4NetViewer.Properties;
using Triamun.Log4NetViewer.Utils;

namespace Log4NetViewer
{
    /// <summary>
    /// Main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Private Constants

        private const int MAX_RECENT = 20;
        private const int RECENT_TEXT_MAX_LENGTH = 100;
        private const int TAB_TEXT_MAX_LENGTH = 1;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            BuildRecentMenu();
            LoadFavoritesMenu();
            UpdateUI();
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

        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        if (File.Exists(file))
                        {
                            OpenLogFromFile(file);
                            Application.DoEvents();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds a new tab for the specified log data source.
        /// </summary>
        /// <param name="dataSource">The <see cref="LogDataSource" /> for which to add a new page.</param>
        private void AddLogTab(LogDataSource dataSource)
        {
            TabPage pageCtrl = null;

            try
            {
                // Creates the page
                pageCtrl = new TabPage(dataSource.ToString(TAB_TEXT_MAX_LENGTH));
                pageCtrl.ToolTipText = dataSource.ToString();

                // Create and add the log control to the tab page
                LogTab logCtrl = new LogTab();
                logCtrl.ShowMilliseconds = Config.Current.ShowMilliseconds;
                pageCtrl.Controls.Add(logCtrl);
                logCtrl.Dock = DockStyle.Fill;

                // Add the tab to the tab page
                tcLogs.SelectedTab = pageCtrl;

                // Add the tab page
                tcLogs.TabPages.Add(pageCtrl);

                logCtrl.LoadSucceeded += LogTab_LoadSucceeded;
                logCtrl.LoadFailed += LogTab_LoadFailed;

                // Loads asynchronously
                logCtrl.LoadSource(dataSource);
                Application.DoEvents();
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
            bool hasRecentItems = false;
            var invalidSources = new List<RecentLog>();
            try
            {
                // Remove entries from the main menu and the toolbar button menu
                for (int i = tsmiOpenRecent.DropDownItems.Count - 1; i >= 0; i--)
                {
                    if (tsmiOpenRecent.DropDownItems[i] is RecentToolStripMenuItem)
                        tsmiOpenRecent.DropDownItems.RemoveAt(i);
                }
                for (int i = tsbLogOpen.DropDownItems.Count - 1; i >= 0; i--)
                {
                    if (tsbLogOpen.DropDownItems[i] is RecentToolStripMenuItem)
                        tsbLogOpen.DropDownItems.RemoveAt(i);
                }

                // Builds the recent entries
                int mainMenuIndex = 0;
                foreach (RecentLog recent in Config.Current.RecentLogs)
                {
                    try
                    {
                        LogDataSource source = LogDataSourceFactory.Create(recent.Type, recent.ConnectionString);

                        if (source.Check())
                        {
                            hasRecentItems = true;

                            // Add at the beginning of the main menu
                            tsmiOpenRecent.DropDownItems.Insert(
                                mainMenuIndex++,
                                new RecentToolStripMenuItem(
                                    recent,
                                    source.ToString(RECENT_TEXT_MAX_LENGTH),
                                    source.Icon,
                                    TsmiFileRecentItemClick));

                            // Add at the end of the toolbar button menu
                            tsbLogOpen.DropDownItems.Add(
                                new RecentToolStripMenuItem(
                                    recent,
                                    source.ToString(RECENT_TEXT_MAX_LENGTH),
                                    source.Icon,
                                    TsmiFileRecentItemClick));
                        }
                        else
                            invalidSources.Add(recent);
                    }
                    catch
                    {
                        invalidSources.Add(recent);
                    }
                }

                // Remove the invalid sources and save the config if invalid sources were found
                if (invalidSources.Count > 0)
                {
                    foreach (RecentLog invalidRecent in invalidSources)
                    {
                        Config.Current.RecentLogs.Remove(invalidRecent);
                    }
                    Config.Save();
                }

                // The visibility of the separators and sub menus
                tsmiOpenRecent.Visible = hasRecentItems;
                tsbLogOpenSep.Visible = hasRecentItems;
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
        /// Checks the already opened. If yes, select it.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>true</c> if file has already be opened; otherwise <c>false</c>.</returns>
        private bool CheckAlreadyOpened(string fileName)
        {
            for (int pageIndex = 0; pageIndex < tcLogs.TabCount; pageIndex++)
            {
                TabPage tab = tcLogs.TabPages[pageIndex];
                var logTab = tab.Controls[0] as LogTab;
                if (logTab != null)
                {
                    if (logTab.LogSource.ConnectionString == fileName)
                    {
                        tcLogs.SelectTab(tab);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Closes the current log.
        /// </summary>
        private void CloseLog()
        {
            try
            {
                LogTab tab = CurrentPageLogControl;
                if (tab != null)
                    tab.CloseSource();

                TabPage page = CurrentPage;
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
        /// Handles the Opening event of the cmsTab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
        private void CmsTabOpening(object sender, CancelEventArgs e)
        {
            if (cmsTab.Items.Count == 0)
                e.Cancel = true;
        }

        /// <summary>
        /// Applies the filter for the current log tab.
        /// </summary>
        private void CurrentApplyFilter()
        {
            try
            {
                LogTab current = CurrentPageLogControl;
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
        /// Goes to a specified event for the current log tab.
        /// </summary>
        private void CurrentGoTo()
        {
            try
            {
                LogTab current = CurrentPageLogControl;
                if (current != null)
                {
                    bool oldPaused = current.IsPaused;
                    try
                    {
                        using (var form = new GoToForm())
                        {
                            current.IsPaused = true;
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                if (!current.GoToEvent(form.SelectedEventNumber))
                                {
                                    MessageBox.Show(
                                        this,
                                        String.Format(Resources.Msg_GoToEventNotFound, form.SelectedEventNumber),
                                        Resources.Msg_GoToEventNotFoundTitle,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                                }
                            }
                            else
                                current.IsPaused = oldPaused;
                        }
                    }
                    catch
                    {
                        current.IsPaused = oldPaused;
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToGoToSpecificEvent, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Applies the filter for the current log tab.
        /// </summary>
        private void CurrentRefresh()
        {
            try
            {
                LogTab current = CurrentPageLogControl;
                if (current != null)
                    current.RefreshLogEntries();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToRefresh, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                LogTab current = CurrentPageLogControl;
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
        /// Toggles the "follow last log" function for the current log tab.
        /// </summary>
        private void CurrentToggleFollowLastLog()
        {
            try
            {
                LogTab current = CurrentPageLogControl;
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
            try
            {
                LogTab current = CurrentPageLogControl;
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
        /// Edits the settings.
        /// </summary>
        private void EditSettings()
        {
            try
            {
                var settingsForm = new SettingsForm();
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    foreach (TabPage page in tcLogs.TabPages)
                    {
                        var logTab = page.Controls[0] as LogTab;
                        if (logTab != null)
                            logTab.ShowMilliseconds = Config.Current.ShowMilliseconds;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToEditSettings, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateUI();
                Invalidate(true);
            }
        }

        /// <summary>
        /// Loads the favorites menu.
        /// </summary>
        private void LoadFavoritesMenu()
        {
            try
            {
                AutoFileMenuHelper.PopulateMenuAsync(tsmiOpenfavorite, TsmiFileFavoriteItemClick);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles the LoadFailed event of the LogTab controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void LogTab_LoadFailed(object sender, EventArgs e)
        {
            var tab = sender as LogTab;

            if (tab != null)
            {
                // Removes the tab that contains the control
                for (int i = tcLogs.TabCount - 1; i >= 0; i--)
                {
                    if (tcLogs.TabPages[i].Contains(tab))
                    {
                        tcLogs.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the LoadSucceeded event of the LogTab controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void LogTab_LoadSucceeded(object sender, EventArgs e)
        {
            var tab = sender as LogTab;

            if (tab != null && tab.LogSource != null)
            {
                try
                {
                    // Updates the history
                    for (int i = Config.Current.RecentLogs.Count - 1; i >= 0; i--)
                    {
                        if (Config.Current.RecentLogs[i].Type == tab.LogSource.Name && Config.Current.RecentLogs[i].ConnectionString == tab.LogSource.ConnectionString)
                        {
                            Config.Current.RecentLogs.RemoveAt(i);
                            break;
                        }
                    }
                    Config.Current.RecentLogs.Insert(0, new RecentLog(tab.LogSource.ConnectionString, tab.LogSource.Name));
                    if (Config.Current.RecentLogs.Count > MAX_RECENT)
                        Config.Current.RecentLogs.RemoveRange(MAX_RECENT, Config.Current.RecentLogs.Count - MAX_RECENT);

                    // Saves the configuration
                    Config.Save();

                    // Rebuilds the recent menu
                    BuildRecentMenu();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Handles the Shown event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Registers the open file callback
            OpenToMainInstance.RegisterOpenCallback(OpenLogFromFile);
        }

        /// <summary>
        /// Opens an XML log file.
        /// </summary>
        /// <param name="fileName">
        /// The optional file name to open; <c>null</c> or <see cref="String.Empty" /> to display the file
        /// open dialog.
        /// </param>
        private void OpenLogFromFile(string fileName)
        {
            if (InvokeRequired)
                Invoke(new Action<string>(OpenLogFromFile), fileName);
            else
            {
                try
                {
                    if (String.IsNullOrEmpty(fileName))
                    {
                        if (ofdXmlLogFile.ShowDialog(this) != DialogResult.OK)
                            return;
                        fileName = ofdXmlLogFile.FileName;
                    }

                    if (!CheckAlreadyOpened(fileName))
                        AddLogTab(new FileLogDataSource(fileName));
                    Activate();
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
        }

        /// <summary>
        /// Opens the log from telnet.
        /// </summary>
        private void OpenLogFromTelnet()
        {
            try
            {
                OpenTelnetDialog dlg = new OpenTelnetDialog();

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
        /// Opens the recent.
        /// </summary>
        /// <param name="recentEntry">The recent entry that indicates the log to open.</param>
        private void OpenRecent(RecentLog recentEntry)
        {
            try
            {
                LogDataSource source = LogDataSourceFactory.Create(recentEntry.Type, recentEntry.ConnectionString);

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

        private void TcLogsMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int x = 0; x < tcLogs.TabCount; x++)
                {
                    Rectangle rt = tcLogs.GetTabRect(x);
                    if (e.X > rt.Left && e.X < rt.Right && e.Y > rt.Top && e.Y < rt.Bottom)
                    {
                        if (tcLogs.TabPages[x].Controls.Count > 0)
                        {
                            cmsTab.Tag = tcLogs.TabPages[x].Controls[0];
                            CreateContextMenu(tcLogs.TabPages[x].Controls[0] as LogTab);
                        }
                        else
                            cmsTab.Tag = null;
                        cmsTab.Show(tcLogs, e.Location);
                    }
                }
            }
        }


        private void CreateContextMenu(LogTab logTab)
        {
            FileLogDataSource fileLog = null;
            if (logTab != null)
                fileLog = logTab.LogSource as FileLogDataSource;

            cmsTab.Items.Clear();
            if (fileLog != null && fileLog.LogFileHistoryThread.Any())
            {
                foreach (string fileName in fileLog.LogFileHistoryThread)
                {
                    var file = new FileInfo(fileName);
                    var subMenu = new ToolStripMenuItem(
                        String.Format("{0} ({1:dd.MM.yyyy HH:mm:ss})", file.Name, file.LastWriteTime),
                        null,
                        TsmiAllHistoryLogsItemClick
                        );
                    subMenu.Enabled = (fileLog.ConnectionString != fileName);
                    subMenu.Tag = fileName;
                    cmsTab.Items.Add(subMenu);
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the tcLogs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TcLogsSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of the tsbFilterApply control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsbFilterApplyClick(object sender, EventArgs e)
        {
            CurrentApplyFilter();
        }

        /// <summary>
        /// Handles the Click event of the tsbFilterReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsbFilterResetClick(object sender, EventArgs e)
        {
            CurrentResetFilter();
        }

        /// <summary>
        /// Handles the Click event of the tsbGoTo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsbGoToClick(object sender, EventArgs e)
        {
            CurrentGoTo();
        }

        /// <summary>
        /// Handles the Click event of the tsbLogClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsbLogCloseClick(object sender, EventArgs e)
        {
            CloseLog();
        }

        /// <summary>
        /// Handles the Click event of the tsbLogOpenFromFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsbLogOpenFromFileClick(object sender, EventArgs e)
        {
            OpenLogFromFile(null);
        }

        /// <summary>
        /// Handles the Click event of the tsbLogOpenFromTelnet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsbLogOpenFromTelnetClick(object sender, EventArgs e)
        {
            OpenLogFromTelnet();
        }

        /// <summary>
        /// Handles the ButtonClick event of the tsbLogOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsbLogOpenButtonClick(object sender, EventArgs e)
        {
            tsbLogOpen.ShowDropDown();
        }

        /// <summary>
        /// Handles the Click event of the tsbModeFollow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsbModeFollowClick(object sender, EventArgs e)
        {
            CurrentToggleFollowLastLog();
        }

        /// <summary>
        /// Handles the Click event of the tsbModePaused control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsbModePausedClick(object sender, EventArgs e)
        {
            CurrentTogglePauseCapture();
        }

        /// <summary>
        /// Handles the Click event of the tsbRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void TsbRefreshClick(object sender, EventArgs e)
        {
            CurrentRefresh();
        }

        /// <summary>
        /// Handles the Click event of the tsmiAllHistoryLogsItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiAllHistoryLogsItemClick(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;

            // Obtains the RecentLog instance from the item
            if (item != null && item.Tag != null)
                OpenLogFromFile(item.Tag.ToString());
        }

        /// <summary>
        /// Handles the Click event of the tsmiEditApplyFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiEditApplyFilterClick(object sender, EventArgs e)
        {
            CurrentApplyFilter();
        }

        /// <summary>
        /// Handles the Click event of the tsmiEditFollowLastLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiEditFollowLastLogClick(object sender, EventArgs e)
        {
            CurrentToggleFollowLastLog();
        }

        /// <summary>
        /// Handles the Click event of the tsmiEditGoTo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiEditGoToClick(object sender, EventArgs e)
        {
            CurrentGoTo();
        }

        /// <summary>
        /// Handles the Click event of the tsmiEditPaused control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiEditPausedClick(object sender, EventArgs e)
        {
            CurrentTogglePauseCapture();
        }

        /// <summary>
        /// Handles the Click event of the tsmiEditRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiEditRefreshClick(object sender, EventArgs e)
        {
            CurrentRefresh();
        }

        /// <summary>
        /// Handles the Click event of the tsmiEditResetFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiEditResetFilterClick(object sender, EventArgs e)
        {
            CurrentResetFilter();
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileCloseLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiFileCloseLogClick(object sender, EventArgs e)
        {
            CloseLog();
        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiFileExitClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileFavoriteItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiFileFavoriteItemClick(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;

            // Obtains the RecentLog instance from the item
            if (item != null && item.Tag is FileSystemInfo)
                OpenLogFromFile(((FileSystemInfo)item.Tag).FullName);
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileOpenLogFromFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiFileOpenLogFromFileClick(object sender, EventArgs e)
        {
            OpenLogFromFile(null);
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileOpenLogFromTelnet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiFileOpenLogFromTelnetClick(object sender, EventArgs e)
        {
            OpenLogFromTelnet();
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileRecentItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiFileRecentItemClick(object sender, EventArgs e)
        {
            var item = sender as RecentToolStripMenuItem;

            // Obtains the RecentLog instance from the item
            if (item != null && item.RecentInfo != null)
                OpenRecent(item.RecentInfo);
        }

        /// <summary>
        /// Handles the Click event of the tsmiFileSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiFileSettingsClick(object sender, EventArgs e)
        {
            EditSettings();
        }

        /// <summary>
        /// Handles the Click event of the tsmiOpenRecentClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TsmiOpenRecentClearClick(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, Resources.Msg_ClearRecentConfirm, Resources.Msg_ClearRecentConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Config.Current.RecentLogs.Clear();
                    Config.Save();
                    BuildRecentMenu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToClearRecent, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates the UI controls so that they reflect the current state.
        /// </summary>
        private void UpdateUI()
        {
            try
            {
                LogTab current = CurrentPageLogControl;

                // Menus
                tsmiFileCloseLog.Enabled = current != null;
                tsmiEdit.Visible = current != null;
                tsmiEditRefresh.Enabled = current != null && current.CanRefreshLogEntries;
                //tsmiEditGoTo.Enabled = current != null;
                //tsmiEditApplyFilter.Enabled = current != null;
                //tsmiEditResetFilter.Enabled = current != null;
                //tsmiEditPaused.Enabled = current != null;
                //tsmiEditFollowLastLog.Enabled = current != null;
                tsmiEditPaused.Checked = current != null && current.IsPaused;
                tsmiEditFollowLastLog.Checked = current != null && current.FollowLastLog;

                // Toolbar                
                tsbLogClose.Enabled = current != null;
                tsbRefresh.Enabled = current != null && current.CanRefreshLogEntries;
                tsbGoTo.Enabled = current != null;
                tsbModeFollow.Enabled = current != null;
                tsbModeFollow.Checked = current != null && current.FollowLastLog;
                tsbModePaused.Enabled = current != null;
                tsbModePaused.Checked = current != null && current.IsPaused;
                tsbFilterApply.Enabled = current != null;
                tsbFilterReset.Enabled = current != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Resources.Err_FailedToUpdateControlsState, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Nested Classes

        #region RecentToolStripMenuItem

        /// <summary>
        /// Represents a recent tool strip menu item.
        /// </summary>
        private class RecentToolStripMenuItem : ToolStripMenuItem
        {
            #region Private Fields

            private RecentLog _recentInfo;

            #endregion

            #region Public Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="RecentToolStripMenuItem" /> class.
            /// </summary>
            /// <param name="recentInfo">
            /// The <see cref="RecentLog" /> instance that contains the informations about the recent log
            /// source.
            /// </param>
            /// <param name="text">The text to display on the menu item..</param>
            /// <param name="image">The <see cref="Image" /> to display on the control.</param>
            /// <param name="clickEvent">
            /// An event handler that raises the <see cref="Control.Click" /> event when the control is
            /// clicked.
            /// </param>
            public RecentToolStripMenuItem(RecentLog recentInfo, string text, Image image, EventHandler clickEvent)
                : base(text, image, clickEvent)
            {
                if (recentInfo == null)
                    throw new ArgumentNullException("recentInfo");

                ToolTipText = recentInfo.ToString();
                _recentInfo = recentInfo;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets or sets the recent info.
            /// </summary>
            /// <value>The <see cref="RecentLog" /> instance that contains the informations about the recent log source.</value>
            public RecentLog RecentInfo
            {
                get { return _recentInfo; }
            }

            #endregion
        }

        #endregion

        #endregion
    }
}
