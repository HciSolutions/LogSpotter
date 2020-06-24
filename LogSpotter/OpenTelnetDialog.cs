using System.Windows.Forms;
using HciSolutions.LogSpotter.Data.Config;

namespace HciSolutions.LogSpotter
{
    /// <summary>
    /// Custom dialog used to open a new Telnet log.
    /// </summary>
    public partial class OpenTelnetDialog : Form
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTelnetDialog"/> class.
        /// </summary>
        public OpenTelnetDialog()
        {
            InitializeComponent();
            Config.Current.WindowPositions.LoadWindow(this);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the selected telnet server host name.
        /// </summary>
        /// <value>The selected telnet server host name.</value>
        public string HostName => tbHhost.Text;

        /// <summary>
        /// Gets the selected telnet server port number.
        /// </summary>
        /// <value>The selected telnet server port number.</value>
        public int PortNumber => (int)nudPort.Value;

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
