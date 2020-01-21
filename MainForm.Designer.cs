namespace Log4NetViewer
{
    partial class MainForm
    {
		#region Private Members 

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripSeparator tssSep3;
        private System.Windows.Forms.ToolStripButton tsbFilterApply;
        private System.Windows.Forms.ToolStripButton tsbFilterReset;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileCloseLog;
        private System.Windows.Forms.ToolStripSeparator tsmiFileSep1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileExit;
        private System.Windows.Forms.TabControl tcLogs;
        private System.Windows.Forms.OpenFileDialog ofdXmlLogFile;
        private System.Windows.Forms.ToolStripButton tsbModePaused;
        private System.Windows.Forms.ToolStripButton tsbModeFollow;
        private System.Windows.Forms.ToolStripSeparator tssSep2;
        private System.Windows.Forms.ToolStripButton tsbLogClose;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSettings;
        private System.Windows.Forms.ToolStripSeparator tsmiFileSep2;
        private System.Windows.Forms.ToolStripSplitButton tsbLogOpen;
        private System.Windows.Forms.ToolStripMenuItem tsbLogOpenFromFile;
        private System.Windows.Forms.ToolStripMenuItem tsbLogOpenFromTelnet;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenRecent;
        private System.Windows.Forms.ToolStripButton tsbRefresh;

		#endregion Private Members 

		#region Protected Methods 

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#endregion Protected Methods 

		#region Private Methods 

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbLogOpen = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbLogOpenFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbLogOpenFromTelnet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbLogOpenSep = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLogClose = new System.Windows.Forms.ToolStripButton();
            this.tssSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbGoTo = new System.Windows.Forms.ToolStripButton();
            this.tssSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbModePaused = new System.Windows.Forms.ToolStripButton();
            this.tsbModeFollow = new System.Windows.Forms.ToolStripButton();
            this.tssSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFilterApply = new System.Windows.Forms.ToolStripButton();
            this.tsbFilterReset = new System.Windows.Forms.ToolStripButton();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileOpenLogFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileOpenLogFromTelnet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenRecentSep = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpenRecentClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenfavorite = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileCloseLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditGoTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEditApplyFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditResetFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEditPaused = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditFollowLastLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tcLogs = new System.Windows.Forms.TabControl();
            this.ofdXmlLogFile = new System.Windows.Forms.OpenFileDialog();
            this.cmsTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLogOpen,
            this.tsbLogClose,
            this.tssSep1,
            this.tsbRefresh,
            this.tsbGoTo,
            this.tssSep2,
            this.tsbModePaused,
            this.tsbModeFollow,
            this.tssSep3,
            this.tsbFilterApply,
            this.tsbFilterReset});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsMain.Size = new System.Drawing.Size(956, 25);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "tsMain";
            // 
            // tsbLogOpen
            // 
            this.tsbLogOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLogOpenFromFile,
            this.tsbLogOpenFromTelnet,
            this.tsbLogOpenSep});
            this.tsbLogOpen.Image = global::Triamun.Log4NetViewer.Properties.Resources.OpenLog;
            this.tsbLogOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLogOpen.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.tsbLogOpen.MergeIndex = 3;
            this.tsbLogOpen.Name = "tsbLogOpen";
            this.tsbLogOpen.Size = new System.Drawing.Size(88, 22);
            this.tsbLogOpen.Text = "Open log";
            this.tsbLogOpen.ButtonClick += new System.EventHandler(this.TsbLogOpenButtonClick);
            // 
            // tsbLogOpenFromFile
            // 
            this.tsbLogOpenFromFile.Image = global::Triamun.Log4NetViewer.Properties.Resources.LogSourceFile;
            this.tsbLogOpenFromFile.Name = "tsbLogOpenFromFile";
            this.tsbLogOpenFromFile.Size = new System.Drawing.Size(107, 22);
            this.tsbLogOpenFromFile.Text = "&File";
            this.tsbLogOpenFromFile.ToolTipText = "Opens a log file.";
            this.tsbLogOpenFromFile.Click += new System.EventHandler(this.TsbLogOpenFromFileClick);
            // 
            // tsbLogOpenFromTelnet
            // 
            this.tsbLogOpenFromTelnet.Image = global::Triamun.Log4NetViewer.Properties.Resources.LogSourceTelnet;
            this.tsbLogOpenFromTelnet.Name = "tsbLogOpenFromTelnet";
            this.tsbLogOpenFromTelnet.Size = new System.Drawing.Size(107, 22);
            this.tsbLogOpenFromTelnet.Text = "&Telnet";
            this.tsbLogOpenFromTelnet.ToolTipText = "Connects to a telnet log provider.";
            this.tsbLogOpenFromTelnet.Click += new System.EventHandler(this.TsbLogOpenFromTelnetClick);
            // 
            // tsbLogOpenSep
            // 
            this.tsbLogOpenSep.Name = "tsbLogOpenSep";
            this.tsbLogOpenSep.Size = new System.Drawing.Size(104, 6);
            // 
            // tsbLogClose
            // 
            this.tsbLogClose.Image = global::Triamun.Log4NetViewer.Properties.Resources.CloseLog;
            this.tsbLogClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLogClose.Name = "tsbLogClose";
            this.tsbLogClose.Size = new System.Drawing.Size(76, 22);
            this.tsbLogClose.Text = "Close log";
            this.tsbLogClose.ToolTipText = "Close log (Ctrl+W)";
            this.tsbLogClose.Click += new System.EventHandler(this.TsbLogCloseClick);
            // 
            // tssSep1
            // 
            this.tssSep1.Name = "tssSep1";
            this.tssSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::Triamun.Log4NetViewer.Properties.Resources.Refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(70, 22);
            this.tsbRefresh.Text = "Rrefresh";
            this.tsbRefresh.ToolTipText = "Refresh the log (F5)";
            this.tsbRefresh.Click += new System.EventHandler(this.TsbRefreshClick);
            // 
            // tsbGoTo
            // 
            this.tsbGoTo.Image = global::Triamun.Log4NetViewer.Properties.Resources.GoToLog;
            this.tsbGoTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGoTo.Name = "tsbGoTo";
            this.tsbGoTo.Size = new System.Drawing.Size(56, 22);
            this.tsbGoTo.Text = "Go to";
            this.tsbGoTo.ToolTipText = "Goes to a specific event number (Ctrl+G)";
            this.tsbGoTo.Click += new System.EventHandler(this.TsbGoToClick);
            // 
            // tssSep2
            // 
            this.tssSep2.Name = "tssSep2";
            this.tssSep2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbModePaused
            // 
            this.tsbModePaused.Image = global::Triamun.Log4NetViewer.Properties.Resources.PauseCapture;
            this.tsbModePaused.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModePaused.Name = "tsbModePaused";
            this.tsbModePaused.Size = new System.Drawing.Size(65, 22);
            this.tsbModePaused.Text = "Paused";
            this.tsbModePaused.ToolTipText = "Pause log acquisition (Ctrl+P)";
            this.tsbModePaused.Click += new System.EventHandler(this.TsbModePausedClick);
            // 
            // tsbModeFollow
            // 
            this.tsbModeFollow.Image = global::Triamun.Log4NetViewer.Properties.Resources.FollowLastLog;
            this.tsbModeFollow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModeFollow.Name = "tsbModeFollow";
            this.tsbModeFollow.Size = new System.Drawing.Size(62, 22);
            this.tsbModeFollow.Text = "Follow";
            this.tsbModeFollow.ToolTipText = "Follow the last log (Ctrl+F)";
            this.tsbModeFollow.Click += new System.EventHandler(this.TsbModeFollowClick);
            // 
            // tssSep3
            // 
            this.tssSep3.Name = "tssSep3";
            this.tssSep3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFilterApply
            // 
            this.tsbFilterApply.Image = global::Triamun.Log4NetViewer.Properties.Resources.FilterApply;
            this.tsbFilterApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilterApply.Name = "tsbFilterApply";
            this.tsbFilterApply.Size = new System.Drawing.Size(85, 22);
            this.tsbFilterApply.Text = "Apply filter";
            this.tsbFilterApply.ToolTipText = "Apply the current filter (F7)";
            this.tsbFilterApply.Click += new System.EventHandler(this.TsbFilterApplyClick);
            // 
            // tsbFilterReset
            // 
            this.tsbFilterReset.Image = global::Triamun.Log4NetViewer.Properties.Resources.FilterErase;
            this.tsbFilterReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilterReset.Name = "tsbFilterReset";
            this.tsbFilterReset.Size = new System.Drawing.Size(82, 22);
            this.tsbFilterReset.Text = "Reset filter";
            this.tsbFilterReset.ToolTipText = "Reset the current filter (F8)";
            this.tsbFilterReset.Click += new System.EventHandler(this.TsbFilterResetClick);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiEdit});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.msMain.Size = new System.Drawing.Size(956, 24);
            this.msMain.TabIndex = 1;
            this.msMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileOpen,
            this.tsmiOpenRecent,
            this.tsmiOpenfavorite,
            this.tsmiFileCloseLog,
            this.tsmiFileSep1,
            this.tsmiFileSettings,
            this.tsmiFileSep2,
            this.tsmiFileExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "&File";
            // 
            // tsmiFileOpen
            // 
            this.tsmiFileOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileOpenLogFromFile,
            this.tsmiFileOpenLogFromTelnet});
            this.tsmiFileOpen.Name = "tsmiFileOpen";
            this.tsmiFileOpen.Size = new System.Drawing.Size(168, 22);
            this.tsmiFileOpen.Text = "&Open log";
            // 
            // tsmiFileOpenLogFromFile
            // 
            this.tsmiFileOpenLogFromFile.Image = global::Triamun.Log4NetViewer.Properties.Resources.LogSourceFile;
            this.tsmiFileOpenLogFromFile.Name = "tsmiFileOpenLogFromFile";
            this.tsmiFileOpenLogFromFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiFileOpenLogFromFile.Size = new System.Drawing.Size(182, 22);
            this.tsmiFileOpenLogFromFile.Text = "&File";
            this.tsmiFileOpenLogFromFile.ToolTipText = "Opens a log file.";
            this.tsmiFileOpenLogFromFile.Click += new System.EventHandler(this.TsmiFileOpenLogFromFileClick);
            // 
            // tsmiFileOpenLogFromTelnet
            // 
            this.tsmiFileOpenLogFromTelnet.Image = global::Triamun.Log4NetViewer.Properties.Resources.LogSourceTelnet;
            this.tsmiFileOpenLogFromTelnet.Name = "tsmiFileOpenLogFromTelnet";
            this.tsmiFileOpenLogFromTelnet.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.tsmiFileOpenLogFromTelnet.Size = new System.Drawing.Size(182, 22);
            this.tsmiFileOpenLogFromTelnet.Text = "&Telnet";
            this.tsmiFileOpenLogFromTelnet.ToolTipText = "Connects to a telnet log provider.";
            this.tsmiFileOpenLogFromTelnet.Click += new System.EventHandler(this.TsmiFileOpenLogFromTelnetClick);
            // 
            // tsmiOpenRecent
            // 
            this.tsmiOpenRecent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenRecentSep,
            this.tsmiOpenRecentClear});
            this.tsmiOpenRecent.Name = "tsmiOpenRecent";
            this.tsmiOpenRecent.Size = new System.Drawing.Size(168, 22);
            this.tsmiOpenRecent.Text = "Open &Recent";
            // 
            // tsmiOpenRecentSep
            // 
            this.tsmiOpenRecentSep.Name = "tsmiOpenRecentSep";
            this.tsmiOpenRecentSep.Size = new System.Drawing.Size(113, 6);
            // 
            // tsmiOpenRecentClear
            // 
            this.tsmiOpenRecentClear.Image = global::Triamun.Log4NetViewer.Properties.Resources.CloseLog;
            this.tsmiOpenRecentClear.Name = "tsmiOpenRecentClear";
            this.tsmiOpenRecentClear.Size = new System.Drawing.Size(116, 22);
            this.tsmiOpenRecentClear.Text = "Clear all";
            this.tsmiOpenRecentClear.ToolTipText = "Removes all the recent entries";
            this.tsmiOpenRecentClear.Click += new System.EventHandler(this.TsmiOpenRecentClearClick);
            // 
            // tsmiOpenfavorite
            // 
            this.tsmiOpenfavorite.Name = "tsmiOpenfavorite";
            this.tsmiOpenfavorite.Size = new System.Drawing.Size(168, 22);
            this.tsmiOpenfavorite.Text = "Open F&avorite";
            // 
            // tsmiFileCloseLog
            // 
            this.tsmiFileCloseLog.Image = global::Triamun.Log4NetViewer.Properties.Resources.CloseLog;
            this.tsmiFileCloseLog.Name = "tsmiFileCloseLog";
            this.tsmiFileCloseLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tsmiFileCloseLog.Size = new System.Drawing.Size(168, 22);
            this.tsmiFileCloseLog.Text = "&Close log";
            this.tsmiFileCloseLog.Click += new System.EventHandler(this.TsmiFileCloseLogClick);
            // 
            // tsmiFileSep1
            // 
            this.tsmiFileSep1.Name = "tsmiFileSep1";
            this.tsmiFileSep1.Size = new System.Drawing.Size(165, 6);
            // 
            // tsmiFileSettings
            // 
            this.tsmiFileSettings.Image = global::Triamun.Log4NetViewer.Properties.Resources.EditConfig;
            this.tsmiFileSettings.Name = "tsmiFileSettings";
            this.tsmiFileSettings.Size = new System.Drawing.Size(168, 22);
            this.tsmiFileSettings.Text = "&Settings";
            this.tsmiFileSettings.Click += new System.EventHandler(this.TsmiFileSettingsClick);
            // 
            // tsmiFileSep2
            // 
            this.tsmiFileSep2.Name = "tsmiFileSep2";
            this.tsmiFileSep2.Size = new System.Drawing.Size(165, 6);
            // 
            // tsmiFileExit
            // 
            this.tsmiFileExit.Name = "tsmiFileExit";
            this.tsmiFileExit.Size = new System.Drawing.Size(168, 22);
            this.tsmiFileExit.Text = "E&xit";
            this.tsmiFileExit.Click += new System.EventHandler(this.TsmiFileExitClick);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditRefresh,
            this.tsmiEditGoTo,
            this.tsmiEditSep1,
            this.tsmiEditApplyFilter,
            this.tsmiEditResetFilter,
            this.tsmiEditSep2,
            this.tsmiEditPaused,
            this.tsmiEditFollowLastLog});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(39, 20);
            this.tsmiEdit.Text = "&Edit";
            // 
            // tsmiEditRefresh
            // 
            this.tsmiEditRefresh.Image = global::Triamun.Log4NetViewer.Properties.Resources.Refresh;
            this.tsmiEditRefresh.Name = "tsmiEditRefresh";
            this.tsmiEditRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiEditRefresh.Size = new System.Drawing.Size(190, 22);
            this.tsmiEditRefresh.Text = "&Refresh";
            this.tsmiEditRefresh.ToolTipText = "Check for and load new logs";
            this.tsmiEditRefresh.Click += new System.EventHandler(this.TsmiEditRefreshClick);
            // 
            // tsmiEditGoTo
            // 
            this.tsmiEditGoTo.Image = global::Triamun.Log4NetViewer.Properties.Resources.GoToLog;
            this.tsmiEditGoTo.Name = "tsmiEditGoTo";
            this.tsmiEditGoTo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.tsmiEditGoTo.Size = new System.Drawing.Size(190, 22);
            this.tsmiEditGoTo.Text = "&Go to";
            this.tsmiEditGoTo.Click += new System.EventHandler(this.TsmiEditGoToClick);
            // 
            // tsmiEditSep1
            // 
            this.tsmiEditSep1.Name = "tsmiEditSep1";
            this.tsmiEditSep1.Size = new System.Drawing.Size(187, 6);
            // 
            // tsmiEditApplyFilter
            // 
            this.tsmiEditApplyFilter.Image = global::Triamun.Log4NetViewer.Properties.Resources.FilterApply;
            this.tsmiEditApplyFilter.Name = "tsmiEditApplyFilter";
            this.tsmiEditApplyFilter.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsmiEditApplyFilter.Size = new System.Drawing.Size(190, 22);
            this.tsmiEditApplyFilter.Text = "&Apply filter";
            this.tsmiEditApplyFilter.Click += new System.EventHandler(this.TsmiEditApplyFilterClick);
            // 
            // tsmiEditResetFilter
            // 
            this.tsmiEditResetFilter.Image = global::Triamun.Log4NetViewer.Properties.Resources.FilterErase;
            this.tsmiEditResetFilter.Name = "tsmiEditResetFilter";
            this.tsmiEditResetFilter.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.tsmiEditResetFilter.Size = new System.Drawing.Size(190, 22);
            this.tsmiEditResetFilter.Text = "&Reset filter";
            this.tsmiEditResetFilter.Click += new System.EventHandler(this.TsmiEditResetFilterClick);
            // 
            // tsmiEditSep2
            // 
            this.tsmiEditSep2.Name = "tsmiEditSep2";
            this.tsmiEditSep2.Size = new System.Drawing.Size(187, 6);
            // 
            // tsmiEditPaused
            // 
            this.tsmiEditPaused.Image = global::Triamun.Log4NetViewer.Properties.Resources.PauseCapture;
            this.tsmiEditPaused.Name = "tsmiEditPaused";
            this.tsmiEditPaused.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsmiEditPaused.Size = new System.Drawing.Size(190, 22);
            this.tsmiEditPaused.Text = "&Paused";
            this.tsmiEditPaused.Click += new System.EventHandler(this.TsmiEditPausedClick);
            // 
            // tsmiEditFollowLastLog
            // 
            this.tsmiEditFollowLastLog.Image = global::Triamun.Log4NetViewer.Properties.Resources.FollowLastLog;
            this.tsmiEditFollowLastLog.Name = "tsmiEditFollowLastLog";
            this.tsmiEditFollowLastLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.tsmiEditFollowLastLog.Size = new System.Drawing.Size(190, 22);
            this.tsmiEditFollowLastLog.Text = "&Follow last log";
            this.tsmiEditFollowLastLog.Click += new System.EventHandler(this.TsmiEditFollowLastLogClick);
            // 
            // tcLogs
            // 
            this.tcLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcLogs.Location = new System.Drawing.Point(0, 49);
            this.tcLogs.Name = "tcLogs";
            this.tcLogs.SelectedIndex = 0;
            this.tcLogs.ShowToolTips = true;
            this.tcLogs.Size = new System.Drawing.Size(956, 481);
            this.tcLogs.TabIndex = 2;
            this.tcLogs.SelectedIndexChanged += new System.EventHandler(this.TcLogsSelectedIndexChanged);
            this.tcLogs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TcLogsMouseUp);
            // 
            // ofdXmlLogFile
            // 
            this.ofdXmlLogFile.DefaultExt = "log4net";
            this.ofdXmlLogFile.Filter = resources.GetString("ofdXmlLogFile.Filter");
            this.ofdXmlLogFile.Title = "Select the XML log file to open";
            // 
            // cmsTab
            // 
            this.cmsTab.Name = "cmsTab";
            this.cmsTab.Size = new System.Drawing.Size(153, 26);
            this.cmsTab.Opening += new System.ComponentModel.CancelEventHandler(this.CmsTabOpening);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 530);
            this.Controls.Add(this.tcLogs);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "MainForm";
            this.Text = "Log4Net viewer";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion Private Methods 

        private System.Windows.Forms.ToolStripSeparator tsmiOpenRecentSep;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenRecentClear;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditGoTo;
        private System.Windows.Forms.ToolStripSeparator tsmiEditSep1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditApplyFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditResetFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditPaused;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditFollowLastLog;
        private System.Windows.Forms.ToolStripSeparator tsmiEditSep2;
        private System.Windows.Forms.ToolStripButton tsbGoTo;
        private System.Windows.Forms.ToolStripSeparator tssSep1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileOpenLogFromFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileOpenLogFromTelnet;
        private System.Windows.Forms.ToolStripSeparator tsbLogOpenSep;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenfavorite;
        private System.Windows.Forms.ContextMenuStrip cmsTab;
    }
}

