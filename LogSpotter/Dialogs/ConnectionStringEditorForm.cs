using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Threading;

namespace Triamun.Tools.DatabaseToCsvExtractor.Gui
{
    /// <summary>
    /// Form that help to construct a connection string.
    /// </summary>
    public partial class ConnectionStringEditorForm : Form
    {
        #region Private Constants

        private const int AuthSql = 1;
        private const int AuthWindows = 0;

        #endregion Private Constants

        #region Private Fields

        private int _busyCounter;
        private bool _databasesLoaded;
        private bool _serversLoaded;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenDatabase"/> class.
        /// </summary>
        public ConnectionStringEditorForm()
        {
            InitializeComponent();

            _serversLoaded = false;
            _databasesLoaded = false;

            UpdateUi();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                if (!string.IsNullOrEmpty(cbServerName.Text))
                    builder.DataSource = cbServerName.Text;
                builder.IntegratedSecurity = cbAuthentication.SelectedIndex != AuthSql;
                if (cbAuthentication.SelectedIndex == AuthSql)
                {
                    builder.UserID = tbUser.Text;
                    builder.Password = tbPassword.Text;
                }
                if (cbDatabaseName.SelectedIndex >= 0)
                    builder.InitialCatalog = Convert.ToString(cbDatabaseName.SelectedItem);

                return builder.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    cbServerName.Text = string.Empty;
                    cbAuthentication.SelectedIndex = 0;
                    tbUser.Text = string.Empty;
                    tbPassword.Text = string.Empty;
                }
                else
                {
                    SqlConnectionStringBuilder builder;
                    try
                    {
                        builder = new SqlConnectionStringBuilder(value);
                    }
                    catch
                    {
                        builder = new SqlConnectionStringBuilder();
                    }

                    cbServerName.Text = builder.DataSource;
                    cbAuthentication.SelectedIndex = builder.IntegratedSecurity ? AuthWindows : AuthSql;
                    tbUser.Text = builder.UserID;
                    tbPassword.Text = builder.Password;

                    cbDatabaseName.Items.Clear();
                    cbDatabaseName.Items.Add(builder.InitialCatalog);
                    cbDatabaseName.SelectedIndex = 0;
                }

