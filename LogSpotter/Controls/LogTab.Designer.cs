namespace HciSolutions.LogSpotter.Controls
{
    partial class LogTab
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.ucDisplayFilter = new HciSolutions.LogSpotter.Controls.DisplayFilter();
            this.ucLogView = new HciSolutions.LogSpotter.Controls.LogViewer();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.tsslLoading = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTotalEvents = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslShownEvents = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslFullSourceName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatusPause = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatusFollow = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatusFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatusOnline = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatusError = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrBinkErrorStatus = new System.Windows.Forms.Timer(this.components);
            this.tlpMain.SuspendLayout();
            this.ssStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.ucDisplayFilter, 0, 0);
            this.tlpMain.Controls.Add(this.ucLogView, 0, 1);
            this.tlpMain.Controls.Add(this.ssStatus, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(780, 588);
            this.tlpMain.TabIndex = 0;
            // 
            // ucDisplayFilter
            // 
            this.ucDisplayFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDisplayFilter.Location = new System.Drawing.Point(3, 3);
            this.ucDisplayFilter.Name = "ucDisplayFilter";
            this.ucDisplayFilter.Size = new System.Drawing.Size(774, 125);
            this.ucDisplayFilter.TabIndex = 0;
            // 
            // ucLogView
            // 
            this.ucLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogView.FollowLastLog = true;
            this.ucLogView.Location = new System.Drawing.Point(3, 134);
            this.ucLogView.LogCollection = null;
            this.ucLogView.Name = "ucLogView";
            this.ucLogView.Size = new System.Drawing.Size(774, 429);
            this.ucLogView.TabIndex = 1;
            // 
            // ssStatus
            // 
            this.ssStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLoading,
            this.tsslTotalEvents,
            this.tsslShownEvents,
            this.tsslFullSourceName,
            this.tsslStatusPause,
            this.tsslStatusFollow,
            this.tsslStatusFilter,
            this.tsslStatusOnline,
            this.tsslStatusError});
            this.ssStatus.Location = new System.Drawing.Point(0, 566);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.ShowItemToolTips = true;
            this.ssStatus.Size = new System.Drawing.Size(780, 22);
            this.ssStatus.TabIndex = 2;
            // 
            // tsslLoading
            // 
            this.tsslLoading.Name = "tsslLoading";
            this.tsslLoading.Size = new System.Drawing.Size(50, 17);
            this.tsslLoading.Text = "Loading";
            // 
            // tsslTotalEvents
            // 
            this.tsslTotalEvents.AutoSize = false;
            this.tsslTotalEvents.Name = "tsslTotalEvents";
            this.tsslTotalEvents.Size = new System.Drawing.Size(130, 17);
            this.tsslTotalEvents.Text = "Total events";
            this.tsslTotalEvents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslShownEvents
            // 
            this.tsslShownEvents.AutoSize = false;
            this.tsslShownEvents.Name = "tsslShownEvents";
            this.tsslShownEvents.Size = new System.Drawing.Size(130, 17);
            this.tsslShownEvents.Text = "Shown events";
            this.tsslShownEvents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslFullSourceName
            // 
            this.tsslFullSourceName.Name = "tsslFullSourceName";
            this.tsslFullSourceName.Size = new System.Drawing.Size(370, 17);
            this.tsslFullSourceName.Spring = true;
            this.tsslFullSourceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslStatusPause
            // 
            this.tsslStatusPause.AutoSize = false;
            this.tsslStatusPause.Image = global::HciSolutions.LogSpotter.Properties.Resources.PauseCapture;
            this.tsslStatusPause.Name = "tsslStatusPause";
            this.tsslStatusPause.Size = new System.Drawing.Size(17, 17);
            this.tsslStatusPause.ToolTipText = "Paused";
            // 
            // tsslStatusFollow
            // 
            this.tsslStatusFollow.AutoSize = false;
            this.tsslStatusFollow.Image = global::HciSolutions.LogSpotter.Properties.Resources.FollowLastLog;
            this.tsslStatusFollow.Name = "tsslStatusFollow";
            this.tsslStatusFollow.Size = new System.Drawing.Size(17, 17);
            this.tsslStatusFollow.ToolTipText = "Follow last log enabled";
            // 
            // tsslStatusFilter
            // 
            this.tsslStatusFilter.AutoSize = false;
            this.tsslStatusFilter.Image = global::HciSolutions.LogSpotter.Properties.Resources.FilterEnabled;
            this.tsslStatusFilter.Name = "tsslStatusFilter";
            this.tsslStatusFilter.Size = new System.Drawing.Size(17, 17);
            this.tsslStatusFilter.ToolTipText = "Filter enabled";
            // 
            // tsslStatusOnline
            // 
            this.tsslStatusOnline.AutoSize = false;
            this.tsslStatusOnline.Image = global::HciSolutions.LogSpotter.Properties.Resources.Offline;
            this.tsslStatusOnline.Name = "tsslStatusOnline";
            this.tsslStatusOnline.Size = new System.Drawing.Size(17, 17);
            // 
            // tsslStatusError
            // 
            this.tsslStatusError.AutoSize = false;
            this.tsslStatusError.Image = global::HciSolutions.LogSpotter.Properties.Resources.DataSourceError;
            this.tsslStatusError.Name = "tsslStatusError";
            this.tsslStatusError.Size = new System.Drawing.Size(17, 17);
            // 
            // tmrBinkErrorStatus
            // 
            this.tmrBinkErrorStatus.Interval = 250;
            this.tmrBinkErrorStatus.Tick += new System.EventHandler(this.tmrBinkErrorStatus_Tick);
            // 
            // LogTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "LogTab";
            this.Size = new System.Drawing.Size(780, 588);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private DisplayFilter ucDisplayFilter;
        private LogViewer ucLogView;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslTotalEvents;
        private System.Windows.Forms.ToolStripStatusLabel tsslShownEvents;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusPause;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusFilter;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusOnline;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusFollow;
        private System.Windows.Forms.ToolStripStatusLabel tsslLoading;
        private System.Windows.Forms.ToolStripStatusLabel tsslFullSourceName;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusError;
        private System.Windows.Forms.Timer tmrBinkErrorStatus;
    }
}
