using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Triamun.Log4NetViewer.Data.Config
{
    /// <summary>
    /// Contains a collection of <see cref="WindowPositioning"/> entries.
    /// </summary>
    public class WindowPositioningCollection : IEnumerable<WindowPositioning>
    {
        #region Private Members
        private List<WindowPositioning> _innerList;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowPositioningCollection"/> class.
        /// </summary>
        public WindowPositioningCollection()
        {
            _innerList = new List<WindowPositioning>();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the <see cref="WindowPositioningCollection"/> at the specified index.
        /// </summary>
        /// <value>The index for which to return the item.</value>
        public WindowPositioning this[int index] 
        {
            get { return _innerList[index]; }
        }

        /// <summary>
        /// Gets the number of items within the collection.
        /// </summary>
        /// <value>The number of items within the collection.</value>
        public int Count
        {
            get { return _innerList.Count; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(WindowPositioning item)
        {
            _innerList.Add(item);
        }

        /// <summary>
        /// Saves the positioning values from the specified <paramref name="window"/> to the collection.
        /// </summary>
        /// <param name="window">The <see cref="Form"/> from which to read the positioning values.</param>
        public void SaveWindow(Form window)
        {
            WindowPositioning pos = null;

            if (window == null)
                throw new ArgumentNullException("window");

            for (int i = 0; i < _innerList.Count && pos == null; i++)
            {
                if (_innerList[i].Name == window.Name)
                    pos = _innerList[i];
            }

            if (pos == null)
            {
                pos = new WindowPositioning();
                _innerList.Add(pos);
            }

            pos.Y = window.Top;
            pos.X = window.Left;
            pos.Width = window.Width;
            pos.Height = window.Height;
            pos.Name = window.Name;
            pos.WindowState = window.WindowState;
        }

        /// <summary>
        /// Loads the positioning values from the collection to the specified <paramref name="window"/>.
        /// </summary>
        /// <param name="window">The <see cref="Form"/> to which to write the positioning values.</param>
        public void LoadWindow(Form window)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            for (int i = 0; i < _innerList.Count;  i++)
            {
                if (_innerList[i].Name == window.Name)
                {
                    window.Location = new Point(_innerList[i].X, _innerList[i].Y);
                    window.Size = new Size(_innerList[i].Width, _innerList[i].Height);
                    window.StartPosition = FormStartPosition.Manual;
                    window.WindowState = _innerList[i].WindowState;
                }
            }
        }
        #endregion

        #region IEnumerable<WindowPositioning> Members
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<WindowPositioning> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
