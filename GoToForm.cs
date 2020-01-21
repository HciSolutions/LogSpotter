using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Triamun.Log4NetViewer
{
    /// <summary>
    /// Allows the user to enter an event number
    /// </summary>
    public partial class GoToForm : Form
    {
		#region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="GoToForm"/> class.
        /// </summary>
        public GoToForm()
        {
            InitializeComponent();
        }

		#endregion Constructor 

		#region Public Properties 

        /// <summary>
        /// Gets or sets the selected event number.
        /// </summary>
        /// <value>The selected event number.</value>
        public int SelectedEventNumber
        {
            get { return (int)nudEventNumber.Value; }
            set { nudEventNumber.Value = value; }
        }

		#endregion Public Properties 

        /// <summary>
        /// Handles the Enter event of the nudEventNumber control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void nudEventNumber_Enter(object sender, EventArgs e)
        {
            nudEventNumber.Select(0, nudEventNumber.Text.Length);
        }
    }
}
