namespace HciSolutions.LogSpotter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbLogOpen = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbLogOpenFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbLogOpenFromTelnet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbLogClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbModePaused = new System.Windows.Forms.ToolStripButton();
            this.tsbModeFollow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFilterApply = new System.Windows.Forms.ToolStripButton();
            this.tsbFilterReset = new System.Windows.Forms.ToolStripButton();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileOpenLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileOpenLogFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileOpenLogFromTelnet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDatabaseSqlServer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileCloseLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModePaused = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModeFollow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFilterApply = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFilterReset = new System.Windows.Forms.ToolStripMenuItem();
            this.tcLogs = new System.Windows.Forms.TabControl();
            this.ofdXmlLogFile = new System.Windows.Forms.OpenFileDialog();
            this.tsbLogOpenFromSqlServerDatabase = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripSeparator2,
            this.tsbModePaused,
            this.tsbModeFollow,
            this.toolStripSeparator1,
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
            this.tsbLogOpenFromSqlServerDatabase});
            this.tsbLogOpen.Image = global::HciSolutions.LogSpotter.Properties.Resources.OpenLog;
            this.tsbLogOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLogOpen.Name = "tsbLogOpen";
            this.tsbLogOpen.Size = new System.Drawing.Size(88, 22);
            this.tsbLogOpen.Text = "Open log";
            this.tsbLogOpen.ButtonClick += new System.EventHandler(this.tsbLogOpen_ButtonClick);
            // 
            // tsbLogOpenFromFile
            // 
            this.tsbLogOpenFromFile.Image = global::HciSolutions.LogSpotter.Properties.Resources.LogSourceFile;
            this.tsbLogOpenFromFile.Name = "tsbLogOpenFromFile";
            this.tsbLogOpenFromFile.Size = new System.Drawing.Size(152, 22);
            this.tsbLogOpenFromFile.Text = "&File";
            this.tsbLogOpenFromFile.Click += new System.EventHandler(this.tsbLogOpenFromFile_Click);
            // 
            // tsbLogOpenFromTelnet
            // 
            this.tsbLogOpenFromTelnet.Image = global::HciSolutions.LogSpotter.Properties.Resources.LogSourceTelnet;
            this.tsbLogOpenFromTelnet.Name = "tsbLogOpenFromTelnet";
            this.tsbLogOpenFromTelnet.Size = new System.Drawing.Size(152, 22);
            this.tsbLogOpenFromTelnet.Text = "&Telnet";
            this.tsbLogOpenFromTelnet.Click += new System.EventHandler(this.tsbLogOpenFromTelnet_Click);
            // 
            // tsbLogClose
            // 
            this.tsbLogClose.Image = global::HciSolutions.LogSpotter.Properties.Resources.CloseLog;
            this.tsbLogClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLogClose.Name = "tsbLogClose";
            this.tsbLogClose.Size = new System.Drawing.Size(76, 22);
            this.tsbLogClose.Text = "Close log";
            this.tsbLogClose.ToolTipText = "Close log (Ctrl+W)";
            this.tsbLogClose.Click += new System.EventHandler(this.tsbLogClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbModePaused
            // 
            this.tsbModePaused.Image = global::HciSolutions.LogSpotter.Properties.Resources.PauseCapture;
            this.tsbModePaused.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModePaused.Name = "tsbModePaused";
            this.tsbModePaused.Size = new System.Drawing.Size(65, 22);
            this.tsbModePaused.Text = "Paused";
            this.tsbModePaused.ToolTipText = "Pause log acquisition (Ctrl+P)";
            this.tsbModePaused.Click += new System.EventHandler(this.tsbModePaused_Click);
            // 
            // tsbModeFollow
            // 
            this.tsbModeFollow.Image = global::HciSolutions.LogSpotter.Properties.Resources.FollowLastLog;
            this.tsbModeFollow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModeFollow.Name = "tsbModeFollow";
            this.tsbModeFollow.Size = new System.Drawing.Size(62, 22);
            this.tsbModeFollow.Text = "Follow";
            this.tsbModeFollow.ToolTipText = "Follow the last log (Ctrl+F)";
            this.tsbModeFollow.Click += new System.EventHandler(this.tsbModeFollow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFilterApply
            // 
            this.tsbFilterApply.Image = global::HciSolutions.LogSpotter.Properties.Resources.FilterApply;
            this.tsbFilterApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilterApply.Name = "tsbFilterApply";
            this.tsbFilterApply.Size = new System.Drawing.Size(85, 22);
            this.tsbFilterApply.Text = "Apply filter";
            this.tsbFilterApply.ToolTipText = "Apply the current filter (F7)";
            this.tsbFilterApply.Click += new System.EventHandler(this.tsbFilterApply_Click);
            // 
            // tsbFilterReset
            // 
            this.tsbFilterReset.Image = global::HciSolutions.LogSpotter.Properties.Resources.FilterErase;
            this.tsbFilterReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilterReset.Name = "tsbFilterReset";
            this.tsbFilterReset.Size = new System.Drawing.Size(82, 22);
            this.tsbFilterReset.Text = "Reset filter";
            this.tsbFilterReset.ToolTipText = "Reset the current filter (F8)";
            this.tsbFilterReset.Click += new System.EventHandler(this.tsbFilterReset_Click);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiMode,
            this.tsmiFilter});
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
            this.tsmiFileOpenLog,
            this.tsmiFileCloseLog,
            this.tsmiRecent,
            this.tsmiFileSep1,
            this.tsmiFileSettings,
            this.tsmiFileSep2,
            this.tsmiFileExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "&File";
            // 
            // tsmiFileOpenLog
            // 
            this.tsmiFileOpenLog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileOpenLogFromFile,
            this.tsmiFileOpenLogFromTelnet,
            this.tsmiDatabaseSqlServer});
            this.tsmiFileOpenLog.Image = global::HciSolutions.LogSpotter.Properties.Resources.OpenLog;
            this.tsmiFileOpenLog.Name = "tsmiFileOpenLog";
            this.tsmiFileOpenLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiFileOpenLog.Size = new System.Drawing.Size(168, 22);
            this.tsmiFileOpenLog.Text = "&Open log";
            // 
            // tsmiFileOpenLogFromFile
            // 
            this.tsmiFileOpenLogFromFile.Image = global::HciSolutions.LogSpotter.Properties.Resources.LogSourceFile;
            this.tsmiFileOpenLogFromFile.Name = "tsmiFileOpenLogFromFile";
            this.tsmiFileOpenLogFromFile.Size = new System.Drawing.Size(181, 22);
            this.tsmiFileOpenLogFromFile.Text = "&File";
            this.tsmiFileOpenLogFromFile.Click += new System.EventHandler(this.tsmiFileOpenLogFromFile_Click);
            // 
            // tsmiFileOpenLogFromTelnet
            // 
            this.tsmiFileOpenLogFromTelnet.Image = global::HciSolutions.LogSpotter.Properties.Resources.LogSourceTelnet;
            this.tsmiFileOpenLogFromTelnet.Name = "tsmiFileOpenLogFromTelnet";
            this.tsmiFileOpenLogFromTelnet.Size = new System.Drawing.Size(181, 22);
            this.tsmiFileOpenLogFromTelnet.Text = "&Telnet";
            this.tsmiFileOpenLogFromTelnet.Click += new System.EventHandler(this.tsmiFileOpenLogFromTelnet_Click);
            // 
            // tsmiDatabaseSqlServer
            // 
            this.tsmiDatabaseSqlServer.Image = global::HciSolutions.LogSpotter.Properties.Resources.SqlServer;
            this.tsmiDatabaseSqlServer.Name = "tsmiDatabaseSqlServer";
            this.tsmiDatabaseSqlServer.Size = new System.Drawing.Size(181, 22);
            this.tsmiDatabaseSqlServer.Text = "Database - SqlServer";
            this.tsmiDatabaseSqlServer.Click += new System.EventHandler(this.tsmiDatabaseSqlServer_Click);
            // 
            // tsmiFileCloseLog
            // 
            this.tsmiFileCloseLog.Image = global::HciSolutions.LogSpotter.Properties.Resources.CloseLog;
            this.tsmiFileCloseLog.Name = "tsmiFileCloseLog";
            this.tsmiFileCloseLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tsmiFileCloseLog.Size = new System.Drawing.Size(168, 22);
            this.tsmiFileCloseLog.Text = "&Close log";
            this.tsmiFileCloseLog.Click += new System.EventHandler(this.tsmiFileCloseLog_Click);
            // 
            // tsmiRecent
            // 
            this.tsmiRecent.Name = "tsmiRecent";
            this.tsmiRecent.Size = new System.Drawing.Size(168, 22);
            this.tsmiRecent.Text = "Recent";
            // 
            // tsmiFileSep1
            // 
            this.tsmiFileSep1.Name = "tsmiFileSep1";
            this.tsmiFileSep1.Size = new System.Drawing.Size(165, 6);
            // 
            // tsmiFileSettings
            // 
            this.tsmiFileSettings.Image = global::HciSolutions.LogSpotter.Properties.Resources.EditConfig;
            this.tsmiFileSettings.Name = "tsmiFileSettings";
            this.tsmiFileSettings.Size = new System.Drawing.Size(168, 22);
            this.tsmiFileSettings.Text = "&Settings";
            this.tsmiFileSettings.Click += new System.EventHandler(this.tsmiFileSettings_Click);
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
            this.tsmiFileExit.Click += new System.EventHandler(this.tsmiFileExit_Click);
            // 
            // tsmiMode
            // 
            this.tsmiMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiModePaused,
            this.tsmiModeFollow});
            this.tsmiMode.Name = "tsmiMode";
            this.tsmiMode.Size = new System.Drawing.Size(50, 20);
            this.tsmiMode.Text = "&Mode";
            // 
            // tsmiModePaused
            // 
            this.tsmiModePaused.Image = global::HciSolutions.LogSpotter.Properties.Resources.PauseCapture;
            this.tsmiModePaused.Name = "tsmiModePaused";
            this.tsmiModePaused.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsmiModePaused.Size = new System.Drawing.Size(190, 22);
            this.tsmiModePaused.Text = "&Paused";
            this.tsmiModePaused.Click += new System.EventHandler(this.tsmiModePaused_Click);
            // 
            // tsmiModeFollow
            // 
            this.tsmiModeFollow.Image = global::HciSolutions.LogSpotter.Properties.Resources.FollowLastLog;
            this.tsmiModeFollow.Name = "tsmiModeFollow";
            this.tsmiModeFollow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.tsmiModeFollow.Size = new System.Drawing.Size(190, 22);
            this.tsmiModeFollow.Text = "&Follow last log";
            this.tsmiModeFollow.Click += new System.EventHandler(this.tsmiModeFollow_Click);
            // 
            // tsmiFilter
            // 
            this.tsmiFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFilterApply,
            this.tsmiFilterReset});
            this.tsmiFilter.Name = "tsmiFilter";
            this.tsmiFilter.Size = new System.Drawing.Size(45, 20);
            this.tsmiFilter.Text = "&Filter";
            // 
            // tsmiFilterApply
            // 
            this.tsmiFilterApply.Image = global::HciSolutions.LogSpotter.Properties.Resources.FilterApply;
            this.tsmiFilterApply.Name = "tsmiFilterApply";
            this.tsmiFilterApply.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsmiFilterApply.Size = new System.Drawing.Size(151, 22);
            this.tsmiFilterApply.Text = "&Apply filter";
            this.tsmiFilterApply.Click += new System.EventHandler(this.tsmiFilterApply_Click);
            // 
            // tsmiFilterReset
            // 
            this.tsmiFilterReset.Image = global::HciSolutions.LogSpotter.Properties.Resources.FilterErase;
            this.tsmiFilterReset.Name = "tsmiFilterReset";
            this.tsmiFilterReset.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.tsmiFilterReset.Size = new System.Drawing.Size(151, 22);
            this.tsmiFilterReset.Text = "&Reset Filter";
            this.tsmiFilterReset.Click += new System.EventHandler(this.tsmiFilterReset_Click);
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
            this.tcLogs.SelectedIndexChanged += new System.EventHandler(this.tcLogs_SelectedIndexChanged);
            // 
            // ofdXmlLogFile
            // 
            this.ofdXmlLogFile.DefaultExt = "xml";
            this.ofdXmlLogFile.Filter = resources.GetString("ofdXmlLogFile.Filter");
            this.ofdXmlLogFile.Title = "Select the XML log file to open";
            // 
            // tsbLogOpenFromSqlServerDatabase
            // 
            this.tsbLogOpenFromSqlServerDatabase.Image = global::HciSolutions.LogSpotter.Properties.Resources.SqlServer;
            this.tsbLogOpenFromSqlServerDatabase.Name = "tsbLogOpenFromSqlServerDatabase";
            this.tsbLogOpenFromSqlServerDatabase.Size = new System.Drawing.Size(152, 22);
            this.tsbLogOpenFromSqlServerDatabase.Text = "SqlServer";
            this.tsbLogOpenFromSqlServerDatabase.Click += new System.EventHandler(this.tsbLogOpenFromSqlServerDatabase_Click);
            // 
            // MainForm
            // 
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
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbFilterApply;
        private System.Windows.Forms.ToolStripButton tsbFilterReset;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileOpenLog;
        private System.Windows.Forms.ToolStripMenuItem tsmiFilter;
        private System.Windows.Forms.ToolStripMenuItem tsmiFilterApply;
        private System.Windows.Forms.ToolStripMenuItem tsmiFilterReset;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileCloseLog;
        private System.Windows.Forms.ToolStripSeparator tsmiFileSep1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileExit;
        private System.Windows.Forms.TabControl tcLogs;
        private System.Windows.Forms.OpenFileDialog ofdXmlLogFile;
        private System.Windows.Forms.ToolStripButton tsbModePaused;
        private System.Windows.Forms.ToolStripButton tsbModeFollow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiMode;
        private System.Windows.Forms.ToolStripMenuItem tsmiModePaused;
        private System.Windows.Forms.ToolStripMenuItem tsmiModeFollow;
        private System.Windows.Forms.ToolStripButton tsbLogClose;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileSettings;
        private System.Windows.Forms.ToolStripSeparator tsmiFileSep2;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileOpenLogFromFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileOpenLogFromTelnet;
        private System.Windows.Forms.ToolStripSplitButton tsbLogOpen;
        private System.Windows.Forms.ToolStripMenuItem tsbLogOpenFromFile;
        private System.Windows.Forms.ToolStripMenuItem tsbLogOpenFromTelnet;
        private System.Windows.Forms.ToolStripMenuItem tsmiRecent;
        private System.Windows.Forms.ToolStripMenuItem tsmiDatabaseSqlServer;
        private System.Windows.Forms.ToolStripMenuItem tsbLogOpenFromSqlServerDatabase;
    }
}

