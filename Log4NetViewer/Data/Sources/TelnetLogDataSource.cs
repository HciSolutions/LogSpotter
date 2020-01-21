using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
using Triamun.Log4NetViewer.Properties;
using System.Drawing;
using Triamun.Log4NetViewer.Data.Reader;

namespace Triamun.Log4NetViewer.Data.Sources
{
    /// <summary>
    /// Loads logs from a telnet connection in XML format.
    /// </summary>
    [LogDataSourceType("Telnet")]
    public class TelnetLogDataSource : LogDataSource
    {
        #region Constants
        private const int DEFAULT_PORT = 23;
        private const string EVENT_END_TAG = "</log4net:event>";
        private const int CONNECT_TIMEOUT = 2000;
        #endregion

        #region Private Members
        private LogReader _logReader;
        private Thread _receiveThread;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TelnetLogDataSource"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public TelnetLogDataSource(string connectionString)
            : base(connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");
            if (connectionString.Length == 0)
                throw new ArgumentException("The connection string cannot be empty.", "connectionString");

            _logReader = new XmlLogReader();
            _receiveThread = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogDataSource"/> class.
        /// </summary>
        /// <param name="host">The host name or ip address of the telnet server followed by an optional colon and port number.</param>
        public TelnetLogDataSource(string host, int portNumber)
            : this(String.Format("{0}:{1}", host, portNumber))
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the icon to show next to the data source in the UI.
        /// </summary>
        /// <value>The icon to show next to the data source in the UI.</value>
        public override Image Icon
        {
            get { return Resources.LogSourceTelnet; }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Main procedure of the receive thread.
        /// </summary>
        private void ReceiveThreadProc()
        {
            string host = null;
            int port = -1;
            int dataLength = 0;
            List<LogEvent> events = null;
            byte[] buf = new byte[80000];
            string receivedData = null;
            Socket sock = null;
            int useableChars = 0;
            char[] newlineChars = new char[] { '\r', '\n' };

            // Extract the host name and port number from the connection string
            if (ConnectionString.Contains(':'))
            {
                host = ConnectionString.Substring(0, ConnectionString.LastIndexOf(':')).Trim();
                if (!Int32.TryParse(ConnectionString.Substring(host.Length + 1).Trim(), out port))
                    throw new InvalidOperationException("Invalid connection string : " + ConnectionString);
            }
            else
            {
                host = ConnectionString;
                port = DEFAULT_PORT;
            }

            while (true)
            {
                try
                {
                    if (sock == null)
                    {
                        IAsyncResult asyncResult = null;

                        // Connects and skip the first message (it's a kind of hello).
                        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                        asyncResult = sock.BeginConnect(host, port, null, null);

                        if (!asyncResult.AsyncWaitHandle.WaitOne(CONNECT_TIMEOUT))
                        {
                            sock.Close();
                            sock = null;
                            throw new TimeoutException();
                        }
                        else
                            sock.EndConnect(asyncResult);

                        // We're online
                        IsOnline = true;
                    }

                    // Reads from the socket and terminate if the host has gone (0 bytes have been read)
                    dataLength = sock.Receive(buf);
                    if (dataLength > 0)
                        receivedData += Encoding.Default.GetString(buf, 0, dataLength);
                    else
                    {
                        sock.Shutdown(SocketShutdown.Both);
                        sock.Close();
                        sock = null;
                        continue;
                    }

                    // Reads all the complete events
                    useableChars = receivedData.LastIndexOfAny(newlineChars) + 1;
                    if (useableChars == receivedData.Length)
                    {
                        using (TextReader reader = new StringReader(receivedData))
                        {
                            events = _logReader.Read(reader);
                        }
                        receivedData = String.Empty;
                    }
                    else
                    {
                        using (TextReader reader = new StringReader(receivedData.Substring(0, useableChars)))
                        {
                            events = _logReader.Read(reader);
                        }
                        receivedData = receivedData.Substring(useableChars);
                    }

                    if (events.Count > 0)
                        OnNewLog(new NewLogEventArgs(events.ToArray()));

                    // Clears the errro
                    Error = null;
                }
                catch (ThreadAbortException) { break; }
                catch (XmlException) { }
                catch (SocketException ex)
                {
                    // Clears the socket, sets the error, the online flag and wait before retrying
                    sock = null;
                    Error = ex.Message;
                    IsOnline = false;
                    Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    // Sets the error
                    Error = ex.Message;
                }
            }

            // We're Offline
            IsOnline = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Closes this the data source.
        /// </summary>
        public override void Close()
        {
            lock (this)
            {
                _receiveThread.Abort();
                _receiveThread = null;
            }
        }

        /// <summary>
        /// Opens the data source and returns the list of initial log events available.
        /// </summary>
        /// <returns>
        /// The list of <see cref="LogEvent"/> initially available.
        /// </returns>
        public override LogEvent[] Open()
        {
            lock (this)
            {
                // Decodes the connection string
                if (String.IsNullOrEmpty(ConnectionString))
                    throw new InvalidOperationException("The connection string cannot be empty or null.");


                _receiveThread = new Thread(new ThreadStart(ReceiveThreadProc));
                _receiveThread.IsBackground = true;
                _receiveThread.Start();
            }

            // Returns the events
            return new LogEvent[0];
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="shortForm">if set to <c>true</c> returns a short representation of the object; otherwise, returns the long representation as <see cref="ToString()"/> does.</param>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString(bool shortForm)
        {
            return ConnectionString;
        }
        #endregion
    }
}
