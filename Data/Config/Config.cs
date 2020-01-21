using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Triamun.Log4NetViewer.Data.Config
{
    /// <summary>
    /// Contains the configuration.
    /// </summary>
    [XmlRoot("log4NetViewerConfig")]
    public class Config
    {
        #region Private Members

        private List<RecentLog> _recentLogs;
        private EventColors _eventColors;
        private int _maxEvents;
        private bool _showMilliseconds;
        private static Config _current;
        private static string _logFileName;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        /// <param name="eventColors">The event colors.</param>
        /// <param name="recentLogs">The recent logs.</param>
        public Config()
        {
            _eventColors = new EventColors();
            _recentLogs = new List<RecentLog>();
            _showMilliseconds = false;
            _maxEvents = 200000;
        }
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

        #endregion Constructor

        #region Public Properties

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
        /// Gets or sets a value indicating whether grid will show milliseconds or not.
        /// </summary>
        /// <value><c>true</c> if grid will show milliseconds; otherwise, <c>false</c>.</value>
        [XmlAttribute("showMilliseconds")]
        public bool ShowMilliseconds
        {
            get { return _showMilliseconds; }
            set { _showMilliseconds = value; }
        }

        #endregion Public Properties

        #region Public Static Properties

        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        /// <value>The current configuration.</value>
        public static Config Current
        {
            get { return _current; }
        }

        #endregion Public Static Properties

        #region Public Static Methods

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        public static void Load()
        {
            try
            {
                if (File.Exists(_logFileName))
                {
                    using (FileStream input = new FileStream(_logFileName, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Config));
                        _current = (Config)serializer.Deserialize(input);
                    }

                    UpgradeConfig();
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
        /// Upgrades the configuration.
        /// </summary>
        private static void UpgradeConfig()
        {
            // To Remove: Reset default color for debug
            try
            {
                if (_current != null && _current.EventColors != null && _current.EventColors.Debug.ForegroundColor.ToArgb() == Color.Black.ToArgb())
                {
                    _current.EventColors.Debug.ForegroundColor = Color.Green;
                    Save();
                }
            }
            catch
            {
            }

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

        #endregion Public Static Methods
    }
}