                UpdateUi();
            }
        }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Begins the busy operation.
        /// </summary>
        private void BeginBusyOperation()
        {
            if (Interlocked.Increment(ref _busyCounter) == 1)
            {
                sslInfoLabel.Visible = true;
                Cursor = Cursors.WaitCursor;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (TestConnection(false))
                DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the Click event of the btnTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            TestConnection(true);
        }

        /// <summary>
        /// Handles the DoWork event of the bwLoadDatabases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void bwLoadDatabases_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SqlServerUtils.FindDatabases((string)e.Argument);
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bwLoadDatabases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bwLoadDatabases_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw e.Error;

                string[] databaseNames = (string[])e.Result ?? new string[0];

                cbDatabaseName.Items.Clear();
                foreach (string database in databaseNames)
                {
                    cbDatabaseName.Items.Add(database);
                }
                _databasesLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Unable to retrieve the list of databases.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EndBusyOperation();
            }
        }

        /// <summary>
        /// Handles the DoWork event of the bwLoadServers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void bwLoadServers_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SqlServerUtils.FindInstances();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bwLoadServers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bwLoadServers_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw e.Error;

                string[] serverNames = (string[])e.Result ?? new string[0];

                cbServerName.Items.Clear();
                foreach (string instance in serverNames)
                {
                    cbServerName.Items.Add(instance);

                    if (String.Equals(Environment.MachineName, instance, StringComparison.OrdinalIgnoreCase))
                        cbServerName.SelectedIndex = cbServerName.Items.Count - 1;
                }
                _serversLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Unable to retrieve the list of SQL Server instances.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EndBusyOperation();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbAuthentication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateUi();
            }
            catch { }
        }

        /// <summary>
        /// Handles the DropDown event of the cbDatabaseName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbDatabaseName_DropDown(object sender, EventArgs e)
        {
            if (!_databasesLoaded)
            {
                BeginBusyOperation();
                bwLoadDatabases.RunWorkerAsync(ConnectionString);
            }
        }

        /// <summary>
        /// Handles the DropDown event of the cbServerName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbServerName_DropDown(object sender, EventArgs e)
        {
            if (!_serversLoaded)
            {
                BeginBusyOperation();
                bwLoadServers.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the cbServerName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbServerName_TextChanged(object sender, EventArgs e)
        {
            UpdateUi();
        }

        /// <summary>
        /// Ends the busy operation.
        /// </summary>
        private void EndBusyOperation()
        {
            if (Interlocked.Decrement(ref _busyCounter) == 0)
            {
                sslInfoLabel.Visible = false;
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Tests the connection.
        /// </summary>
        /// <param name="showSuccessMessage">if set to <c>true</c> displays a message in case of success; otherwise, show only a message in case of failure.</param>
        /// <returns>
        /// 	<c>true</c> if the test succeeded; otherwise, <c>false</c>.
        /// </returns>
        private bool TestConnection(bool showSuccessMessage)
        {
            SqlConnectionStringBuilder builder = null;
            try
            {
                builder = new SqlConnectionStringBuilder(ConnectionString);
                if (SqlServerUtils.TestSqlConnection(builder.ToString()))
                {
                    if (showSuccessMessage)
                        MessageBox.Show(this, "Connection successful.", "Connection test result.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

                MessageBox.Show(this, "Unable to connect to the server.", "Connection test result.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch
            {
                MessageBox.Show(this, "Unable to connect to the server.", "Connection test result.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        private void UpdateUi()
        {
            tbUser.Enabled = cbAuthentication.SelectedIndex == AuthSql;
            lblUserName.Enabled = cbAuthentication.SelectedIndex == AuthSql;
            tbPassword.Enabled = cbAuthentication.SelectedIndex == AuthSql;
            lblPassword.Enabled = cbAuthentication.SelectedIndex == AuthSql;

            btnTest.Enabled = !String.IsNullOrEmpty(cbServerName.Text);
            btnOK.Enabled = !String.IsNullOrEmpty(cbServerName.Text);
        }

        #endregion Private Methods

        #region Private Classes


        /// <summary>
        /// Sql server utilities.
        /// </summary>
        private static class SqlServerUtils
        {
            #region Private Constants

            private const string CATALOG_DATABASES = "Databases";
            private const string CATALOG_TABLES = "Tables";
            private const string COL_DATABASE_NAME = "database_name";
            private const string COL_SERVER_INSTANCE_NAME = "InstanceName";
            private const string COL_SERVER_NAME = "ServerName";
            private const string COL_TABLE_NAME = "TABLE_NAME";

            #endregion Private Constants

            #region Public Methods

            /// <summary>
            /// Finds the databases available.
            /// </summary>
            /// <param name="connectionString">The connection string.</param>
            /// <returns>An array of string that contains the found databases.</returns>
            public static string[] FindDatabases(string connectionString)
            {
                DataTable dt = null;
                string[] databases = null;
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

                builder.ConnectTimeout = 1;
                builder.InitialCatalog = String.Empty;

                using (SqlConnection cnn = new SqlConnection(builder.ToString()))
                {
                    cnn.Open();
                    dt = cnn.GetSchema(CATALOG_DATABASES);

                    databases = new string[dt.Rows.Count];
                    for (int i = 0; i < databases.Length; i++)
                    {
                        databases[i] = Convert.ToString(dt.Rows[i][COL_DATABASE_NAME]);
                    }
                }

                Array.Sort(databases);
                return databases;
            }

            /// <summary>
            /// Search for sql server instances.
            /// </summary>
            /// <returns>An array of string that contains the host name of the SQL Server instances found.</returns>
            public static string[] FindInstances()
            {
                DataTable dt = null;
                string[] instances = null;

                dt = SqlClientFactory.Instance.CreateDataSourceEnumerator().GetDataSources();

                instances = new string[dt.Rows.Count];
                for (int i = 0; i < instances.Length; i++)
                {
                    string serverName = Convert.ToString(dt.Rows[i][COL_SERVER_NAME]);
                    string instanceName = Convert.ToString(dt.Rows[i][COL_SERVER_INSTANCE_NAME]);

                    if (String.IsNullOrEmpty(instanceName))
                        instances[i] = serverName;
                    else
                        instances[i] = serverName + "\\" + instanceName;
                }

                return instances;
            }

            /// <summary>
            /// Tests the SQL connection.
            /// </summary>
            /// <param name="connectionString">The connection string.</param>
            /// <returns><c>true</c> if the connection is ok; otherwise, <c>false</c>.</returns>
            public static bool TestSqlConnection(string connectionString)
            {
                try
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                    builder.ConnectTimeout = 1;

                    if (String.IsNullOrEmpty(builder.DataSource))
                        return false;

                    using (SqlConnection cnn = new SqlConnection(builder.ToString()))
                    {
                        cnn.Open();
                    }

                    return true;
                }
                catch { return false; }
            }

            #endregion Public Methods
        }
        #endregion Private Classes
    }
}
