using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using HciSolutions.LogSpotter.Data;
using Triamun.Tools.DatabaseToCsvExtractor.Gui;

namespace HciSolutions.LogSpotter.Dialogs
{
    public partial class OpenSqlServerDatabaseDialog : Form
    {
        #region Private Fields

        private readonly SqlServerLogEventMapping _logEventMapping;
        private readonly PropertyGrid _proeprtyGrid;
        private SqlConnection _connection;
        private string _connectionString;

        #endregion

        #region Public Constructors

        public OpenSqlServerDatabaseDialog()
        {
            InitializeComponent();

            _logEventMapping = new SqlServerLogEventMapping();
            pgColumnMapping.SelectedObject = _logEventMapping;
            UpdateUi();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the connection string to SqlServer database.
        /// </summary>
        public string ConnectionString
        {
            get => _connectionString;
            private set
            {
                _connectionString = value;
                _connection = new SqlConnection(_connectionString);
                UpdateTables();
            }
        }

        /// <summary>
        /// Gets the mapping from SqlServer log table to <see cref="LogEvent" />.
        /// </summary>
        public Dictionary<PropertyInfo, string> Mapping => _logEventMapping.ToMappingDictionary();

        /// <summary>
        /// Gets the ordering (descendant) column name.
        /// </summary>
        public string OrderingColumn => cbOrderingColumn.SelectedItem?.ToString();

        /// <summary>
        /// Gets the primary key column name.
        /// </summary>
        public string PrimaryKey => cbPrimaryKey.SelectedItem?.ToString();

        /// <summary>
        /// Gets the table name that contains logs.
        /// </summary>
        public string TableName => cbTableName.SelectedItem?.ToString();

        #endregion

        #region Private Methods

        private void btnConnectionStringDialog_Click(object sender, EventArgs e)
        {
            var dlg = new ConnectionStringEditorForm();
            dlg.ConnectionString = _connectionString;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ConnectionString = dlg.ConnectionString;
                tbConnectionString.Text = ConnectionString;
            }
        }

        private void cbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadColumns();
        }

        private void LoadColumns()
        {
            ColumnNameConverter.Columns.Clear();
            _connection.Open();
            try
            {
                var cmd = new SqlCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{TableName}' ORDER BY COLUMN_NAME", _connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ColumnNameConverter.Columns.Add(reader.GetString(0));
                    }
                }

                // Try to automatically map fields
                try
                {
                    foreach (var property in _logEventMapping.GetType().GetProperties())
                    {
                        property.SetValue(
                            _logEventMapping, 
                            ColumnNameConverter.Columns.FirstOrDefault(c => c.ToUpper().StartsWith(property.Name.ToUpper())), 
                            null);
                    }
                }
                catch {}
            }
            finally
            {
                _connection.Close();
            }

            UpdateUi();
        }

        private void UpdateTables()
        {
            _connection.Open();
            try
            {
                cbTableName.Items.Clear();
                cbPrimaryKey.Items.Clear();
                cbOrderingColumn.Items.Clear();
                ColumnNameConverter.Columns.Clear();

                var cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME", _connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cbTableName.Items.Add(reader.GetString(0));
                    }
                }
            }
            finally
            {
                _connection.Close();
            }

            UpdateUi();
        }

        private void UpdateUi()
        {
            if (ColumnNameConverter.Columns.Any())
            {
                cbPrimaryKey.Items.Clear();
                cbPrimaryKey.Items.AddRange(ColumnNameConverter.Columns.Cast<object>().ToArray());
                cbOrderingColumn.Items.Clear();
                cbOrderingColumn.Items.AddRange(ColumnNameConverter.Columns.Cast<object>().ToArray());
            }

            cbTableName.Enabled = cbTableName.Items.Count > 0;
            cbPrimaryKey.Enabled = ColumnNameConverter.Columns.Any();
            cbOrderingColumn.Enabled = ColumnNameConverter.Columns.Any();
        }

        #endregion
    }
}
