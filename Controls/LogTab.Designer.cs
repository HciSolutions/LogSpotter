namespace Triamun.Log4NetViewer.Controls
{
    partial class LogTab
    {
		#region Private Members 

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
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
            this.tlpLoad = new System.Windows.Forms.TableLayoutPanel();
            this.pbLoad = new System.Windows.Forms.ProgressBar();
            this.lblLoad = new System.Windows.Forms.Label();
            this.ucDisplayFilter = new Triamun.Log4NetViewer.Controls.DisplayFilter();
            this.ucLogView = new Triamun.Log4NetViewer.Controls.LogViewer();
            this.tlpMain.SuspendLayout();
            this.ssStatus.SuspendLayout();
            this.tlpLoad.SuspendLayout();
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
            this.tsslLoading.Size = new System.Drawing.Size(44, 17);
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
            this.tsslFullSourceName.Size = new System.Drawing.Size(376, 17);
            this.tsslFullSourceName.Spring = true;
            this.tsslFullSourceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslStatusPause
            // 
            this.tsslStatusPause.AutoSize = false;
            this.tsslStatusPause.Image = global::Triamun.Log4NetViewer.Properties.Resources.PauseCapture;
            this.tsslStatusPause.Name = "tsslStatusPause";
            this.tsslStatusPause.Size = new System.Drawing.Size(17, 17);
            this.tsslStatusPause.ToolTipText = "Paused";
            // 
            // tsslStatusFollow
            // 
            this.tsslStatusFollow.AutoSize = false;
            this.tsslStatusFollow.Image = global::Triamun.Log4NetViewer.Properties.Resources.FollowLastLog;
            this.tsslStatusFollow.Name = "tsslStatusFollow";
            this.tsslStatusFollow.Size = new System.Drawing.Size(17, 17);
            this.tsslStatusFollow.ToolTipText = "Follow last log enabled";
            // 
            // tsslStatusFilter
            // 
            this.tsslStatusFilter.AutoSize = false;
            this.tsslStatusFilter.Image = global::Triamun.Log4NetViewer.Properties.Resources.FilterEnabled;
            this.tsslStatusFilter.Name = "tsslStatusFilter";
            this.tsslStatusFilter.Size = new System.Drawing.Size(17, 17);
            this.tsslStatusFilter.ToolTipText = "Filter enabled";
            // 
            // tsslStatusOnline
            // 
            this.tsslStatusOnline.AutoSize = false;
            this.tsslStatusOnline.Image = global::Triamun.Log4NetViewer.Properties.Resources.Offline;
            this.tsslStatusOnline.Name = "tsslStatusOnline";
            this.tsslStatusOnline.Size = new System.Drawing.Size(17, 17);
            // 
            // tsslStatusError
            // 
            this.tsslStatusError.AutoSize = false;
            this.tsslStatusError.Image = global::Triamun.Log4NetViewer.Properties.Resources.DataSourceError;
            this.tsslStatusError.Name = "tsslStatusError";
            this.tsslStatusError.Size = new System.Drawing.Size(17, 17);
            // 
            // tmrBinkErrorStatus
            // 
            this.tmrBinkErrorStatus.Interval = 250;
            this.tmrBinkErrorStatus.Tick += new System.EventHandler(this.tmrBinkErrorStatus_Tick);
            // 
            // tlpLoad
            // 
            this.tlpLoad.ColumnCount = 1;
            this.tlpLoad.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoad.Controls.Add(this.pbLoad, 0, 2);
            this.tlpLoad.Controls.Add(this.lblLoad, 0, 1);
            this.tlpLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLoad.Location = new System.Drawing.Point(0, 0);
            this.tlpLoad.Name = "tlpLoad";
            this.tlpLoad.RowCount = 4;
            this.tlpLoad.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoad.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpLoad.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLoad.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoad.Size = new System.Drawing.Size(780, 588);
            this.tlpLoad.TabIndex = 1;
            this.tlpLoad.Visible = false;
            // 
            // pbLoad
            // 
            this.pbLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLoad.Location = new System.Drawing.Point(23, 292);
            this.pbLoad.Margin = new System.Windows.Forms.Padding(23, 3, 23, 3);
            this.pbLoad.Name = "pbLoad";
            this.pbLoad.Size = new System.Drawing.Size(734, 23);
            this.pbLoad.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbLoad.TabIndex = 0;
            // 
            // lblLoad
            // 
            this.lblLoad.AutoSize = true;
            this.lblLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoad.Location = new System.Drawing.Point(23, 272);
            this.lblLoad.Margin = new System.Windows.Forms.Padding(23, 3, 23, 3);
            this.lblLoad.Name = "lblLoad";
            this.lblLoad.Size = new System.Drawing.Size(734, 14);
            this.lblLoad.TabIndex = 1;
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
            // LogTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.tlpLoad);
            this.Name = "LogTab";
            this.Size = new System.Drawing.Size(780, 588);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.tlpLoad.ResumeLayout(false);
            this.tlpLoad.PerformLayout();
            this.ResumeLayout(false);

        }

		#endregion Private Methods 

        private System.Windows.Forms.TableLayoutPanel tlpLoad;
        private System.Windows.Forms.ProgressBar pbLoad;
        private System.Windows.Forms.Label lblLoad;
    }
}
