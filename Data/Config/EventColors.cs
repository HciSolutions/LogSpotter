using System.Drawing;
using System.Xml.Serialization;

namespace Triamun.Log4NetViewer.Data.Config
{
    /// <summary>
    /// Represents the list of <see cref="EventColor"/> for all possible log levels.
    /// </summary>
    public class EventColors
    {
        #region Private Members

        private EventColor _trace;
        private EventColor _debug;
        private EventColor _info;
        private EventColor _warning;
        private EventColor _error;
        private EventColor _fatal;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EventColorCollection"/> class with default colors.
        /// </summary>
        public EventColors()
        {
            _trace = new EventColor(Color.White, Color.Purple);
            _debug = new EventColor(Color.White, Color.Green);
            _info = new EventColor(Color.White, Color.FromArgb(0, 0, 255));
            _warning = new EventColor(Color.White, Color.FromArgb(255, 128, 0));
            _error = new EventColor(Color.White, Color.FromArgb(255, 0, 0));
            _fatal = new EventColor(Color.FromArgb(255, 0, 0), Color.FromArgb(255, 255, 0));
        }

        #endregion Constructor

        #region Public Properties

        /// <summary>
        /// Gets or sets the trace colors.
        /// </summary>
        /// <value>The trace colors.</value>
        [XmlElement("trace")]
        public EventColor Trace
        {
            get { return _trace; }
            set { _trace = value; }
        }

        /// <summary>
        /// Gets or sets the debug colors.
        /// </summary>
        /// <value>The debug colors.</value>
        [XmlElement("debug")]
        public EventColor Debug
        {
            get { return _debug; }
            set { _debug = value; }
        }

        /// <summary>
        /// Gets or sets the error colors.
        /// </summary>
        /// <value>The error colors.</value>
        [XmlElement("error")]
        public EventColor Error
        {
            get { return _error; }
            set { _error = value; }
        }

        /// <summary>
        /// Gets or sets the fatal colors.
        /// </summary>
        /// <value>The fatal colors.</value>
        [XmlElement("fatal")]
        public EventColor Fatal
        {
            get { return _fatal; }
            set { _fatal = value; }
        }

        /// <summary>
        /// Gets or sets the info colors.
        /// </summary>
        /// <value>The info colors.</value>
        [XmlElement("info")]
        public EventColor Info
        {
            get { return _info; }
            set { _info = value; }
        }

        /// <summary>
        /// Gets or sets the warning colors.
        /// </summary>
        /// <value>The warning colors.</value>
        [XmlElement("warning")]
        public EventColor Warning
        {
            get { return _warning; }
            set { _warning = value; }
        }

        #endregion Public Properties
    }
}
