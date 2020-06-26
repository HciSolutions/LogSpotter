using HciSolutions.LogSpotter.Data.Config;

namespace HciSolutions.LogSpotter
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpColors = new System.Windows.Forms.TabPage();
            this.tlpColors = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogDebugTitle = new System.Windows.Forms.Label();
            this.lblLogInfoTitle = new System.Windows.Forms.Label();
            this.lblLogWarningTitle = new System.Windows.Forms.Label();
            this.lblLogErrorTitle = new System.Windows.Forms.Label();
            this.lblLogFatalTitle = new System.Windows.Forms.Label();
            this.lblLogBackgroundTitle = new System.Windows.Forms.Label();
            this.lblLogForegroundTitle = new System.Windows.Forms.Label();
            this.pnlLogDebugBackgroundColor = new System.Windows.Forms.Panel();
            this.pnlLogDebugForegroundColor = new System.Windows.Forms.Panel();
            this.pnlLogInfoForegroundColor = new System.Windows.Forms.Panel();
            this.pnlLogWarningForegroundColor = new System.Windows.Forms.Panel();
            this.pnlLogErrorForegroundColor = new System.Windows.Forms.Panel();
            this.pnlLogFatalForegroundColor = new System.Windows.Forms.Panel();
            this.pnlLogFatalBackgroundColor = new System.Windows.Forms.Panel();
            this.pnlLogErrorBackgroundColor = new System.Windows.Forms.Panel();
            this.pnlLogWarningBackgroundColor = new System.Windows.Forms.Panel();
            this.pnlLogInfoBackgroundColor = new System.Windows.Forms.Panel();
            this.lbLogDebugSample = new System.Windows.Forms.Label();
            this.lblLogInfoSample = new System.Windows.Forms.Label();
            this.lblLogWarningSample = new System.Windows.Forms.Label();
            this.lblLogErrorSample = new System.Windows.Forms.Label();
            this.lblLogFatalSample = new System.Windows.Forms.Label();
            this.lblLogTraceTitle = new System.Windows.Forms.Label();
            this.pnlLogTraceBackgroundColor = new System.Windows.Forms.Panel();
            this.pnlLogTraceForegroundColor = new System.Windows.Forms.Panel();
            this.lbLogTraceSample = new System.Windows.Forms.Label();
            this.tpMisc = new System.Windows.Forms.TabPage();
            this.tlpMisc = new System.Windows.Forms.TableLayoutPanel();
            this.lblMiscMaxEventsTitle = new System.Windows.Forms.Label();
            this.nudMiscMaxEvents = new System.Windows.Forms.NumericUpDown();
            this.lblShowMiliseconds = new System.Windows.Forms.Label();
            this.cbShowMiliseconds = new System.Windows.Forms.CheckBox();
            this.cdColor = new System.Windows.Forms.ColorDialog();
            this.bsColorDebug = new System.Windows.Forms.BindingSource(this.components);
            this.bsColorInfo = new System.Windows.Forms.BindingSource(this.components);
            this.bsColorWarning = new System.Windows.Forms.BindingSource(this.components);
            this.bsColorError = new System.Windows.Forms.BindingSource(this.components);
            this.bsColorFatal = new System.Windows.Forms.BindingSource(this.components);
            this.bsConfig = new System.Windows.Forms.BindingSource(this.components);
            this.bsColorTrace = new System.Windows.Forms.BindingSource(this.components);
            this.tlpMain.SuspendLayout();
            this.flpButtons.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpColors.SuspendLayout();
            this.tlpColors.SuspendLayout();
            this.tpMisc.SuspendLayout();
            this.tlpMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMiscMaxEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorDebug)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorFatal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorTrace)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.flpButtons, 0, 1);
            this.tlpMain.Controls.Add(this.tcMain, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(10, 10);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(306, 219);
            this.tlpMain.TabIndex = 0;
            // 
            // flpButtons
            // 
            this.flpButtons.AutoSize = true;
            this.flpButtons.Controls.Add(this.btnOK);
            this.flpButtons.Controls.Add(this.btnCancel);
            this.flpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpButtons.Location = new System.Drawing.Point(0, 190);
            this.flpButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.Size = new System.Drawing.Size(306, 29);
            this.flpButtons.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(84, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpColors);
            this.tcMain.Controls.Add(this.tpMisc);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(3, 3);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(300, 184);
            this.tcMain.TabIndex = 1;
            // 
            // tpColors
            // 
            this.tpColors.Controls.Add(this.tlpColors);
            this.tpColors.Location = new System.Drawing.Point(4, 22);
            this.tpColors.Name = "tpColors";
            this.tpColors.Padding = new System.Windows.Forms.Padding(3);
            this.tpColors.Size = new System.Drawing.Size(292, 158);
            this.tpColors.TabIndex = 0;
            this.tpColors.Text = "Log level colors";
            this.tpColors.UseVisualStyleBackColor = true;
            // 
            // tlpColors
            // 
            this.tlpColors.ColumnCount = 4;
            this.tlpColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpColors.Controls.Add(this.lblLogDebugTitle, 0, 2);
            this.tlpColors.Controls.Add(this.lblLogInfoTitle, 0, 3);
            this.tlpColors.Controls.Add(this.lblLogWarningTitle, 0, 4);
            this.tlpColors.Controls.Add(this.lblLogErrorTitle, 0, 5);
            this.tlpColors.Controls.Add(this.lblLogFatalTitle, 0, 6);
            this.tlpColors.Controls.Add(this.lblLogBackgroundTitle, 1, 0);
            this.tlpColors.Controls.Add(this.lblLogForegroundTitle, 2, 0);
            this.tlpColors.Controls.Add(this.pnlLogDebugBackgroundColor, 1, 2);
            this.tlpColors.Controls.Add(this.pnlLogDebugForegroundColor, 2, 2);
            this.tlpColors.Controls.Add(this.pnlLogInfoForegroundColor, 2, 3);
            this.tlpColors.Controls.Add(this.pnlLogWarningForegroundColor, 2, 4);
            this.tlpColors.Controls.Add(this.pnlLogErrorForegroundColor, 2, 5);
            this.tlpColors.Controls.Add(this.pnlLogFatalForegroundColor, 2, 6);
            this.tlpColors.Controls.Add(this.pnlLogFatalBackgroundColor, 1, 6);
            this.tlpColors.Controls.Add(this.pnlLogErrorBackgroundColor, 1, 5);
            this.tlpColors.Controls.Add(this.pnlLogWarningBackgroundColor, 1, 4);
            this.tlpColors.Controls.Add(this.pnlLogInfoBackgroundColor, 1, 3);
            this.tlpColors.Controls.Add(this.lbLogDebugSample, 3, 2);
            this.tlpColors.Controls.Add(this.lblLogInfoSample, 3, 3);
            this.tlpColors.Controls.Add(this.lblLogWarningSample, 3, 4);
            this.tlpColors.Controls.Add(this.lblLogErrorSample, 3, 5);
            this.tlpColors.Controls.Add(this.lblLogFatalSample, 3, 6);
            this.tlpColors.Controls.Add(this.lblLogTraceTitle, 0, 1);
            this.tlpColors.Controls.Add(this.pnlLogTraceBackgroundColor, 1, 1);
            this.tlpColors.Controls.Add(this.pnlLogTraceForegroundColor, 2, 1);
            this.tlpColors.Controls.Add(this.lbLogTraceSample, 3, 1);
            this.tlpColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpColors.Location = new System.Drawing.Point(3, 3);
            this.tlpColors.Name = "tlpColors";
            this.tlpColors.RowCount = 7;
            this.tlpColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpColors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpColors.Size = new System.Drawing.Size(286, 152);
            this.tlpColors.TabIndex = 0;
            // 
            // lblLogDebugTitle
            // 
            this.lblLogDebugTitle.AutoSize = true;
            this.lblLogDebugTitle.Location = new System.Drawing.Point(3, 44);
            this.lblLogDebugTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogDebugTitle.Name = "lblLogDebugTitle";
            this.lblLogDebugTitle.Size = new System.Drawing.Size(39, 13);
            this.lblLogDebugTitle.TabIndex = 0;
            this.lblLogDebugTitle.Text = "Debug";
            // 
            // lblLogInfoTitle
            // 
            this.lblLogInfoTitle.AutoSize = true;
            this.lblLogInfoTitle.Location = new System.Drawing.Point(3, 65);
            this.lblLogInfoTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogInfoTitle.Name = "lblLogInfoTitle";
            this.lblLogInfoTitle.Size = new System.Drawing.Size(25, 13);
            this.lblLogInfoTitle.TabIndex = 0;
            this.lblLogInfoTitle.Text = "Info";
            // 
            // lblLogWarningTitle
            // 
            this.lblLogWarningTitle.AutoSize = true;
            this.lblLogWarningTitle.Location = new System.Drawing.Point(3, 86);
            this.lblLogWarningTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogWarningTitle.Name = "lblLogWarningTitle";
            this.lblLogWarningTitle.Size = new System.Drawing.Size(47, 13);
            this.lblLogWarningTitle.TabIndex = 0;
            this.lblLogWarningTitle.Text = "Warning";
            // 
            // lblLogErrorTitle
            // 
            this.lblLogErrorTitle.AutoSize = true;
            this.lblLogErrorTitle.Location = new System.Drawing.Point(3, 107);
            this.lblLogErrorTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogErrorTitle.Name = "lblLogErrorTitle";
            this.lblLogErrorTitle.Size = new System.Drawing.Size(29, 13);
            this.lblLogErrorTitle.TabIndex = 0;
            this.lblLogErrorTitle.Text = "Error";
            // 
            // lblLogFatalTitle
            // 
            this.lblLogFatalTitle.AutoSize = true;
            this.lblLogFatalTitle.Location = new System.Drawing.Point(3, 128);
            this.lblLogFatalTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogFatalTitle.Name = "lblLogFatalTitle";
            this.lblLogFatalTitle.Size = new System.Drawing.Size(30, 13);
            this.lblLogFatalTitle.TabIndex = 0;
            this.lblLogFatalTitle.Text = "Fatal";
            // 
            // lblLogBackgroundTitle
            // 
            this.lblLogBackgroundTitle.AutoSize = true;
            this.lblLogBackgroundTitle.Location = new System.Drawing.Point(56, 3);
            this.lblLogBackgroundTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogBackgroundTitle.Name = "lblLogBackgroundTitle";
            this.lblLogBackgroundTitle.Size = new System.Drawing.Size(68, 14);
            this.lblLogBackgroundTitle.TabIndex = 0;
            this.lblLogBackgroundTitle.Text = "Background color";
            // 
            // lblLogForegroundTitle
            // 
            this.lblLogForegroundTitle.AutoSize = true;
            this.lblLogForegroundTitle.Location = new System.Drawing.Point(137, 3);
            this.lblLogForegroundTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogForegroundTitle.Name = "lblLogForegroundTitle";
            this.lblLogForegroundTitle.Size = new System.Drawing.Size(64, 14);
            this.lblLogForegroundTitle.TabIndex = 0;
            this.lblLogForegroundTitle.Text = "Foreground color";
            // 
            // pnlLogDebugBackgroundColor
            // 
            this.pnlLogDebugBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogDebugBackgroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorDebug, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogDebugBackgroundColor.Location = new System.Drawing.Point(56, 44);
            this.pnlLogDebugBackgroundColor.Name = "pnlLogDebugBackgroundColor";
            this.pnlLogDebugBackgroundColor.Size = new System.Drawing.Size(71, 15);
            this.pnlLogDebugBackgroundColor.TabIndex = 1;
            this.pnlLogDebugBackgroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogDebugForegroundColor
            // 
            this.pnlLogDebugForegroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogDebugForegroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorDebug, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogDebugForegroundColor.Location = new System.Drawing.Point(137, 44);
            this.pnlLogDebugForegroundColor.Name = "pnlLogDebugForegroundColor";
            this.pnlLogDebugForegroundColor.Size = new System.Drawing.Size(71, 15);
            this.pnlLogDebugForegroundColor.TabIndex = 1;
            this.pnlLogDebugForegroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogInfoForegroundColor
            // 
            this.pnlLogInfoForegroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogInfoForegroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorInfo, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogInfoForegroundColor.Location = new System.Drawing.Point(137, 65);
            this.pnlLogInfoForegroundColor.Name = "pnlLogInfoForegroundColor";
            this.pnlLogInfoForegroundColor.Size = new System.Drawing.Size(71, 15);
            this.pnlLogInfoForegroundColor.TabIndex = 1;
            this.pnlLogInfoForegroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogWarningForegroundColor
            // 
            this.pnlLogWarningForegroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogWarningForegroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorWarning, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogWarningForegroundColor.Location = new System.Drawing.Point(137, 86);
            this.pnlLogWarningForegroundColor.Name = "pnlLogWarningForegroundColor";
            this.pnlLogWarningForegroundColor.Size = new System.Drawing.Size(71, 15);
            this.pnlLogWarningForegroundColor.TabIndex = 1;
            this.pnlLogWarningForegroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogErrorForegroundColor
            // 
            this.pnlLogErrorForegroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogErrorForegroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorError, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogErrorForegroundColor.Location = new System.Drawing.Point(137, 107);
            this.pnlLogErrorForegroundColor.Name = "pnlLogErrorForegroundColor";
            this.pnlLogErrorForegroundColor.Size = new System.Drawing.Size(71, 15);
            this.pnlLogErrorForegroundColor.TabIndex = 1;
            this.pnlLogErrorForegroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogFatalForegroundColor
            // 
            this.pnlLogFatalForegroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogFatalForegroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorFatal, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogFatalForegroundColor.Location = new System.Drawing.Point(137, 128);
            this.pnlLogFatalForegroundColor.Name = "pnlLogFatalForegroundColor";
            this.pnlLogFatalForegroundColor.Size = new System.Drawing.Size(71, 16);
            this.pnlLogFatalForegroundColor.TabIndex = 1;
            this.pnlLogFatalForegroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogFatalBackgroundColor
            // 
            this.pnlLogFatalBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogFatalBackgroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorFatal, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogFatalBackgroundColor.Location = new System.Drawing.Point(56, 128);
            this.pnlLogFatalBackgroundColor.Name = "pnlLogFatalBackgroundColor";
            this.pnlLogFatalBackgroundColor.Size = new System.Drawing.Size(71, 16);
            this.pnlLogFatalBackgroundColor.TabIndex = 1;
            this.pnlLogFatalBackgroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogErrorBackgroundColor
            // 
            this.pnlLogErrorBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogErrorBackgroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorError, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogErrorBackgroundColor.Location = new System.Drawing.Point(56, 107);
            this.pnlLogErrorBackgroundColor.Name = "pnlLogErrorBackgroundColor";
            this.pnlLogErrorBackgroundColor.Size = new System.Drawing.Size(71, 15);
            this.pnlLogErrorBackgroundColor.TabIndex = 1;
            this.pnlLogErrorBackgroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogWarningBackgroundColor
            // 
            this.pnlLogWarningBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogWarningBackgroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorWarning, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogWarningBackgroundColor.Location = new System.Drawing.Point(56, 86);
            this.pnlLogWarningBackgroundColor.Name = "pnlLogWarningBackgroundColor";
            this.pnlLogWarningBackgroundColor.Size = new System.Drawing.Size(71, 15);
            this.pnlLogWarningBackgroundColor.TabIndex = 1;
            this.pnlLogWarningBackgroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogInfoBackgroundColor
            // 
            this.pnlLogInfoBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogInfoBackgroundColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorInfo, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pnlLogInfoBackgroundColor.Location = new System.Drawing.Point(56, 65);
            this.pnlLogInfoBackgroundColor.Name = "pnlLogInfoBackgroundColor";
            this.pnlLogInfoBackgroundColor.Size = new System.Drawing.Size(71, 15);
            this.pnlLogInfoBackgroundColor.TabIndex = 1;
            this.pnlLogInfoBackgroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // lbLogDebugSample
            // 
            this.lbLogDebugSample.AutoSize = true;
            this.lbLogDebugSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLogDebugSample.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorDebug, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lbLogDebugSample.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", this.bsColorDebug, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lbLogDebugSample.Location = new System.Drawing.Point(218, 44);
            this.lbLogDebugSample.Margin = new System.Windows.Forms.Padding(3);
            this.lbLogDebugSample.Name = "lbLogDebugSample";
            this.lbLogDebugSample.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lbLogDebugSample.Size = new System.Drawing.Size(64, 15);
            this.lbLogDebugSample.TabIndex = 0;
            this.lbLogDebugSample.Text = "Sample";
            // 
            // lblLogInfoSample
            // 
            this.lblLogInfoSample.AutoSize = true;
            this.lblLogInfoSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogInfoSample.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorInfo, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lblLogInfoSample.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", this.bsColorInfo, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lblLogInfoSample.Location = new System.Drawing.Point(218, 65);
            this.lblLogInfoSample.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogInfoSample.Name = "lblLogInfoSample";
            this.lblLogInfoSample.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblLogInfoSample.Size = new System.Drawing.Size(64, 15);
            this.lblLogInfoSample.TabIndex = 0;
            this.lblLogInfoSample.Text = "Sample";
            // 
            // lblLogWarningSample
            // 
            this.lblLogWarningSample.AutoSize = true;
            this.lblLogWarningSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogWarningSample.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorWarning, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lblLogWarningSample.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", this.bsColorWarning, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lblLogWarningSample.Location = new System.Drawing.Point(218, 86);
            this.lblLogWarningSample.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogWarningSample.Name = "lblLogWarningSample";
            this.lblLogWarningSample.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblLogWarningSample.Size = new System.Drawing.Size(64, 15);
            this.lblLogWarningSample.TabIndex = 0;
            this.lblLogWarningSample.Text = "Sample";
            // 
            // lblLogErrorSample
            // 
            this.lblLogErrorSample.AutoSize = true;
            this.lblLogErrorSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogErrorSample.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorError, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lblLogErrorSample.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", this.bsColorError, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lblLogErrorSample.Location = new System.Drawing.Point(218, 107);
            this.lblLogErrorSample.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogErrorSample.Name = "lblLogErrorSample";
            this.lblLogErrorSample.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblLogErrorSample.Size = new System.Drawing.Size(64, 15);
            this.lblLogErrorSample.TabIndex = 0;
            this.lblLogErrorSample.Text = "Sample";
            // 
            // lblLogFatalSample
            // 
            this.lblLogFatalSample.AutoSize = true;
            this.lblLogFatalSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogFatalSample.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", this.bsColorFatal, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lblLogFatalSample.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", this.bsColorFatal, "ForegroundColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lblLogFatalSample.Location = new System.Drawing.Point(218, 128);
            this.lblLogFatalSample.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogFatalSample.Name = "lblLogFatalSample";
            this.lblLogFatalSample.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblLogFatalSample.Size = new System.Drawing.Size(64, 15);
            this.lblLogFatalSample.TabIndex = 0;
            this.lblLogFatalSample.Text = "Sample";
            // 
            // lblLogTraceTitle
            // 
            this.lblLogTraceTitle.AutoSize = true;
            this.lblLogTraceTitle.Location = new System.Drawing.Point(3, 23);
            this.lblLogTraceTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogTraceTitle.Name = "lblLogTraceTitle";
            this.lblLogTraceTitle.Size = new System.Drawing.Size(35, 13);
            this.lblLogTraceTitle.TabIndex = 0;
            this.lblLogTraceTitle.Text = "Trace";
            // 
            // pnlLogTraceBackgroundColor
            // 
            this.pnlLogTraceBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogTraceBackgroundColor.Location = new System.Drawing.Point(56, 23);
            this.pnlLogTraceBackgroundColor.Name = "pnlLogTraceBackgroundColor";
            this.pnlLogTraceBackgroundColor.Size = new System.Drawing.Size(71, 14);
            this.pnlLogTraceBackgroundColor.TabIndex = 1;
            this.pnlLogTraceBackgroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // pnlLogTraceForegroundColor
            // 
            this.pnlLogTraceForegroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogTraceForegroundColor.Location = new System.Drawing.Point(137, 23);
            this.pnlLogTraceForegroundColor.Name = "pnlLogTraceForegroundColor";
            this.pnlLogTraceForegroundColor.Size = new System.Drawing.Size(71, 14);
            this.pnlLogTraceForegroundColor.TabIndex = 1;
            this.pnlLogTraceForegroundColor.Click += new System.EventHandler(this.pnlLogColor_Click);
            // 
            // lbLogTraceSample
            // 
            this.lbLogTraceSample.AutoSize = true;
            this.lbLogTraceSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbLogTraceSample.Location = new System.Drawing.Point(218, 23);
            this.lbLogTraceSample.Margin = new System.Windows.Forms.Padding(3);
            this.lbLogTraceSample.Name = "lbLogTraceSample";
            this.lbLogTraceSample.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lbLogTraceSample.Size = new System.Drawing.Size(64, 15);
            this.lbLogTraceSample.TabIndex = 0;
            this.lbLogTraceSample.Text = "Sample";
            // 
            // tpMisc
            // 
            this.tpMisc.Controls.Add(this.tlpMisc);
            this.tpMisc.Location = new System.Drawing.Point(4, 22);
            this.tpMisc.Name = "tpMisc";
            this.tpMisc.Padding = new System.Windows.Forms.Padding(3);
            this.tpMisc.Size = new System.Drawing.Size(292, 158);
            this.tpMisc.TabIndex = 1;
            this.tpMisc.Text = "Misc";
            this.tpMisc.UseVisualStyleBackColor = true;
            // 
            // tlpMisc
            // 
            this.tlpMisc.ColumnCount = 2;
            this.tlpMisc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMisc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMisc.Controls.Add(this.lblMiscMaxEventsTitle, 0, 0);
            this.tlpMisc.Controls.Add(this.nudMiscMaxEvents, 1, 0);
            this.tlpMisc.Controls.Add(this.lblShowMiliseconds, 0, 1);
            this.tlpMisc.Controls.Add(this.cbShowMiliseconds, 1, 1);
            this.tlpMisc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMisc.Location = new System.Drawing.Point(3, 3);
            this.tlpMisc.Name = "tlpMisc";
            this.tlpMisc.RowCount = 3;
            this.tlpMisc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMisc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMisc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMisc.Size = new System.Drawing.Size(286, 152);
            this.tlpMisc.TabIndex = 0;
            // 
            // lblMiscMaxEventsTitle
            // 
            this.lblMiscMaxEventsTitle.AutoSize = true;
            this.lblMiscMaxEventsTitle.Location = new System.Drawing.Point(3, 5);
            this.lblMiscMaxEventsTitle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.lblMiscMaxEventsTitle.Name = "lblMiscMaxEventsTitle";
            this.lblMiscMaxEventsTitle.Size = new System.Drawing.Size(115, 13);
            this.lblMiscMaxEventsTitle.TabIndex = 1;
            this.lblMiscMaxEventsTitle.Text = "Max events in memory:";
            // 
            // nudMiscMaxEvents
            // 
            this.nudMiscMaxEvents.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsConfig, "MaxEvents", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.nudMiscMaxEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudMiscMaxEvents.Location = new System.Drawing.Point(124, 3);
            this.nudMiscMaxEvents.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.nudMiscMaxEvents.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMiscMaxEvents.Name = "nudMiscMaxEvents";
            this.nudMiscMaxEvents.Size = new System.Drawing.Size(159, 20);
            this.nudMiscMaxEvents.TabIndex = 2;
            this.nudMiscMaxEvents.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // lblShowMiliseconds
            // 
            this.lblShowMiliseconds.AutoSize = true;
            this.lblShowMiliseconds.Location = new System.Drawing.Point(3, 35);
            this.lblShowMiliseconds.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.lblShowMiliseconds.Name = "lblShowMiliseconds";
            this.lblShowMiliseconds.Size = new System.Drawing.Size(91, 13);
            this.lblShowMiliseconds.TabIndex = 1;
            this.lblShowMiliseconds.Text = "Show miliseconds";
            // 
            // cbShowMiliseconds
            // 
            this.cbShowMiliseconds.AutoSize = true;
            this.cbShowMiliseconds.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsConfig, "ShowMilliseconds", true));
            this.cbShowMiliseconds.Location = new System.Drawing.Point(124, 33);
            this.cbShowMiliseconds.Name = "cbShowMiliseconds";
            this.cbShowMiliseconds.Size = new System.Drawing.Size(15, 14);
            this.cbShowMiliseconds.TabIndex = 3;
            this.cbShowMiliseconds.UseVisualStyleBackColor = true;
            // 
            // bsColorDebug
            // 
            this.bsColorDebug.DataSource = typeof(HciSolutions.LogSpotter.Data.Config.EventColor);
            // 
            // bsColorInfo
            // 
            this.bsColorInfo.DataSource = typeof(HciSolutions.LogSpotter.Data.Config.EventColor);
            // 
            // bsColorWarning
            // 
            this.bsColorWarning.DataSource = typeof(HciSolutions.LogSpotter.Data.Config.EventColor);
            // 
            // bsColorError
            // 
            this.bsColorError.DataSource = typeof(HciSolutions.LogSpotter.Data.Config.EventColor);
            // 
            // bsColorFatal
            // 
            this.bsColorFatal.DataSource = typeof(HciSolutions.LogSpotter.Data.Config.EventColor);
            // 
            // bsConfig
            // 
            this.bsConfig.DataSource = typeof(HciSolutions.LogSpotter.Data.Config.Config);
            // 
            // bsColorTrace
            // 
            this.bsColorTrace.DataSource = typeof(HciSolutions.LogSpotter.Data.Config.EventColor);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(326, 239);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Settings";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.flpButtons.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpColors.ResumeLayout(false);
            this.tlpColors.ResumeLayout(false);
            this.tlpColors.PerformLayout();
            this.tpMisc.ResumeLayout(false);
            this.tlpMisc.ResumeLayout(false);
            this.tlpMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMiscMaxEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorDebug)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorFatal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsColorTrace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.FlowLayoutPanel flpButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TableLayoutPanel tlpColors;
        private System.Windows.Forms.Label lblLogDebugTitle;
        private System.Windows.Forms.Label lblLogInfoTitle;
        private System.Windows.Forms.Label lblLogWarningTitle;
        private System.Windows.Forms.Label lblLogErrorTitle;
        private System.Windows.Forms.Label lblLogFatalTitle;
        private System.Windows.Forms.Label lblLogForegroundTitle;
        private System.Windows.Forms.Label lblLogBackgroundTitle;
        private System.Windows.Forms.Panel pnlLogDebugBackgroundColor;
        private System.Windows.Forms.Panel pnlLogDebugForegroundColor;
        private System.Windows.Forms.Panel pnlLogInfoForegroundColor;
        private System.Windows.Forms.Panel pnlLogWarningForegroundColor;
        private System.Windows.Forms.Panel pnlLogErrorForegroundColor;
        private System.Windows.Forms.Panel pnlLogFatalForegroundColor;
        private System.Windows.Forms.Panel pnlLogFatalBackgroundColor;
        private System.Windows.Forms.Panel pnlLogErrorBackgroundColor;
        private System.Windows.Forms.Panel pnlLogWarningBackgroundColor;
        private System.Windows.Forms.Panel pnlLogInfoBackgroundColor;
        private System.Windows.Forms.ColorDialog cdColor;
        private System.Windows.Forms.BindingSource bsColorDebug;
        private System.Windows.Forms.BindingSource bsColorInfo;
        private System.Windows.Forms.BindingSource bsColorWarning;
        private System.Windows.Forms.BindingSource bsColorError;
        private System.Windows.Forms.BindingSource bsColorFatal;
        private System.Windows.Forms.Label lbLogDebugSample;
        private System.Windows.Forms.Label lblLogInfoSample;
        private System.Windows.Forms.Label lblLogWarningSample;
        private System.Windows.Forms.Label lblLogErrorSample;
        private System.Windows.Forms.Label lblLogFatalSample;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpColors;
        private System.Windows.Forms.TabPage tpMisc;
        private System.Windows.Forms.TableLayoutPanel tlpMisc;
        private System.Windows.Forms.Label lblMiscMaxEventsTitle;
        private System.Windows.Forms.NumericUpDown nudMiscMaxEvents;
        private System.Windows.Forms.BindingSource bsConfig;
        private System.Windows.Forms.Label lblLogTraceTitle;
        private System.Windows.Forms.Panel pnlLogTraceBackgroundColor;
        private System.Windows.Forms.Panel pnlLogTraceForegroundColor;
        private System.Windows.Forms.Label lbLogTraceSample;
        private System.Windows.Forms.Label lblShowMiliseconds;
        private System.Windows.Forms.CheckBox cbShowMiliseconds;
        private System.Windows.Forms.BindingSource bsColorTrace;
    }
}