using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
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
    class SqlServerDataSource : LogDataSource
    {
        #region Private Fields

        private readonly SqlConnection _connection;

        private readonly Dictionary<PropertyInfo, string> _logEventMapping = new Dictionary<PropertyInfo, string>();
        private int _lastLoadedLogId;
        private readonly Timer _timer;
        private bool _loading;

        #endregion

        #region Public Constructors

        public SqlServerDataSource(string connectionString)
            : base(connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _timer = new Timer(HandleTimerExpired, null, 1000, 1000);
        }

        #endregion

        #region Public Properties

        public override Image Icon => Resources.SqlServer;

        /// <summary>
        /// Gets the mapping between <see cref="LogEvent" /> and column on <see cref="TableName" />.
        /// </summary>
        public Dictionary<PropertyInfo, string> LogEventMapping => _logEventMapping;

        /// <summary>
        /// Gets or sets the column used to order the logs (descending). Often
        /// this is the time stamps of log.
        /// </summary>
        public string OrderingColumn { get; set; }

        /// <summary>
        /// Gets or sets the incremental identity primary key used to load incrementally the logs.
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets the table name where are stored the logs.
        /// </summary>
        public string TableName { get; set; }

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
            cmdText.Append(string.Join(", ", _logEventMapping.Values.ToArray()));
            cmdText.Append(" FROM ");
            cmdText.Append(TableName);
            cmdText.Append(" WHERE ");
            cmdText.Append(PrimaryKey);
            cmdText.Append(" > ");
            cmdText.Append(_lastLoadedLogId);
            cmdText.Append(" ORDER BY ");
            cmdText.Append(OrderingColumn);
            cmdText.Append(" DESC, ");
            cmdText.Append(PrimaryKey);
            cmdText.Append(" DESC");

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
                        while (reader.NextResult())
                        {
                            var logEvent = new LogEvent();

                            // Set values to new log instance
                            foreach (var mapping in _logEventMapping)
                            {
                                var value = reader.GetValue(reader.GetOrdinal(mapping.Value));
                                mapping.Key.SetValue(logEvent, value, null);
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
    }
}
