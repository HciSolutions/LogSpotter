using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using HciSolutions.LogSpotter.Properties;

namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    ///     <para>
    ///     Represents data source of logs that comes from SqlServer database.
    ///     </para>
    ///     <para>
    ///     To allow incremental loading of logs (that is required).
    ///     </para>
    ///     <para>
    ///     This table to must contains an incremental identity primary key specified in <see cref="PrimaryKey" />.
    ///     </para>
    ///     <para>
    ///     Also to ensure logical ordering, specify the <see cref="OrderingColumn" /> that will be used to order logs.
    ///     </para>
    /// </summary>
    /// <example>
    /// Here is an example of script to create table for logs.
    /// <code>
    /// 		CREATE TABLE dbo.EXECUTION_LOG (
    /// 			[EXECUTION_LOG_ID] int IDENTITY(1,1) NOT NULL,
    /// 			[DATE_TIME] datetime NOT NULL,
    /// 			[LEVEL] nvarchar(10),
    /// 			[MACHINE_NAME] nvarchar(255),
    /// 			[LOGGER] nvarchar(255),
    /// 			[THREAD_ID] int,
    /// 			[MESSAGE] nvarchar(max),
    /// 			[CALLSITE] nvarchar(max) NULL,
    /// 			[EXCEPTION] nvarchar(max) NULL,
    /// 			CONSTRAINT [PK_EXECUTION_LOG] PRIMARY KEY CLUSTERED ([EXECUTION_LOG_ID] ASC) WITH (FILLFACTOR = 80))		
    /// 		
    /// 		CREATE NONCLUSTERED INDEX [IX_EXECUTION_LOG__DATE_TIME]
    /// 			ON [dbo].[EXECUTION_LOG]([DATE_TIME] DESC) WITH (FILLFACTOR = 70);
    /// 		
    /// 		CREATE NONCLUSTERED INDEX [IX_EXECUTION_LOG__LEVEL]
    /// 			ON [dbo].[EXECUTION_LOG]([LEVEL] ASC) WITH (FILLFACTOR = 70);
    /// 		
    /// 		CREATE NONCLUSTERED INDEX [IX_EXECUTION_LOG__LOGGER]
    /// 			ON [dbo].[EXECUTION_LOG]([LOGGER] ASC) WITH (FILLFACTOR = 70); 
    ///  </code>
    /// </example>
    [LogDataSourceType("SqlServerDatabase")]
    class SqlServerDataSource : LogDataSource
    {
        #region Private Fields

        private readonly SqlConnection _connection;

        private readonly Timer _timer;
        private int _lastLoadedLogId;
        private bool _loading;
        private readonly Parameters _parameters;
        private Dictionary<PropertyInfo, string> _mapping;

        #endregion

        #region Public Constructors

        public SqlServerDataSource(string connectionString)
            : base(connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            _parameters = JsonSerializer.Deserialize<Parameters>(connectionString);
            _mapping = _parameters.GetPropertyMapping();
            _connection = new SqlConnection(_parameters.ConnectionString);
            _timer = new Timer(HandleTimerExpired, null, 1000, 1000);
        }

        #endregion

        #region Public Properties

        public override Image Icon => Resources.SqlServer;

        /// <summary>
        /// Gets the mapping between <see cref="LogEvent" /> and column on <see cref="TableName" />.
        /// </summary>
        public Dictionary<PropertyInfo, string> LogEventMapping => _mapping;

        /// <summary>
        /// Gets or sets the column used to order the logs. Often
        /// this is the time stamps of log.
        /// </summary>
        public string OrderingColumn => _parameters.OrderingColumn;

        /// <summary>
        /// Gets or sets the incremental identity primary key used to load incrementally the logs.
        /// </summary>
        public string PrimaryKey => _parameters.PrimaryKey;

        /// <summary>
        /// Gets or sets the table name where are stored the logs.
        /// </summary>
        public string TableName => _parameters.TableName;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Builds the connections string for <see cref="SqlServerDataSource" />.
        /// </summary>
        /// <param name="dbConnectionString">The SqlServer connection string.</param>
        /// <param name="tableName">The name of table that contains logs.</param>
        /// <param name="primaryKey">The primary key that must be identity.</param>
        /// <param name="orderingColumn">The column that is used to order result.</param>
        /// <param name="mapping">The mapping from <paramref name="tableName" /> columns to <see cref="LogEvent" /> object.</param>
        /// <returns>The connection string used to initialize a <see cref="SqlServerDataSource" />.</returns>
        public static string BuildConnectionString(string dbConnectionString, string tableName, string primaryKey, string orderingColumn, Dictionary<PropertyInfo, string> mapping)
        {
            var parameters = new Parameters
            {
                ConnectionString = dbConnectionString,
                TableName = tableName,
                PrimaryKey = primaryKey,
                OrderingColumn = orderingColumn,
                Mapping = mapping.ToDictionary(p => p.Key.Name, p => p.Value),
            };

            var connectionString = JsonSerializer.Serialize(parameters);
            return connectionString;
        }

        #endregion

        #region Public Methods

        public override void Close()
        {
            _connection.Close();
        }

        public override LogEvent[] Open()
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            return LoadLogs().ToArray();
        }

        public override string ToString(bool shortForm)
        {
            return $"{_connection.DataSource}/{_connection.Database}.{TableName}";
        }

        #endregion

        #region Private Methods

        private string GetCommandText()
        {
            var cmdText = new StringBuilder();

            cmdText.Append("SELECT TOP(");
            cmdText.Append(Config.Config.Current.MaxEvents);
            cmdText.Append(") ");
            cmdText.Append(string.Join(", ", LogEventMapping.Values.Where(v => !string.IsNullOrEmpty(v)).ToArray()));
            cmdText.Append(" FROM ");
            cmdText.Append(TableName);
            cmdText.Append(" WHERE ");
            cmdText.Append(PrimaryKey);
            cmdText.Append(" > ");
            cmdText.Append(_lastLoadedLogId);
            cmdText.Append(" ORDER BY ");
            cmdText.Append(OrderingColumn);
            cmdText.Append(" ASC, ");
            cmdText.Append(PrimaryKey);
            cmdText.Append(" ASC");

            return cmdText.ToString();
        }

        /// <summary>
        /// Handles the end of the notification timer.
        /// </summary>
        /// <param name="data">The unused data argument.</param>
        private void HandleTimerExpired(object data)
        {
            try
            {
                lock (_timer)
                {
                    UpdateLogs();
                }
            }
            catch
            {
            }
        }

        private IEnumerable<LogEvent> LoadLogs()
        {
            // If already loading, break
            if (_loading) yield break;

            lock (_timer)
            {
                // Due to concurrency, recheck if already loading
                if (_loading) yield break;

                // If not loading do it!
                _loading = true;
                try
                {
                    var command = new SqlCommand(GetCommandText(), _connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var logEvent = new LogEvent();

                            // Set values to new log instance
                            foreach (var mapping in LogEventMapping.Where(m => !string.IsNullOrEmpty(m.Value)))
                            {
                                try
                                {
                                    var value = reader.GetValue(reader.GetOrdinal(mapping.Value));
                                    if (mapping.Key.PropertyType == typeof(LogLevels))
                                        mapping.Key.SetValue(logEvent, Enum.Parse(typeof(LogLevels), value.ToString()), null);
                                    //else if (mapping.Key.PropertyType == typeof(DateTime))
                                    //    mapping.Key.SetValue(logEvent, DateTime.Parse(value.ToString()), null);
                                    else if (mapping.Key.PropertyType == typeof(string))
                                        mapping.Key.SetValue(logEvent, value?.ToString(), null);
                                    else
                                        mapping.Key.SetValue(logEvent, value, null);
                                }
                                catch
                                {
                                }
                            }

                            // Update las log event number
                            _lastLoadedLogId = Math.Max(_lastLoadedLogId, logEvent.EventNumber);

                            yield return logEvent;
                        }
                    }
                }
                finally
                {
                    _loading = false;
                }
            }
        }

        private void UpdateLogs()
        {
            var logsArray = LoadLogs().ToArray();
            if (logsArray.Any())
                OnNewLog(new NewLogEventArgs(logsArray));
        }

        #endregion

        #region Nested Classes

        private class Parameters
        {
            #region Public Properties

            public string ConnectionString { get; set; }

            public Dictionary<string, string> Mapping { get; set; }

            public string OrderingColumn { get; set; }

            public string PrimaryKey { get; set; }

            public string TableName { get; set; }

            #endregion

            public Dictionary<PropertyInfo, string> GetPropertyMapping()
            {
                return Mapping.ToDictionary(
                    kv => typeof(LogEvent).GetProperty(kv.Key),
                    kv => kv.Value);
            }
        }

        #endregion
    }
}
