using System.Xml.Serialization;

namespace HciSolutions.LogSpotter.Data.Config
{
    /// <summary>
    /// Represents a recently opened log.
    /// </summary>
    public class RecentLog
    {
        #region Private Members

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentLog"/> class.
        /// </summary>
        public RecentLog()
        {
            Type = null;
            ConnectionString = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentLog"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="type">The type.</param>
        public RecentLog(string connectionString, string type)
        {
            ConnectionString = connectionString;
            Type = type;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        [XmlText]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [XmlAttribute("type")]
        public string Type { get; set; }

        #endregion
    }
}
