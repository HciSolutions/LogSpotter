namespace HciSolutions.LogSpotter.Controls
{
    partial class DisplayFilter
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
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.tlpFilters = new System.Windows.Forms.TableLayoutPanel();
            this.flpLogLevels = new System.Windows.Forms.FlowLayoutPanel();
            this.cbFilterLevelDebug = new System.Windows.Forms.CheckBox();
            this.cbFilterLevelInfo = new System.Windows.Forms.CheckBox();
            this.cbFilterLevelWarning = new System.Windows.Forms.CheckBox();
            this.cbFilterLevelError = new System.Windows.Forms.CheckBox();
            this.cbFilterLevelFatal = new System.Windows.Forms.CheckBox();
            this.tbFilterLogger = new System.Windows.Forms.TextBox();
            this.tbFilterThread = new System.Windows.Forms.TextBox();
            this.tbFilterDomain = new System.Windows.Forms.TextBox();
            this.tbFilterFileName = new System.Windows.Forms.TextBox();
            this.tbFilterException = new System.Windows.Forms.TextBox();
            this.tbFilterMessage = new System.Windows.Forms.TextBox();
            this.tbFilterClassName = new System.Windows.Forms.TextBox();
            this.tlpFilterTimeStamp = new System.Windows.Forms.TableLayoutPanel();
            this.lblFilterTimeStampFromToTitle = new System.Windows.Forms.Label();
            this.dtpFilterTimeStampTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFilterTimeStampFrom = new System.Windows.Forms.DateTimePicker();
            this.cbFilterTimeStampEnabled = new System.Windows.Forms.CheckBox();
            this.lblFilterLoggerTitle = new System.Windows.Forms.Label();
            this.lblFilterTimeStampTitle = new System.Windows.Forms.Label();
            this.lblFilterClassNameTitle = new System.Windows.Forms.Label();
            this.lblFilterMessageTitle = new System.Windows.Forms.Label();
            this.lblFilterThreadTitle = new System.Windows.Forms.Label();
            this.lblFilterDomainTitle = new System.Windows.Forms.Label();
            this.lblFilterFileNameTitle = new System.Windows.Forms.Label();
            this.lblFilterExceptionTitle = new System.Windows.Forms.Label();
            this.gbFilters.SuspendLayout();
            this.tlpFilters.SuspendLayout();
            this.flpLogLevels.SuspendLayout();
            this.tlpFilterTimeStamp.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilters
            // 
            this.gbFilters.Controls.Add(this.tlpFilters);
            this.gbFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilters.Location = new System.Drawing.Point(0, 0);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(773, 125);
            this.gbFilters.TabIndex = 1;
            this.gbFilters.TabStop = false;
            this.gbFilters.Text = "Filters";
            // 
            // tlpFilters
            // 
            this.tlpFilters.ColumnCount = 5;
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilters.Controls.Add(this.flpLogLevels, 4, 0);
            this.tlpFilters.Controls.Add(this.tbFilterLogger, 1, 0);
            this.tlpFilters.Controls.Add(this.tbFilterThread, 3, 0);
            this.tlpFilters.Controls.Add(this.tbFilterDomain, 3, 1);
            this.tlpFilters.Controls.Add(this.tbFilterFileName, 3, 2);
            this.tlpFilters.Controls.Add(this.tbFilterException, 3, 3);
            this.tlpFilters.Controls.Add(this.tbFilterMessage, 1, 3);
            this.tlpFilters.Controls.Add(this.tbFilterClassName, 1, 2);
            this.tlpFilters.Controls.Add(this.tlpFilterTimeStamp, 1, 1);
            this.tlpFilters.Controls.Add(this.lblFilterLoggerTitle, 0, 0);
            this.tlpFilters.Controls.Add(this.lblFilterTimeStampTitle, 0, 1);
            this.tlpFilters.Controls.Add(this.lblFilterClassNameTitle, 0, 2);
            this.tlpFilters.Controls.Add(this.lblFilterMessageTitle, 0, 3);
            this.tlpFilters.Controls.Add(this.lblFilterThreadTitle, 2, 0);
            this.tlpFilters.Controls.Add(this.lblFilterDomainTitle, 2, 1);
            this.tlpFilters.Controls.Add(this.lblFilterFileNameTitle, 2, 2);
            this.tlpFilters.Controls.Add(this.lblFilterExceptionTitle, 2, 3);
            this.tlpFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilters.Location = new System.Drawing.Point(3, 16);
            this.tlpFilters.Name = "tlpFilters";
            this.tlpFilters.RowCount = 5;
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFilters.Size = new System.Drawing.Size(767, 106);
            this.tlpFilters.TabIndex = 0;
            // 
            // flpLogLevels
            // 
            this.flpLogLevels.AutoSize = true;
            this.flpLogLevels.Controls.Add(this.cbFilterLevelDebug);
            this.flpLogLevels.Controls.Add(this.cbFilterLevelInfo);
            this.flpLogLevels.Controls.Add(this.cbFilterLevelWarning);
            this.flpLogLevels.Controls.Add(this.cbFilterLevelError);
            this.flpLogLevels.Controls.Add(this.cbFilterLevelFatal);
            this.flpLogLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpLogLevels.Location = new System.Drawing.Point(695, 0);
            this.flpLogLevels.Margin = new System.Windows.Forms.Padding(0);
            this.flpLogLevels.Name = "flpLogLevels";
            this.tlpFilters.SetRowSpan(this.flpLogLevels, 5);
            this.flpLogLevels.Size = new System.Drawing.Size(72, 106);
            this.flpLogLevels.TabIndex = 10;
            // 
            // cbFilterLevelDebug
            // 
            this.cbFilterLevelDebug.AutoSize = true;
            this.cbFilterLevelDebug.Checked = true;
            this.cbFilterLevelDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterLevelDebug.Location = new System.Drawing.Point(3, 3);
            this.cbFilterLevelDebug.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.cbFilterLevelDebug.Name = "cbFilterLevelDebug";
            this.cbFilterLevelDebug.Size = new System.Drawing.Size(58, 17);
            this.cbFilterLevelDebug.TabIndex = 0;
            this.cbFilterLevelDebug.Text = "Debug";
            this.cbFilterLevelDebug.UseVisualStyleBackColor = true;
            // 
            // cbFilterLevelInfo
            // 
            this.cbFilterLevelInfo.AutoSize = true;
            this.cbFilterLevelInfo.Checked = true;
            this.cbFilterLevelInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterLevelInfo.Location = new System.Drawing.Point(3, 23);
            this.cbFilterLevelInfo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.cbFilterLevelInfo.Name = "cbFilterLevelInfo";
            this.cbFilterLevelInfo.Size = new System.Drawing.Size(44, 17);
            this.cbFilterLevelInfo.TabIndex = 1;
            this.cbFilterLevelInfo.Text = "Info";
            this.cbFilterLevelInfo.UseVisualStyleBackColor = true;
            // 
            // cbFilterLevelWarning
            // 
            this.cbFilterLevelWarning.AutoSize = true;
            this.cbFilterLevelWarning.Checked = true;
            this.cbFilterLevelWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterLevelWarning.Location = new System.Drawing.Point(3, 43);
            this.cbFilterLevelWarning.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.cbFilterLevelWarning.Name = "cbFilterLevelWarning";
            this.cbFilterLevelWarning.Size = new System.Drawing.Size(66, 17);
            this.cbFilterLevelWarning.TabIndex = 2;
            this.cbFilterLevelWarning.Text = "Warning";
            this.cbFilterLevelWarning.UseVisualStyleBackColor = true;
            // 
            // cbFilterLevelError
            // 
            this.cbFilterLevelError.AutoSize = true;
            this.cbFilterLevelError.Checked = true;
            this.cbFilterLevelError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterLevelError.Location = new System.Drawing.Point(3, 63);
            this.cbFilterLevelError.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.cbFilterLevelError.Name = "cbFilterLevelError";
            this.cbFilterLevelError.Size = new System.Drawing.Size(48, 17);
            this.cbFilterLevelError.TabIndex = 3;
            this.cbFilterLevelError.Text = "Error";
            this.cbFilterLevelError.UseVisualStyleBackColor = true;
            // 
            // cbFilterLevelFatal
            // 
            this.cbFilterLevelFatal.AutoSize = true;
            this.cbFilterLevelFatal.Checked = true;
            this.cbFilterLevelFatal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterLevelFatal.Location = new System.Drawing.Point(3, 83);
            this.cbFilterLevelFatal.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.cbFilterLevelFatal.Name = "cbFilterLevelFatal";
            this.cbFilterLevelFatal.Size = new System.Drawing.Size(49, 17);
            this.cbFilterLevelFatal.TabIndex = 4;
            this.cbFilterLevelFatal.Text = "Fatal";
            this.cbFilterLevelFatal.UseVisualStyleBackColor = true;
            // 
            // tbFilterLogger
            // 
            this.tbFilterLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilterLogger.Location = new System.Drawing.Point(75, 3);
            this.tbFilterLogger.Name = "tbFilterLogger";
            this.tbFilterLogger.Size = new System.Drawing.Size(274, 20);
            this.tbFilterLogger.TabIndex = 0;
            // 
            // tbFilterThread
            // 
            this.tbFilterThread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilterThread.Location = new System.Drawing.Point(418, 3);
            this.tbFilterThread.Name = "tbFilterThread";
            this.tbFilterThread.Size = new System.Drawing.Size(274, 20);
            this.tbFilterThread.TabIndex = 1;
            // 
            // tbFilterDomain
            // 
            this.tbFilterDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilterDomain.Location = new System.Drawing.Point(418, 29);
            this.tbFilterDomain.Name = "tbFilterDomain";
            this.tbFilterDomain.Size = new System.Drawing.Size(274, 20);
            this.tbFilterDomain.TabIndex = 5;
            // 
            // tbFilterFileName
            // 
            this.tbFilterFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilterFileName.Location = new System.Drawing.Point(418, 55);
            this.tbFilterFileName.Name = "tbFilterFileName";
            this.tbFilterFileName.Size = new System.Drawing.Size(274, 20);
            this.tbFilterFileName.TabIndex = 7;
            // 
            // tbFilterException
            // 
            this.tbFilterException.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilterException.Location = new System.Drawing.Point(418, 81);
            this.tbFilterException.Name = "tbFilterException";
            this.tbFilterException.Size = new System.Drawing.Size(274, 20);
            this.tbFilterException.TabIndex = 9;
            // 
            // tbFilterMessage
            // 
            this.tbFilterMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilterMessage.Location = new System.Drawing.Point(75, 81);
            this.tbFilterMessage.Name = "tbFilterMessage";
            this.tbFilterMessage.Size = new System.Drawing.Size(274, 20);
            this.tbFilterMessage.TabIndex = 8;
            // 
            // tbFilterClassName
            // 
            this.tbFilterClassName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilterClassName.Location = new System.Drawing.Point(75, 55);
            this.tbFilterClassName.Name = "tbFilterClassName";
            this.tbFilterClassName.Size = new System.Drawing.Size(274, 20);
            this.tbFilterClassName.TabIndex = 6;
            // 
            // tlpFilterTimeStamp
            // 
            this.tlpFilterTimeStamp.ColumnCount = 4;
            this.tlpFilterTimeStamp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilterTimeStamp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFilterTimeStamp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilterTimeStamp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFilterTimeStamp.Controls.Add(this.lblFilterTimeStampFromToTitle, 2, 0);
            this.tlpFilterTimeStamp.Controls.Add(this.dtpFilterTimeStampTo, 3, 0);
            this.tlpFilterTimeStamp.Controls.Add(this.dtpFilterTimeStampFrom, 1, 0);
            this.tlpFilterTimeStamp.Controls.Add(this.cbFilterTimeStampEnabled, 0, 0);
            this.tlpFilterTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilterTimeStamp.Location = new System.Drawing.Point(72, 26);
            this.tlpFilterTimeStamp.Margin = new System.Windows.Forms.Padding(0);
            this.tlpFilterTimeStamp.Name = "tlpFilterTimeStamp";
            this.tlpFilterTimeStamp.RowCount = 1;
            this.tlpFilterTimeStamp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilterTimeStamp.Size = new System.Drawing.Size(280, 26);
            this.tlpFilterTimeStamp.TabIndex = 3;
            // 
            // lblFilterTimeStampFromToTitle
            // 
            this.lblFilterTimeStampFromToTitle.AutoSize = true;
            this.lblFilterTimeStampFromToTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFilterTimeStampFromToTitle.Location = new System.Drawing.Point(145, 3);
            this.lblFilterTimeStampFromToTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilterTimeStampFromToTitle.Name = "lblFilterTimeStampFromToTitle";
            this.lblFilterTimeStampFromToTitle.Size = new System.Drawing.Size(10, 20);
            this.lblFilterTimeStampFromToTitle.TabIndex = 0;
            this.lblFilterTimeStampFromToTitle.Text = "-";
            this.lblFilterTimeStampFromToTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFilterTimeStampTo
            // 
            this.dtpFilterTimeStampTo.CustomFormat = "dd.MM.yy HH:mm:ss";
            this.dtpFilterTimeStampTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFilterTimeStampTo.Enabled = false;
            this.dtpFilterTimeStampTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFilterTimeStampTo.Location = new System.Drawing.Point(161, 3);
            this.dtpFilterTimeStampTo.Name = "dtpFilterTimeStampTo";
            this.dtpFilterTimeStampTo.Size = new System.Drawing.Size(116, 20);
            this.dtpFilterTimeStampTo.TabIndex = 4;
            // 
            // dtpFilterTimeStampFrom
            // 
            this.dtpFilterTimeStampFrom.CustomFormat = "dd.MM.yy HH:mm:ss";
            this.dtpFilterTimeStampFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFilterTimeStampFrom.Enabled = false;
            this.dtpFilterTimeStampFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFilterTimeStampFrom.Location = new System.Drawing.Point(24, 3);
            this.dtpFilterTimeStampFrom.Name = "dtpFilterTimeStampFrom";
            this.dtpFilterTimeStampFrom.Size = new System.Drawing.Size(115, 20);
            this.dtpFilterTimeStampFrom.TabIndex = 4;
            // 
            // cbFilterTimeStampEnabled
            // 
            this.cbFilterTimeStampEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbFilterTimeStampEnabled.Location = new System.Drawing.Point(3, 3);
            this.cbFilterTimeStampEnabled.Name = "cbFilterTimeStampEnabled";
            this.cbFilterTimeStampEnabled.Size = new System.Drawing.Size(15, 20);
            this.cbFilterTimeStampEnabled.TabIndex = 2;
            this.cbFilterTimeStampEnabled.UseVisualStyleBackColor = true;
            this.cbFilterTimeStampEnabled.CheckedChanged += new System.EventHandler(this.cbFilterTimeStampEnabled_CheckedChanged);
            // 
            // lblFilterLoggerTitle
            // 
            this.lblFilterLoggerTitle.AutoSize = true;
            this.lblFilterLoggerTitle.Location = new System.Drawing.Point(3, 3);
            this.lblFilterLoggerTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilterLoggerTitle.Name = "lblFilterLoggerTitle";
            this.lblFilterLoggerTitle.Size = new System.Drawing.Size(40, 13);
            this.lblFilterLoggerTitle.TabIndex = 0;
            this.lblFilterLoggerTitle.Text = "Logger";
            // 
            // lblFilterTimeStampTitle
            // 
            this.lblFilterTimeStampTitle.AutoSize = true;
            this.lblFilterTimeStampTitle.Location = new System.Drawing.Point(3, 29);
            this.lblFilterTimeStampTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilterTimeStampTitle.Name = "lblFilterTimeStampTitle";
            this.lblFilterTimeStampTitle.Size = new System.Drawing.Size(66, 13);
            this.lblFilterTimeStampTitle.TabIndex = 0;
            this.lblFilterTimeStampTitle.Text = "Time Stamp:";
            // 
            // lblFilterClassNameTitle
            // 
            this.lblFilterClassNameTitle.AutoSize = true;
            this.lblFilterClassNameTitle.Location = new System.Drawing.Point(3, 55);
            this.lblFilterClassNameTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilterClassNameTitle.Name = "lblFilterClassNameTitle";
            this.lblFilterClassNameTitle.Size = new System.Drawing.Size(66, 13);
            this.lblFilterClassNameTitle.TabIndex = 0;
            this.lblFilterClassNameTitle.Text = "Class Name:";
            // 
            // lblFilterMessageTitle
            // 
            this.lblFilterMessageTitle.AutoSize = true;
            this.lblFilterMessageTitle.Location = new System.Drawing.Point(3, 81);
            this.lblFilterMessageTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilterMessageTitle.Name = "lblFilterMessageTitle";
            this.lblFilterMessageTitle.Size = new System.Drawing.Size(53, 13);
            this.lblFilterMessageTitle.TabIndex = 0;
            this.lblFilterMessageTitle.Text = "Message:";
            // 
            // lblFilterThreadTitle
            // 
            this.lblFilterThreadTitle.AutoSize = true;
            this.lblFilterThreadTitle.Location = new System.Drawing.Point(355, 3);
            this.lblFilterThreadTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilterThreadTitle.Name = "lblFilterThreadTitle";
            this.lblFilterThreadTitle.Size = new System.Drawing.Size(44, 13);
            this.lblFilterThreadTitle.TabIndex = 0;
            this.lblFilterThreadTitle.Text = "Thread:";
            // 
            // lblFilterDomainTitle
            // 
            this.lblFilterDomainTitle.AutoSize = true;
            this.lblFilterDomainTitle.Location = new System.Drawing.Point(355, 29);
            this.lblFilterDomainTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilterDomainTitle.Name = "lblFilterDomainTitle";
            this.lblFilterDomainTitle.Size = new System.Drawing.Size(46, 13);
            this.lblFilterDomainTitle.TabIndex = 0;
            this.lblFilterDomainTitle.Text = "Domain:";
            // 
            // lblFilterFileNameTitle
            // 
            this.lblFilterFileNameTitle.AutoSize = true;
            this.lblFilterFileNameTitle.Location = new System.Drawing.Point(355, 55);
            this.lblFilterFileNameTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilterFileNameTitle.Name = "lblFilterFileNameTitle";
            this.lblFilterFileNameTitle.Size = new System.Drawing.Size(57, 13);
            this.lblFilterFileNameTitle.TabIndex = 0;
            this.lblFilterFileNameTitle.Text = "File Name:";
            // 
            // lblFilterExceptionTitle
            // 
            this.lblFilterExceptionTitle.AutoSize = true;
            this.lblFilterExceptionTitle.Location = new System.Drawing.Point(355, 81);
            this.lblFilterExceptionTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblFilterExceptionTitle.Name = "lblFilterExceptionTitle";
            this.lblFilterExceptionTitle.Size = new System.Drawing.Size(57, 13);
            this.lblFilterExceptionTitle.TabIndex = 0;
            this.lblFilterExceptionTitle.Text = "Exception:";
            // 
            // DisplayFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilters);
            this.Name = "DisplayFilter";
            this.Size = new System.Drawing.Size(773, 125);
            this.gbFilters.ResumeLayout(false);
            this.tlpFilters.ResumeLayout(false);
            this.tlpFilters.PerformLayout();
            this.flpLogLevels.ResumeLayout(false);
            this.flpLogLevels.PerformLayout();
            this.tlpFilterTimeStamp.ResumeLayout(false);
            this.tlpFilterTimeStamp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.TableLayoutPanel tlpFilters;
        private System.Windows.Forms.Label lblFilterLoggerTitle;
        private System.Windows.Forms.Label lblFilterTimeStampTitle;
        private System.Windows.Forms.Label lblFilterClassNameTitle;
        private System.Windows.Forms.Label lblFilterMessageTitle;
        private System.Windows.Forms.Label lblFilterThreadTitle;
        private System.Windows.Forms.Label lblFilterDomainTitle;
        private System.Windows.Forms.Label lblFilterFileNameTitle;
        private System.Windows.Forms.Label lblFilterExceptionTitle;
        private System.Windows.Forms.FlowLayoutPanel flpLogLevels;
        private System.Windows.Forms.CheckBox cbFilterLevelDebug;
        private System.Windows.Forms.CheckBox cbFilterLevelInfo;
        private System.Windows.Forms.CheckBox cbFilterLevelWarning;
        private System.Windows.Forms.CheckBox cbFilterLevelError;
        private System.Windows.Forms.CheckBox cbFilterLevelFatal;
        private System.Windows.Forms.TextBox tbFilterLogger;
        private System.Windows.Forms.TextBox tbFilterThread;
        private System.Windows.Forms.TextBox tbFilterDomain;
        private System.Windows.Forms.TextBox tbFilterFileName;
        private System.Windows.Forms.TextBox tbFilterException;
        private System.Windows.Forms.TextBox tbFilterMessage;
        private System.Windows.Forms.TextBox tbFilterClassName;
        private System.Windows.Forms.TableLayoutPanel tlpFilterTimeStamp;
        private System.Windows.Forms.Label lblFilterTimeStampFromToTitle;
        private System.Windows.Forms.DateTimePicker dtpFilterTimeStampTo;
        private System.Windows.Forms.DateTimePicker dtpFilterTimeStampFrom;
        private System.Windows.Forms.CheckBox cbFilterTimeStampEnabled;
    }
}
