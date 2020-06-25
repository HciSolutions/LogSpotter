using System;
using System.Collections.Generic;
using System.Reflection;

namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Factory class for the log datasources.
    /// </summary>
    public class LogDataSourceFactory
    {
        #region Private Members
        private static Dictionary<string, Type> _dataSourceTypes;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the static members of the <see cref="LogDataSourceFactory"/> class.
        /// </summary>
        static LogDataSourceFactory()
        {
            _dataSourceTypes = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.IsDefined(typeof(LogDataSourceTypeAttribute), true) && t.IsSubclassOf(typeof(LogDataSource)))
                    _dataSourceTypes[((LogDataSourceTypeAttribute)t.GetCustomAttributes(typeof(LogDataSourceTypeAttribute), true)[0]).Name] = t;
            }

            DataSourceTypeNames = new string[_dataSourceTypes.Count];
            _dataSourceTypes.Keys.CopyTo(DataSourceTypeNames,  0);
            Array.Sort<string>(DataSourceTypeNames);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the data source type names.
        /// </summary>
        /// <value>The data source type names.</value>
        public static string[] DataSourceTypeNames { get; }

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Creates a new <see cref="LogDataSource"/> with thhe specified log datasource type name.
        /// </summary>
        /// <param name="typeName">Name of the log datasource type to create.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>A new <see cref="LogDataSource"/>.</returns>
        public static LogDataSource Create(string typeName, string connectionString)
        {
            Type t = null;

            if (typeName == null)
                throw new ArgumentNullException("typeName");

            if (!_dataSourceTypes.TryGetValue(typeName, out t))
                throw new ArgumentException("Unknonw data source type : " + typeName + ".");

            return (LogDataSource)Activator.CreateInstance(t, connectionString);
        }
        #endregion
    }
}
