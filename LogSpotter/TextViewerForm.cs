using System;
using System.Windows.Forms;
using HciSolutions.LogSpotter.Data.Config;

namespace HciSolutions.LogSpotter
{
    /// <summary>
    /// Displays the detail of a text field.
    /// </summary>
    public partial class TextViewerForm : Form
    {
        #region Private Static Members
        private static TextViewerForm _instance;
        #endregion

        #region Constructor
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
            Config.Current.WindowPositions.LoadWindow(this);
        }
        #endregion

        #region Public Static Methods
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
        #endregion

        #region Protected Methods
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            Config.Current.WindowPositions.SaveWindow(this);
            Config.Save();
        }
        #endregion
    }
}
