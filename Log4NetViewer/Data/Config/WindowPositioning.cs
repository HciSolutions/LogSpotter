using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;

namespace Triamun.Log4NetViewer.Data.Config
{
    /// <summary>
    /// Contains the configuration of a window.
    /// </summary>
    public class WindowPositioning : IComparable<WindowPositioning>
    {
        #region Private Members
        private int _y;
        private int _x;
        private int _width;
        private int _height;
        private string _name;
        private FormWindowState _windowState;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowConfig"/> class.
        /// </summary>
        public WindowPositioning()
        {
            _y = 0;
            _x = 0;
            _width = 0;
            _height = 0;
            _name = null;
            _windowState = FormWindowState.Normal;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the window.
        /// </summary>
        /// <value>The name of the window.</value>
        [XmlAttribute("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        /// <summary>
        /// Gets or sets the X coordinate of the top-left window corner.
        /// </summary>
        /// <value>The X coordinate of the top-left window corner.</value>
        [XmlAttribute("x")]
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the top-left window corner.
        /// </summary>
        /// <value>The Y coordinate of the top-left window corner.</value>
        [XmlAttribute("y")]
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        /// <value>The width of the window.</value>
        [XmlAttribute("width")]
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        /// <value>The height of the window.</value>
        [XmlAttribute("height")]
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        /// <value>The state of the window.</value>
        [XmlAttribute("state")]
        public FormWindowState WindowState
        {
            get { return _windowState; }
            set { _windowState = value; }
        }
        #endregion

        #region IComparable<WindowPositioning> Members
        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Less than zero: This object is less than the <paramref name="other"/> parameter.
        /// Zero: This object is equal to <paramref name="other"/>.
        /// Greater than zero:This object is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(WindowPositioning other)
        {
            if (other == null)
                return 1;

            return String.Compare(_name, other.Name);
        }
        #endregion
    }
}
