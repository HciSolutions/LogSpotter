namespace Triamun.Tools.DatabaseToCsvExtractor.Gui
{
    partial class ConnectionStringEditorForm
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
            System.Windows.Forms.Label lblDatabase;
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.cbAuthentication = new System.Windows.Forms.ComboBox();
            this.cbServerName = new System.Windows.Forms.ComboBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblAuthentication = new System.Windows.Forms.Label();
            this.lblServerName = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.ssStatusBar = new System.Windows.Forms.StatusStrip();
            this.sslInfoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cbDatabaseName = new System.Windows.Forms.ComboBox();
            this.bwLoadServers = new System.ComponentModel.BackgroundWorker();
            this.bwLoadDatabases = new System.ComponentModel.BackgroundWorker();
            lblDatabase = new System.Windows.Forms.Label();
            this.ssStatusBar.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDatabase
            // 
            lblDatabase.AutoSize = true;
            lblDatabase.Dock = System.Windows.Forms.DockStyle.Left;
            lblDatabase.Location = new System.Drawing.Point(8, 114);
            lblDatabase.Margin = new System.Windows.Forms.Padding(3);
            lblDatabase.Name = "lblDatabase";
            lblDatabase.Size = new System.Drawing.Size(56, 21);
            lblDatabase.TabIndex = 10;
            lblDatabase.Text = "Database:";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(8, 173);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(92, 173);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbPassword
            // 
            this.tlpMain.SetColumnSpan(this.tbPassword, 2);
            this.tbPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPassword.Location = new System.Drawing.Point(92, 88);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '●';
            this.tbPassword.Size = new System.Drawing.Size(371, 20);
            this.tbPassword.TabIndex = 3;
            // 
            // tbUser
            // 
            this.tlpMain.SetColumnSpan(this.tbUser, 2);
            this.tbUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUser.Location = new System.Drawing.Point(92, 62);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(371, 20);
            this.tbUser.TabIndex = 2;
            // 
            // cbAuthentication
            // 
            this.tlpMain.SetColumnSpan(this.cbAuthentication, 2);
            this.cbAuthentication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAuthentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuthentication.FormattingEnabled = true;
            this.cbAuthentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authencication"});
            this.cbAuthentication.Location = new System.Drawing.Point(92, 35);
            this.cbAuthentication.Name = "cbAuthentication";
            this.cbAuthentication.Size = new System.Drawing.Size(371, 21);
            this.cbAuthentication.TabIndex = 1;
            this.cbAuthentication.SelectedIndexChanged += new System.EventHandler(this.cbAuthentication_SelectedIndexChanged);
            // 
            // cbServerName
            // 
            this.tlpMain.SetColumnSpan(this.cbServerName, 2);
            this.cbServerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbServerName.FormattingEnabled = true;
            this.cbServerName.Items.AddRange(new object[] {
            "Loading..."});
            this.cbServerName.Location = new System.Drawing.Point(92, 8);
            this.cbServerName.Name = "cbServerName";
            this.cbServerName.Size = new System.Drawing.Size(371, 21);
            this.cbServerName.TabIndex = 0;
            this.cbServerName.DropDown += new System.EventHandler(this.cbServerName_DropDown);
            this.cbServerName.TextChanged += new System.EventHandler(this.cbServerName_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPassword.Location = new System.Drawing.Point(18, 88);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(13, 3, 3, 3);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 20);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUserName.Location = new System.Drawing.Point(18, 62);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(13, 3, 3, 3);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(61, 20);
            this.lblUserName.TabIndex = 6;
            this.lblUserName.Text = "User name:";
            // 
            // lblAuthentication
            // 
            this.lblAuthentication.AutoSize = true;
            this.lblAuthentication.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAuthentication.Location = new System.Drawing.Point(8, 35);
            this.lblAuthentication.Margin = new System.Windows.Forms.Padding(3);
            this.lblAuthentication.Name = "lblAuthentication";
            this.lblAuthentication.Size = new System.Drawing.Size(78, 21);
            this.lblAuthentication.TabIndex = 9;
            this.lblAuthentication.Text = "Authentication:";
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblServerName.Location = new System.Drawing.Point(8, 8);
            this.lblServerName.Margin = new System.Windows.Forms.Padding(3);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(70, 21);
            this.lblServerName.TabIndex = 8;
            this.lblServerName.Text = "Server name:";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(388, 173);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // ssStatusBar
            // 
            this.ssStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslInfoLabel});
            this.ssStatusBar.Location = new System.Drawing.Point(0, 204);
            this.ssStatusBar.Name = "ssStatusBar";
            this.ssStatusBar.Size = new System.Drawing.Size(471, 22);
            this.ssStatusBar.TabIndex = 6;
            this.ssStatusBar.Text = "statusStrip1";
            // 
            // sslInfoLabel
            // 
            this.sslInfoLabel.Name = "sslInfoLabel";
            this.sslInfoLabel.Size = new System.Drawing.Size(59, 17);
            this.sslInfoLabel.Text = "Loading...";
            this.sslInfoLabel.Visible = false;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(lblDatabase, 0, 4);
            this.tlpMain.Controls.Add(this.tbPassword, 1, 3);
            this.tlpMain.Controls.Add(this.btnOK, 0, 6);
            this.tlpMain.Controls.Add(this.tbUser, 1, 2);
            this.tlpMain.Controls.Add(this.btnCancel, 1, 6);
            this.tlpMain.Controls.Add(this.cbAuthentication, 1, 1);
            this.tlpMain.Controls.Add(this.btnTest, 2, 6);
            this.tlpMain.Controls.Add(this.cbServerName, 1, 0);
            this.tlpMain.Controls.Add(this.lblPassword, 0, 3);
            this.tlpMain.Controls.Add(this.lblServerName, 0, 0);
            this.tlpMain.Controls.Add(this.lblUserName, 0, 2);
            this.tlpMain.Controls.Add(this.lblAuthentication, 0, 1);
            this.tlpMain.Controls.Add(this.cbDatabaseName, 1, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(10);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(5);
            this.tlpMain.RowCount = 7;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(471, 204);
            this.tlpMain.TabIndex = 7;
            // 
            // cbDatabaseName
            // 
            this.tlpMain.SetColumnSpan(this.cbDatabaseName, 2);
            this.cbDatabaseName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDatabaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabaseName.FormattingEnabled = true;
            this.cbDatabaseName.Items.AddRange(new object[] {
            "Loading..."});
            this.cbDatabaseName.Location = new System.Drawing.Point(92, 114);
            this.cbDatabaseName.Name = "cbDatabaseName";
            this.cbDatabaseName.Size = new System.Drawing.Size(371, 21);
            this.cbDatabaseName.TabIndex = 11;
            this.cbDatabaseName.DropDown += new System.EventHandler(this.cbDatabaseName_DropDown);
            // 
            // bwLoadServers
            // 
            this.bwLoadServers.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadServers_DoWork);
            this.bwLoadServers.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadServers_RunWorkerCompleted);
            // 
            // bwLoadDatabases
            // 
            this.bwLoadDatabases.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadDatabases_DoWork);
            this.bwLoadDatabases.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadDatabases_RunWorkerCompleted);
            // 
            // ConnectionStringEditorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(471, 226);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.ssStatusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(100000, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(270, 222);
            this.Name = "ConnectionStringEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database connection string";
            this.ssStatusBar.ResumeLayout(false);
            this.ssStatusBar.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.ComboBox cbAuthentication;
        private System.Windows.Forms.ComboBox cbServerName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblAuthentication;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.StatusStrip ssStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel sslInfoLabel;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ComboBox cbDatabaseName;
        private System.ComponentModel.BackgroundWorker bwLoadServers;
        private System.ComponentModel.BackgroundWorker bwLoadDatabases;
    }
}