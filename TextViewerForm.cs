using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Triamun.Log4NetViewer.Data.Config;

namespace Triamun.Log4NetViewer
{
    /// <summary>
    /// Displays the detail of a text field.
    /// </summary>
    public partial class TextViewerForm : Form
    {
        private static TextViewerForm _instance;

        /// <summary>
        /// Initializes the <see cref="TextViewerForm"/> class.
        /// </summary>
        static TextViewerForm()
        {
            _instance = new TextViewerForm();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextViewerForm"/> class.
        /// </summary>
        public TextViewerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the text viewer.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="label">The label.</param>
        /// <param name="content">The content.</param>
        public static void ShowTextViewer(IWin32Window ownerWindow, string label, string content)
        {
            if (_instance.Visible)
                throw new InvalidOperationException();

            _instance.Text = label;
            _instance.tbContent.Text = content;

            _instance.ShowDialog(ownerWindow);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            Config.Save();
        }
    }
}
