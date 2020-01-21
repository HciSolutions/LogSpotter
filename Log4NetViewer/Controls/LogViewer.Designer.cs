namespace Triamun.Log4NetViewer.Controls
{
    partial class LogViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblDetailLoggerTitle = new System.Windows.Forms.Label();
            this.gbDetail = new System.Windows.Forms.GroupBox();
            this.tlpDetail = new System.Windows.Forms.TableLayoutPanel();
            this.tbDetailLogger = new System.Windows.Forms.TextBox();
            this.bsLogs = new System.Windows.Forms.BindingSource(this.components);
            this.lblDetailLevelTitle = new System.Windows.Forms.Label();
            this.lblDetailTimeStampTitle = new System.Windows.Forms.Label();
            this.lblDetailThreadTitle = new System.Windows.Forms.Label();
            this.lblDetailDomainTitle = new System.Windows.Forms.Label();
            this.lblDetailMethodTitle = new System.Windows.Forms.Label();
            this.lblDetailClassTitle = new System.Windows.Forms.Label();
            this.lblDetailFileNameTitle = new System.Windows.Forms.Label();
            this.lblLineNumberTitle = new System.Windows.Forms.Label();
            this.lblDetailMessageTitle = new System.Windows.Forms.Label();
            this.lblDetailExceptionTitle = new System.Windows.Forms.Label();
            this.tbDetailLevel = new System.Windows.Forms.TextBox();
            this.tbDetailTimeStamp = new System.Windows.Forms.TextBox();
            this.tbDetailThread = new System.Windows.Forms.TextBox();
            this.tbDetailDomain = new System.Windows.Forms.TextBox();
            this.tbDetailClass = new System.Windows.Forms.TextBox();
            this.tbDetailMethod = new System.Windows.Forms.TextBox();
            this.tbDetailFileName = new System.Windows.Forms.TextBox();
            this.tbLineNumber = new System.Windows.Forms.TextBox();
            this.tbDetailMessage = new System.Windows.Forms.TextBox();
            this.tbDetailException = new System.Windows.Forms.TextBox();
            this.gbLogs = new System.Windows.Forms.GroupBox();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.eventNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeStampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loggerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.threadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.domainDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.classNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exceptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.cmsColumns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gbDetail.SuspendLayout();
            this.tlpDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLogs)).BeginInit();
            this.gbLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDetailLoggerTitle
            // 
            this.lblDetailLoggerTitle.AutoSize = true;
            this.lblDetailLoggerTitle.Location = new System.Drawing.Point(3, 32);
            this.lblDetailLoggerTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailLoggerTitle.Name = "lblDetailLoggerTitle";
            this.lblDetailLoggerTitle.Size = new System.Drawing.Size(43, 13);
            this.lblDetailLoggerTitle.TabIndex = 20;
            this.lblDetailLoggerTitle.Text = "Logger:";
            // 
            // gbDetail
            // 
            this.gbDetail.Controls.Add(this.tlpDetail);
            this.gbDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetail.Location = new System.Drawing.Point(0, 0);
            this.gbDetail.Name = "gbDetail";
            this.gbDetail.Size = new System.Drawing.Size(836, 243);
            this.gbDetail.TabIndex = 3;
            this.gbDetail.TabStop = false;
            this.gbDetail.Text = "Log Detail";
            // 
            // tlpDetail
            // 
            this.tlpDetail.AutoScroll = true;
            this.tlpDetail.AutoSize = true;
            this.tlpDetail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpDetail.ColumnCount = 8;
            this.tlpDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpDetail.Controls.Add(this.lblDetailLoggerTitle, 0, 1);
            this.tlpDetail.Controls.Add(this.tbDetailLogger, 1, 1);
            this.tlpDetail.Controls.Add(this.lblDetailLevelTitle, 0, 0);
            this.tlpDetail.Controls.Add(this.lblDetailTimeStampTitle, 2, 0);
            this.tlpDetail.Controls.Add(this.lblDetailThreadTitle, 4, 0);
            this.tlpDetail.Controls.Add(this.lblDetailDomainTitle, 6, 0);
            this.tlpDetail.Controls.Add(this.lblDetailMethodTitle, 4, 2);
            this.tlpDetail.Controls.Add(this.lblDetailClassTitle, 0, 2);
            this.tlpDetail.Controls.Add(this.lblDetailFileNameTitle, 0, 3);
            this.tlpDetail.Controls.Add(this.lblLineNumberTitle, 6, 3);
            this.tlpDetail.Controls.Add(this.lblDetailMessageTitle, 0, 4);
            this.tlpDetail.Controls.Add(this.lblDetailExceptionTitle, 4, 4);
            this.tlpDetail.Controls.Add(this.tbDetailLevel, 1, 0);
            this.tlpDetail.Controls.Add(this.tbDetailTimeStamp, 3, 0);
            this.tlpDetail.Controls.Add(this.tbDetailThread, 5, 0);
            this.tlpDetail.Controls.Add(this.tbDetailDomain, 7, 0);
            this.tlpDetail.Controls.Add(this.tbDetailClass, 1, 2);
            this.tlpDetail.Controls.Add(this.tbDetailMethod, 5, 2);
            this.tlpDetail.Controls.Add(this.tbDetailFileName, 1, 3);
            this.tlpDetail.Controls.Add(this.tbLineNumber, 7, 3);
            this.tlpDetail.Controls.Add(this.tbDetailMessage, 1, 4);
            this.tlpDetail.Controls.Add(this.tbDetailException, 5, 4);
            this.tlpDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDetail.Location = new System.Drawing.Point(3, 16);
            this.tlpDetail.Name = "tlpDetail";
            this.tlpDetail.RowCount = 5;
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpDetail.Size = new System.Drawing.Size(830, 224);
            this.tlpDetail.TabIndex = 0;
            // 
            // tbDetailLogger
            // 
            this.tlpDetail.SetColumnSpan(this.tbDetailLogger, 7);
            this.tbDetailLogger.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "Logger", true));
            this.tbDetailLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailLogger.Location = new System.Drawing.Point(64, 29);
            this.tbDetailLogger.Name = "tbDetailLogger";
            this.tbDetailLogger.Size = new System.Drawing.Size(763, 20);
            this.tbDetailLogger.TabIndex = 21;
            this.tbDetailLogger.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // bsLogs
            // 
            this.bsLogs.DataSource = typeof(Triamun.Log4NetViewer.Data.LogEvent);
            this.bsLogs.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bsLogs_ListChanged);
            // 
            // lblDetailLevelTitle
            // 
            this.lblDetailLevelTitle.AutoSize = true;
            this.lblDetailLevelTitle.Location = new System.Drawing.Point(3, 6);
            this.lblDetailLevelTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailLevelTitle.Name = "lblDetailLevelTitle";
            this.lblDetailLevelTitle.Size = new System.Drawing.Size(36, 13);
            this.lblDetailLevelTitle.TabIndex = 1;
            this.lblDetailLevelTitle.Text = "Level:";
            // 
            // lblDetailTimeStampTitle
            // 
            this.lblDetailTimeStampTitle.AutoSize = true;
            this.lblDetailTimeStampTitle.Location = new System.Drawing.Point(217, 6);
            this.lblDetailTimeStampTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailTimeStampTitle.Name = "lblDetailTimeStampTitle";
            this.lblDetailTimeStampTitle.Size = new System.Drawing.Size(33, 13);
            this.lblDetailTimeStampTitle.TabIndex = 1;
            this.lblDetailTimeStampTitle.Text = "Time:";
            // 
            // lblDetailThreadTitle
            // 
            this.lblDetailThreadTitle.AutoSize = true;
            this.lblDetailThreadTitle.Location = new System.Drawing.Point(409, 6);
            this.lblDetailThreadTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailThreadTitle.Name = "lblDetailThreadTitle";
            this.lblDetailThreadTitle.Size = new System.Drawing.Size(44, 13);
            this.lblDetailThreadTitle.TabIndex = 1;
            this.lblDetailThreadTitle.Text = "Thread:";
            // 
            // lblDetailDomainTitle
            // 
            this.lblDetailDomainTitle.AutoSize = true;
            this.lblDetailDomainTitle.Location = new System.Drawing.Point(625, 6);
            this.lblDetailDomainTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailDomainTitle.Name = "lblDetailDomainTitle";
            this.lblDetailDomainTitle.Size = new System.Drawing.Size(46, 13);
            this.lblDetailDomainTitle.TabIndex = 1;
            this.lblDetailDomainTitle.Text = "Domain:";
            // 
            // lblDetailMethodTitle
            // 
            this.lblDetailMethodTitle.AutoSize = true;
            this.lblDetailMethodTitle.Location = new System.Drawing.Point(409, 58);
            this.lblDetailMethodTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailMethodTitle.Name = "lblDetailMethodTitle";
            this.lblDetailMethodTitle.Size = new System.Drawing.Size(46, 13);
            this.lblDetailMethodTitle.TabIndex = 1;
            this.lblDetailMethodTitle.Text = "Method:";
            // 
            // lblDetailClassTitle
            // 
            this.lblDetailClassTitle.AutoSize = true;
            this.lblDetailClassTitle.Location = new System.Drawing.Point(3, 58);
            this.lblDetailClassTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailClassTitle.Name = "lblDetailClassTitle";
            this.lblDetailClassTitle.Size = new System.Drawing.Size(35, 13);
            this.lblDetailClassTitle.TabIndex = 1;
            this.lblDetailClassTitle.Text = "Class:";
            // 
            // lblDetailFileNameTitle
            // 
            this.lblDetailFileNameTitle.AutoSize = true;
            this.lblDetailFileNameTitle.Location = new System.Drawing.Point(3, 84);
            this.lblDetailFileNameTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailFileNameTitle.Name = "lblDetailFileNameTitle";
            this.lblDetailFileNameTitle.Size = new System.Drawing.Size(55, 13);
            this.lblDetailFileNameTitle.TabIndex = 1;
            this.lblDetailFileNameTitle.Text = "File name:";
            // 
            // lblLineNumberTitle
            // 
            this.lblLineNumberTitle.AutoSize = true;
            this.lblLineNumberTitle.Location = new System.Drawing.Point(625, 84);
            this.lblLineNumberTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblLineNumberTitle.Name = "lblLineNumberTitle";
            this.lblLineNumberTitle.Size = new System.Drawing.Size(30, 13);
            this.lblLineNumberTitle.TabIndex = 1;
            this.lblLineNumberTitle.Text = "Line:";
            // 
            // lblDetailMessageTitle
            // 
            this.lblDetailMessageTitle.AutoSize = true;
            this.lblDetailMessageTitle.Location = new System.Drawing.Point(3, 110);
            this.lblDetailMessageTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailMessageTitle.Name = "lblDetailMessageTitle";
            this.lblDetailMessageTitle.Size = new System.Drawing.Size(53, 13);
            this.lblDetailMessageTitle.TabIndex = 1;
            this.lblDetailMessageTitle.Text = "Message:";
            // 
            // lblDetailExceptionTitle
            // 
            this.lblDetailExceptionTitle.AutoSize = true;
            this.lblDetailExceptionTitle.Location = new System.Drawing.Point(409, 110);
            this.lblDetailExceptionTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lblDetailExceptionTitle.Name = "lblDetailExceptionTitle";
            this.lblDetailExceptionTitle.Size = new System.Drawing.Size(57, 13);
            this.lblDetailExceptionTitle.TabIndex = 1;
            this.lblDetailExceptionTitle.Text = "Exception:";
            // 
            // tbDetailLevel
            // 
            this.tbDetailLevel.BackColor = System.Drawing.SystemColors.Window;
            this.tbDetailLevel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "Level", true));
            this.tbDetailLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailLevel.Location = new System.Drawing.Point(64, 3);
            this.tbDetailLevel.Name = "tbDetailLevel";
            this.tbDetailLevel.ReadOnly = true;
            this.tbDetailLevel.Size = new System.Drawing.Size(147, 20);
            this.tbDetailLevel.TabIndex = 11;
            this.tbDetailLevel.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // tbDetailTimeStamp
            // 
            this.tbDetailTimeStamp.BackColor = System.Drawing.SystemColors.Window;
            this.tbDetailTimeStamp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "TimeStamp", true));
            this.tbDetailTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailTimeStamp.Location = new System.Drawing.Point(256, 3);
            this.tbDetailTimeStamp.Name = "tbDetailTimeStamp";
            this.tbDetailTimeStamp.ReadOnly = true;
            this.tbDetailTimeStamp.Size = new System.Drawing.Size(147, 20);
            this.tbDetailTimeStamp.TabIndex = 12;
            this.tbDetailTimeStamp.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // tbDetailThread
            // 
            this.tbDetailThread.BackColor = System.Drawing.SystemColors.Window;
            this.tbDetailThread.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "Thread", true));
            this.tbDetailThread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailThread.Location = new System.Drawing.Point(472, 3);
            this.tbDetailThread.Name = "tbDetailThread";
            this.tbDetailThread.ReadOnly = true;
            this.tbDetailThread.Size = new System.Drawing.Size(147, 20);
            this.tbDetailThread.TabIndex = 13;
            this.tbDetailThread.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // tbDetailDomain
            // 
            this.tbDetailDomain.BackColor = System.Drawing.SystemColors.Window;
            this.tbDetailDomain.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "Domain", true));
            this.tbDetailDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailDomain.Location = new System.Drawing.Point(677, 3);
            this.tbDetailDomain.Name = "tbDetailDomain";
            this.tbDetailDomain.ReadOnly = true;
            this.tbDetailDomain.Size = new System.Drawing.Size(150, 20);
            this.tbDetailDomain.TabIndex = 14;
            this.tbDetailDomain.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // tbDetailClass
            // 
            this.tbDetailClass.BackColor = System.Drawing.SystemColors.Window;
            this.tlpDetail.SetColumnSpan(this.tbDetailClass, 3);
            this.tbDetailClass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "ClassName", true));
            this.tbDetailClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailClass.Location = new System.Drawing.Point(64, 55);
            this.tbDetailClass.Name = "tbDetailClass";
            this.tbDetailClass.ReadOnly = true;
            this.tbDetailClass.Size = new System.Drawing.Size(339, 20);
            this.tbDetailClass.TabIndex = 15;
            this.tbDetailClass.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // tbDetailMethod
            // 
            this.tbDetailMethod.BackColor = System.Drawing.SystemColors.Window;
            this.tlpDetail.SetColumnSpan(this.tbDetailMethod, 3);
            this.tbDetailMethod.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "MethodName", true));
            this.tbDetailMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailMethod.Location = new System.Drawing.Point(472, 55);
            this.tbDetailMethod.Name = "tbDetailMethod";
            this.tbDetailMethod.ReadOnly = true;
            this.tbDetailMethod.Size = new System.Drawing.Size(355, 20);
            this.tbDetailMethod.TabIndex = 16;
            this.tbDetailMethod.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // tbDetailFileName
            // 
            this.tbDetailFileName.BackColor = System.Drawing.SystemColors.Window;
            this.tlpDetail.SetColumnSpan(this.tbDetailFileName, 5);
            this.tbDetailFileName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "FileName", true));
            this.tbDetailFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailFileName.Location = new System.Drawing.Point(64, 81);
            this.tbDetailFileName.Name = "tbDetailFileName";
            this.tbDetailFileName.ReadOnly = true;
            this.tbDetailFileName.Size = new System.Drawing.Size(555, 20);
            this.tbDetailFileName.TabIndex = 17;
            this.tbDetailFileName.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // tbLineNumber
            // 
            this.tbLineNumber.BackColor = System.Drawing.SystemColors.Window;
            this.tbLineNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "LineNumber", true));
            this.tbLineNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLineNumber.Location = new System.Drawing.Point(677, 81);
            this.tbLineNumber.Name = "tbLineNumber";
            this.tbLineNumber.ReadOnly = true;
            this.tbLineNumber.Size = new System.Drawing.Size(150, 20);
            this.tbLineNumber.TabIndex = 18;
            this.tbLineNumber.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // tbDetailMessage
            // 
            this.tbDetailMessage.BackColor = System.Drawing.SystemColors.Window;
            this.tlpDetail.SetColumnSpan(this.tbDetailMessage, 3);
            this.tbDetailMessage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "Message", true));
            this.tbDetailMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailMessage.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDetailMessage.Location = new System.Drawing.Point(64, 107);
            this.tbDetailMessage.Multiline = true;
            this.tbDetailMessage.Name = "tbDetailMessage";
            this.tbDetailMessage.ReadOnly = true;
            this.tbDetailMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDetailMessage.Size = new System.Drawing.Size(339, 114);
            this.tbDetailMessage.TabIndex = 19;
            this.tbDetailMessage.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // tbDetailException
            // 
            this.tbDetailException.BackColor = System.Drawing.SystemColors.Window;
            this.tlpDetail.SetColumnSpan(this.tbDetailException, 3);
            this.tbDetailException.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsLogs, "Exception", true));
            this.tbDetailException.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetailException.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDetailException.Location = new System.Drawing.Point(472, 107);
            this.tbDetailException.Multiline = true;
            this.tbDetailException.Name = "tbDetailException";
            this.tbDetailException.ReadOnly = true;
            this.tbDetailException.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDetailException.Size = new System.Drawing.Size(355, 114);
            this.tbDetailException.TabIndex = 20;
            this.tbDetailException.WordWrap = false;
            this.tbDetailException.DoubleClick += new System.EventHandler(this.tbDetail_DoubleClick);
            // 
            // gbLogs
            // 
            this.gbLogs.Controls.Add(this.dgvLogs);
            this.gbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLogs.Location = new System.Drawing.Point(0, 0);
            this.gbLogs.Name = "gbLogs";
            this.gbLogs.Size = new System.Drawing.Size(836, 213);
            this.gbLogs.TabIndex = 1;
            this.gbLogs.TabStop = false;
            this.gbLogs.Text = "Logs";
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.AllowUserToOrderColumns = true;
            this.dgvLogs.AutoGenerateColumns = false;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eventNumberDataGridViewTextBoxColumn,
            this.levelDataGridViewTextBoxColumn,
            this.timeStampDataGridViewTextBoxColumn,
            this.loggerDataGridViewTextBoxColumn,
            this.threadDataGridViewTextBoxColumn,
            this.domainDataGridViewTextBoxColumn,
            this.classNameDataGridViewTextBoxColumn,
            this.methodNameDataGridViewTextBoxColumn,
            this.fileNameDataGridViewTextBoxColumn,
            this.lineNumberDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn,
            this.exceptionDataGridViewTextBoxColumn});
            this.dgvLogs.DataSource = this.bsLogs;
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.Location = new System.Drawing.Point(3, 16);
            this.dgvLogs.MultiSelect = false;
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogs.ShowCellErrors = false;
            this.dgvLogs.ShowCellToolTips = false;
            this.dgvLogs.ShowEditingIcon = false;
            this.dgvLogs.ShowRowErrors = false;
            this.dgvLogs.Size = new System.Drawing.Size(830, 194);
            this.dgvLogs.TabIndex = 0;
            this.dgvLogs.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvLogs_CellPainting);
            // 
            // eventNumberDataGridViewTextBoxColumn
            // 
            this.eventNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.eventNumberDataGridViewTextBoxColumn.DataPropertyName = "EventNumber";
            this.eventNumberDataGridViewTextBoxColumn.HeaderText = "#";
            this.eventNumberDataGridViewTextBoxColumn.Name = "eventNumberDataGridViewTextBoxColumn";
            this.eventNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.eventNumberDataGridViewTextBoxColumn.Width = 60;
            // 
            // levelDataGridViewTextBoxColumn
            // 
            this.levelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.levelDataGridViewTextBoxColumn.DataPropertyName = "Level";
            this.levelDataGridViewTextBoxColumn.FillWeight = 10F;
            this.levelDataGridViewTextBoxColumn.HeaderText = "Level";
            this.levelDataGridViewTextBoxColumn.Name = "levelDataGridViewTextBoxColumn";
            this.levelDataGridViewTextBoxColumn.ReadOnly = true;
            this.levelDataGridViewTextBoxColumn.Width = 80;
            // 
            // timeStampDataGridViewTextBoxColumn
            // 
            this.timeStampDataGridViewTextBoxColumn.DataPropertyName = "TimeStamp";
            dataGridViewCellStyle1.Format = "G";
            dataGridViewCellStyle1.NullValue = null;
            this.timeStampDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.timeStampDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.timeStampDataGridViewTextBoxColumn.HeaderText = "Time";
            this.timeStampDataGridViewTextBoxColumn.Name = "timeStampDataGridViewTextBoxColumn";
            this.timeStampDataGridViewTextBoxColumn.ReadOnly = true;
            this.timeStampDataGridViewTextBoxColumn.Width = 114;
            // 
            // loggerDataGridViewTextBoxColumn
            // 
            this.loggerDataGridViewTextBoxColumn.DataPropertyName = "Logger";
            this.loggerDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.loggerDataGridViewTextBoxColumn.HeaderText = "Logger";
            this.loggerDataGridViewTextBoxColumn.Name = "loggerDataGridViewTextBoxColumn";
            this.loggerDataGridViewTextBoxColumn.ReadOnly = true;
            this.loggerDataGridViewTextBoxColumn.Width = 113;
            // 
            // threadDataGridViewTextBoxColumn
            // 
            this.threadDataGridViewTextBoxColumn.DataPropertyName = "Thread";
            this.threadDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.threadDataGridViewTextBoxColumn.HeaderText = "Thread";
            this.threadDataGridViewTextBoxColumn.Name = "threadDataGridViewTextBoxColumn";
            this.threadDataGridViewTextBoxColumn.ReadOnly = true;
            this.threadDataGridViewTextBoxColumn.Width = 114;
            // 
            // domainDataGridViewTextBoxColumn
            // 
            this.domainDataGridViewTextBoxColumn.DataPropertyName = "Domain";
            this.domainDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.domainDataGridViewTextBoxColumn.HeaderText = "Domain";
            this.domainDataGridViewTextBoxColumn.Name = "domainDataGridViewTextBoxColumn";
            this.domainDataGridViewTextBoxColumn.ReadOnly = true;
            this.domainDataGridViewTextBoxColumn.Visible = false;
            // 
            // classNameDataGridViewTextBoxColumn
            // 
            this.classNameDataGridViewTextBoxColumn.DataPropertyName = "ClassName";
            this.classNameDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.classNameDataGridViewTextBoxColumn.HeaderText = "Class";
            this.classNameDataGridViewTextBoxColumn.Name = "classNameDataGridViewTextBoxColumn";
            this.classNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.classNameDataGridViewTextBoxColumn.Width = 113;
            // 
            // methodNameDataGridViewTextBoxColumn
            // 
            this.methodNameDataGridViewTextBoxColumn.DataPropertyName = "MethodName";
            this.methodNameDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.methodNameDataGridViewTextBoxColumn.HeaderText = "Method";
            this.methodNameDataGridViewTextBoxColumn.Name = "methodNameDataGridViewTextBoxColumn";
            this.methodNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.methodNameDataGridViewTextBoxColumn.Width = 114;
            // 
            // fileNameDataGridViewTextBoxColumn
            // 
            this.fileNameDataGridViewTextBoxColumn.DataPropertyName = "FileName";
            this.fileNameDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.fileNameDataGridViewTextBoxColumn.HeaderText = "File";
            this.fileNameDataGridViewTextBoxColumn.Name = "fileNameDataGridViewTextBoxColumn";
            this.fileNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileNameDataGridViewTextBoxColumn.Visible = false;
            // 
            // lineNumberDataGridViewTextBoxColumn
            // 
            this.lineNumberDataGridViewTextBoxColumn.DataPropertyName = "LineNumber";
            this.lineNumberDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.lineNumberDataGridViewTextBoxColumn.HeaderText = "Line";
            this.lineNumberDataGridViewTextBoxColumn.Name = "lineNumberDataGridViewTextBoxColumn";
            this.lineNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.lineNumberDataGridViewTextBoxColumn.Visible = false;
            // 
            // userNameDataGridViewTextBoxColumn
            // 
            this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
            this.userNameDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.userNameDataGridViewTextBoxColumn.HeaderText = "User";
            this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
            this.userNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.userNameDataGridViewTextBoxColumn.Visible = false;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // exceptionDataGridViewTextBoxColumn
            // 
            this.exceptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.exceptionDataGridViewTextBoxColumn.DataPropertyName = "Exception";
            this.exceptionDataGridViewTextBoxColumn.FillWeight = 51.28205F;
            this.exceptionDataGridViewTextBoxColumn.HeaderText = "Exception";
            this.exceptionDataGridViewTextBoxColumn.Name = "exceptionDataGridViewTextBoxColumn";
            this.exceptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.exceptionDataGridViewTextBoxColumn.Visible = false;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.gbLogs);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.gbDetail);
            this.scMain.Size = new System.Drawing.Size(836, 460);
            this.scMain.SplitterDistance = 213;
            this.scMain.TabIndex = 1;
            // 
            // cmsColumns
            // 
            this.cmsColumns.Name = "cmsColumns";
            this.cmsColumns.ShowCheckMargin = true;
            this.cmsColumns.ShowImageMargin = false;
            this.cmsColumns.Size = new System.Drawing.Size(61, 4);
            // 
            // LogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Name = "LogViewer";
            this.Size = new System.Drawing.Size(836, 460);
            this.gbDetail.ResumeLayout(false);
            this.gbDetail.PerformLayout();
            this.tlpDetail.ResumeLayout(false);
            this.tlpDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsLogs)).EndInit();
            this.gbLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogs;
        private System.Windows.Forms.BindingSource bsLogs;
        private System.Windows.Forms.GroupBox gbDetail;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.TableLayoutPanel tlpDetail;
        private System.Windows.Forms.Label lblDetailLevelTitle;
        private System.Windows.Forms.Label lblDetailTimeStampTitle;
        private System.Windows.Forms.Label lblDetailThreadTitle;
        private System.Windows.Forms.Label lblDetailDomainTitle;
        private System.Windows.Forms.Label lblDetailFileNameTitle;
        private System.Windows.Forms.Label lblDetailMethodTitle;
        private System.Windows.Forms.Label lblDetailClassTitle;
        private System.Windows.Forms.Label lblLineNumberTitle;
        private System.Windows.Forms.Label lblDetailMessageTitle;
        private System.Windows.Forms.Label lblDetailExceptionTitle;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.TextBox tbDetailLevel;
        private System.Windows.Forms.TextBox tbDetailTimeStamp;
        private System.Windows.Forms.TextBox tbDetailThread;
        private System.Windows.Forms.TextBox tbDetailDomain;
        private System.Windows.Forms.TextBox tbDetailClass;
        private System.Windows.Forms.TextBox tbDetailMethod;
        private System.Windows.Forms.TextBox tbDetailFileName;
        private System.Windows.Forms.TextBox tbLineNumber;
        private System.Windows.Forms.TextBox tbDetailMessage;
        private System.Windows.Forms.TextBox tbDetailException;
        private System.Windows.Forms.ContextMenuStrip cmsColumns;
        private System.Windows.Forms.DataGridViewTextBoxColumn eventNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeStampDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loggerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn threadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn domainDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn classNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn methodNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exceptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox tbDetailLogger;
        private System.Windows.Forms.Label lblDetailLoggerTitle;
    }
}
