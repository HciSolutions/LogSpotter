namespace Triamun.Log4NetViewer
{
    partial class GoToForm
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
            this.lblEventNumberTitle = new System.Windows.Forms.Label();
            this.nudEventNumber = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudEventNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEventNumberTitle
            // 
            this.lblEventNumberTitle.AutoSize = true;
            this.lblEventNumberTitle.Location = new System.Drawing.Point(13, 13);
            this.lblEventNumberTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblEventNumberTitle.Name = "lblEventNumberTitle";
            this.lblEventNumberTitle.Size = new System.Drawing.Size(104, 13);
            this.lblEventNumberTitle.TabIndex = 0;
            this.lblEventNumberTitle.Text = "Go to event number:";
            // 
            // nudEventNumber
            // 
            this.nudEventNumber.Location = new System.Drawing.Point(16, 32);
            this.nudEventNumber.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudEventNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEventNumber.Name = "nudEventNumber";
            this.nudEventNumber.Size = new System.Drawing.Size(156, 20);
            this.nudEventNumber.TabIndex = 0;
            this.nudEventNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEventNumber.Enter += new System.EventHandler(this.nudEventNumber_Enter);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(16, 58);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(97, 58);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // GoToForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(184, 93);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.nudEventNumber);
            this.Controls.Add(this.lblEventNumberTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GoToForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Go to";
            ((System.ComponentModel.ISupportInitialize)(this.nudEventNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEventNumberTitle;
        private System.Windows.Forms.NumericUpDown nudEventNumber;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}