using System.Drawing;
using System.Xml.Serialization;

namespace HciSolutions.LogSpotter.Data.Config
{
    /// <summary>
    /// Represents the list of <see cref="EventColor"/> for all possible log levels.
    /// </summary>
    public class EventColors
    {
        #region Private Members

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EventColorCollection"/> class with default colors.
        /// </summary>
        public EventColors()
        {
            Debug = new EventColor(Color.White, Color.Black);
            Info = new EventColor(Color.White, Color.FromArgb(0, 0, 255));
            Warning = new EventColor(Color.White, Color.FromArgb(255, 128, 0));
            Error = new EventColor(Color.White, Color.FromArgb(255, 0, 0));
            Fatal = new EventColor(Color.FromArgb(255, 0, 0), Color.FromArgb(255, 255, 0));
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the debug colors.
        /// </summary>
        /// <value>The debug colors.</value>
        [XmlElement("debug")]
        public EventColor Debug { get; set; }

        /// <summary>
        /// Gets or sets the error colors.
        /// </summary>
        /// <value>The error colors.</value>
        [XmlElement("error")]
        public EventColor Error { get; set; }

        /// <summary>
        /// Gets or sets the fatal colors.
        /// </summary>
        /// <value>The fatal colors.</value>
        [XmlElement("fatal")]
        public EventColor Fatal { get; set; }

        /// <summary>
        /// Gets or sets the info colors.
        /// </summary>
        /// <value>The info colors.</value>
        [XmlElement("info")]
        public EventColor Info { get; set; }

        /// <summary>
        /// Gets or sets the warning colors.
        /// </summary>
        /// <value>The warning colors.</value>
        [XmlElement("warning")]
        public EventColor Warning { get; set; }

        #endregion
    }
}
