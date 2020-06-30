using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using HciSolutions.LogSpotter.Data;
using HciSolutions.LogSpotter.Dialogs;

namespace HciSolutions.LogSpotter
{
    /// <summary>
    /// Represents the mapping from SqlServer log table to <see cref="LogEvent" /> class.
    /// </summary>
    public class SqlServerLogEventMapping
    {
        #region Public Properties

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.ClassName" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string ClassName { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.Domain" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string Domain { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.EventNumber" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string EventNumber { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.Exception" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string Exception { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.Level" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string Level { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.LineNumber" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string LineNumber { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.Logger" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string Logger { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.Message" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string Message { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.MethodName" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string MethodName { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.Thread" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string Thread { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.TimeStamp" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string TimeStamp { get; set; }

        /// <summary>
        /// Gets the SQL column name for <see cref="LogEvent.UserName" />.
        /// </summary>
        [TypeConverter(typeof(ColumnNameConverter))]
        public string UserName { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Transform current mapping to a dictionary of <see cref="PropertyInfo" /> mapped to column name.
        /// </summary>
        /// <returns></returns>
        public Dictionary<PropertyInfo, string> ToMappingDictionary()
        {
            var logEventProperties = typeof(LogEvent).GetProperties();
            return GetType().GetProperties().ToDictionary(
                p => logEventProperties.Single(lep => lep.Name == p.Name),
                p => p.GetValue(this, null)?.ToString());
        }

        #endregion
    }
}
