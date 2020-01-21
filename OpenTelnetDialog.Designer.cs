namespace Triamun.Log4NetViewer
{
    partial class OpenTelnetDialog
    {
		#region Private Members 

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHostTitle;
        private System.Windows.Forms.Label lblPortTitle;
        private System.Windows.Forms.TextBox tbHhost;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnCancel;

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
            this.lblHostTitle = new System.Windows.Forms.Label();
            this.lblPortTitle = new System.Windows.Forms.Label();
            this.tbHhost = new System.Windows.Forms.TextBox();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHostTitle
            // 
            this.lblHostTitle.AutoSize = true;
            this.lblHostTitle.Location = new System.Drawing.Point(12, 12);
            this.lblHostTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblHostTitle.Name = "lblHostTitle";
            this.lblHostTitle.Size = new System.Drawing.Size(147, 13);
            this.lblHostTitle.TabIndex = 0;
            this.lblHostTitle.Text = "Telnet server host name or ip:";
            // 
            // lblPortTitle
            // 
            this.lblPortTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPortTitle.AutoSize = true;
            this.lblPortTitle.Location = new System.Drawing.Point(411, 12);
            this.lblPortTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblPortTitle.Name = "lblPortTitle";
            this.lblPortTitle.Size = new System.Drawing.Size(67, 13);
            this.lblPortTitle.TabIndex = 0;
            this.lblPortTitle.Text = "Port number:";
            // 
            // tbHhost
            // 
            this.tbHhost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHhost.Location = new System.Drawing.Point(15, 30);
            this.tbHhost.Name = "tbHhost";
            this.tbHhost.Size = new System.Drawing.Size(393, 20);
            this.tbHhost.TabIndex = 1;
            // 
            // nudPort
            // 
            this.nudPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPort.Location = new System.Drawing.Point(414, 31);
            this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(61, 20);
            this.nudPort.TabIndex = 2;
            this.nudPort.Value = new decimal(new int[] {
            2323,
            0,
            0,
            0});
            // 
            // btnOpen
            // 
            this.btnOpen.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOpen.Location = new System.Drawing.Point(15, 56);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "&Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(96, 56);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // OpenTelnetDialog
            // 
            this.AcceptButton = this.btnOpen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(490, 91);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.nudPort);
            this.Controls.Add(this.tbHhost);
            this.Controls.Add(this.lblPortTitle);
            this.Controls.Add(this.lblHostTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OpenTelnetDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open telnet log";
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion Private Methods 
    }
}