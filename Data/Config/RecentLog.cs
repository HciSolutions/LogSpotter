using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Triamun.Log4NetViewer.Data.Config
{
    /// <summary>
    /// Represents a recently opened log.
    /// </summary>
    public class RecentLog
    {
		#region Private Members 

        private string _type;
        private string _connectionString;

		#endregion Private Members 

		#region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentLog"/> class.
        /// </summary>
        public RecentLog()
        {
            _type = null;
            _connectionString = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentLog"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="type">The type.</param>
        public RecentLog(string connectionString, string type)
        {
            _connectionString = connectionString;
            _type = type;
        }

		#endregion Constructor 

		#region Public Properties 

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        [XmlText]
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [XmlAttribute("type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

		#endregion Public Properties 
    }
}
