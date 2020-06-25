using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace HciSolutions.LogSpotter.Data.Config
{
    /// <summary>
    /// Contains the configuration.
    /// </summary>
    [XmlRoot("log4NetViewerConfig")]
    public class Config
    {
        #region Private Static Members

        private static string _logFileName;
        #endregion

        #region Private Members

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the static members of the <see cref="Config"/> class.
        /// </summary>
        static Config()
        {
            Current = null;
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
            EventColors = new EventColors();
            RecentLogs = new List<RecentLog>();
            WindowPositions = new WindowPositioningCollection();
            MaxEvents = 200000;
        }
        #endregion

        #region Public Static Properties
        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        /// <value>The current configuration.</value>
        public static Config Current { get; private set; }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the event colors.
        /// </summary>
        /// <value>The event colors.</value>
        [XmlElement("colors")]
        public EventColors EventColors { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of events kept in memory.
        /// </summary>
        /// <value>The maximum number of events kept in memory.</value>
        [XmlAttribute("maxEvents")]
        public int MaxEvents { get; set; }

        /// <summary>
        /// Gets or sets the recent logs.
        /// </summary>
        /// <value>The recent logs.</value>
        [XmlArray("recentLogs")]
        [XmlArrayItem("recentLog")]
        public List<RecentLog> RecentLogs { get; set; }

        /// <summary>
        /// Gets or sets the window positions.
        /// </summary>
        /// <value>The window positions.</value>
        [XmlArray("windowPositions")]
        [XmlArrayItem("windowPosition")]
        public WindowPositioningCollection WindowPositions { get; set; }

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
                        Current = (Config)serializer.Deserialize(input);
                    }
                }
                else
                {
                    Current = new Config();
                    Save();
                }
            }
            catch { Current = new Config(); }
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        public static void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));

            using (FileStream output = new FileStream(_logFileName, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(output, Current);
            }
        }
        #endregion
    }
}
