using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Triamun.Log4NetViewer.Data.Config
{
    /// <summary>
    /// Contains the configuration.
    /// </summary>
    [XmlRoot("log4NetViewerConfig")]
    public class Config
    {
        #region Private Static Members
        private static Config _current;
        private static string _logFileName;
        #endregion

        #region Private Members
        private List<RecentLog> _recentLogs;
        private WindowPositioningCollection _windowPositions;
        private EventColors _eventColors;
        private int _maxEvents;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the static members of the <see cref="Config"/> class.
        /// </summary>
        static Config()
        {
            _current = null;
            _logFileName = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Log4NetViewer"), "Config.xml");            

            if (!Directory.Exists(Path.GetDirectoryName(_logFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(_logFileName));

            Load();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        /// <param name="eventColors">The event colors.</param>
        /// <param name="recentLogs">The recent logs.</param>
        public Config()
        {
            _eventColors = new EventColors();
            _recentLogs = new List<RecentLog>();
            _windowPositions = new WindowPositioningCollection();
            _maxEvents = 200000;
        }
        #endregion

        #region Public Static Properties
        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        /// <value>The current configuration.</value>
        public static Config Current
        {
            get { return _current; }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the event colors.
        /// </summary>
        /// <value>The event colors.</value>
        [XmlElement("colors")]
        public EventColors EventColors
        {
            get { return _eventColors; }
            set { _eventColors = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of events kept in memory.
        /// </summary>
        /// <value>The maximum number of events kept in memory.</value>
        [XmlAttribute("maxEvents")]
        public int MaxEvents
        {
            get { return _maxEvents; }
            set { _maxEvents = value; }
        }

        /// <summary>
        /// Gets or sets the recent logs.
        /// </summary>
        /// <value>The recent logs.</value>
        [XmlArray("recentLogs")]
        [XmlArrayItem("recentLog")]
        public List<RecentLog> RecentLogs
        {
            get { return _recentLogs; }
            set { _recentLogs = value; }
        }

        /// <summary>
        /// Gets or sets the window positions.
        /// </summary>
        /// <value>The window positions.</value>
        [XmlArray("windowPositions")]
        [XmlArrayItem("windowPosition")]
        public WindowPositioningCollection WindowPositions
        {
            get { return _windowPositions; }
            set { _windowPositions = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Loads the configuration.
        /// </summary>
        public static void Load()
        {
            XmlSerializer serializer = null;

            try
            {
                if (File.Exists(_logFileName))
                {
                    using (FileStream input = new FileStream(_logFileName, FileMode.Open, FileAccess.Read))
                    {
                        serializer = new XmlSerializer(typeof(Config));
                        _current = (Config)serializer.Deserialize(input);
                    }
                }
                else
                {
                    _current = new Config();
                    Save();
                }
            }
            catch { _current = new Config(); }
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        public static void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            using (FileStream output = new FileStream(_logFileName, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(output, _current);
            }
        }
        #endregion
    }
}
