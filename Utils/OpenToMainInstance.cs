using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting;
using System.Windows.Forms;
using System.Reflection;

namespace Log4NetViewer.Utils
{
    /// <summary>
    /// Implements the remote object that handles file opens through command line from another instance.
    /// </summary>
    public static class OpenToMainInstance
    {
        #region Private Members

        private static readonly string _proxyUrl;
        private static List<string> _pendingFiles;
        private static IChannel _channel;
        private static OpenFileProxy _proxy;
        private static OpenFileCallback _callback;
        private static Mutex _systemMutex;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes the static members of <see cref="OpenToMainInstance"/> class.
        /// </summary>
        static OpenToMainInstance()
        {
            string applicationName = Assembly.GetExecutingAssembly().GetName().Name + "Session_" + Process.GetCurrentProcess().SessionId.ToString(); ;
            string proxyName = "OpenToMainInstance";
            bool isMainInstance = false;

            // Initialize members
            _proxyUrl = String.Format("ipc://{0}/{1}", applicationName, proxyName);
            _pendingFiles = null;
            _channel = null;
            _proxy = null;
            _callback = null;

            // Creates a system wide Mutex to check if we are the only application
            _systemMutex = new Mutex(true, applicationName, out isMainInstance);
            GC.KeepAlive(_systemMutex);


            // If we are the main instance, we need to publish the Remoting object !
            if (isMainInstance)
            {
                // Setup the IPC channel
                _channel = new IpcServerChannel(applicationName);
                ChannelServices.RegisterChannel(_channel, false);

                // Register the service
                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(OpenToMainInstance),
                    proxyName,
                    WellKnownObjectMode.Singleton);

                // Registers the singleton object
                _proxy = new OpenFileProxy(new OpenFileCallback(HandleOpenFileCall));
                RemotingServices.Marshal(_proxy, proxyName);

                // Register the domain exit delegate in order to cleanup the remoting proxy
                AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_DomainUnload);
            }
        }

        #endregion Constructor

        #region Public Static Properties

        /// <summary>
        /// Gets a value indicating whether the application is the main instance.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the application is the main instance; otherwise, <c>false</c>.
        /// </value>
        public static bool IsMainInstance
        {
            get { return _proxy != null; }
        }

        #endregion Public Static Properties

        #region Public Static Methods

        /// <summary>
        /// Opens a file to this instance or to the main instance.
        /// </summary>
        /// <param name="fileName">Name of the file to be opened.</param>
        /// <returns><c>true</c> true if the file will be or has been opened in the current instance; <c>false</c> if another instance will open the file.</returns>
        public static bool OpenFile(string fileName)
        {
            if (IsMainInstance)
            {
                HandleOpenFileCall(fileName);
                return true;
            }
            else
            {
                // Connect ot the main instance and notify the opening request
                IOpenFileProxy client = null;
                IChannel channel = new IpcClientChannel();
                ChannelServices.RegisterChannel(channel, false);
                try
                {
                    client = (IOpenFileProxy)Activator.GetObject(typeof(IOpenFileProxy), _proxyUrl);
                    client.OpenFile(fileName);
                }
                finally { ChannelServices.UnregisterChannel(channel); }

                return false;
            }
        }

        /// <summary>
        /// Registers the delegate to call when a file must be opened.
        /// </summary>
        /// <param name="callback">The <see cref="OpenFileCallback"/> delegate that must be called when a file must be opened.</param>
        /// <remarks>When a callback is registered, the specified <paramref name="callback"/> will be called for every pending open request before the method returns.</remarks>
        public static void RegisterOpenCallback(OpenFileCallback callback)
        {
            _callback = callback;

            if (_callback != null && _pendingFiles != null)
            {
                foreach (string file in _pendingFiles)
                {
                    try
                    {
                        callback(file);
                    }
                    catch { }
                }
                _pendingFiles = null;
            }
        }

        #endregion Public Static Methods

        #region Private Static Methods

        /// <summary>
        /// Handles the DomainUnload event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            if (_channel != null)
                ChannelServices.UnregisterChannel(_channel);

            _systemMutex.Close();
            _systemMutex = null;
            _channel = null;
            _proxy = null;
        }

        /// <summary>
        /// Handles an open file call.
        /// </summary>
        /// <param name="fileName">Name of the file to open.</param>
        private static void HandleOpenFileCall(string fileName)
        {
            // If a callback is already registered, call it directly
            if (_callback != null)
                _callback(fileName);
            else
            {
                // If no callback is registered yet, save the pending open request...
                if (_pendingFiles == null)
                    _pendingFiles = new List<string>();
                _pendingFiles.Add(fileName);
            }
        }

        #endregion Private Static Methods

        #region Nested Classes


        /// <summary>
        /// Priovides a remoting server implementation of <see cref="IOpenToNzbProxy"/>.
        /// </summary>
        private class OpenFileProxy : MarshalByRefObject, IOpenFileProxy
        {
            #region Private Members

            private OpenFileCallback _callback;

            #endregion Private Members

            #region Constructor

            /// <summary>
            /// Initializes a new instance of the <see cref="OpenFileProxy"/> class.
            /// </summary>
            /// <param name="callback">The <see cref="OpenFileCallback"/> delegate to invoke when a file must be opened.</param>
            public OpenFileProxy(OpenFileCallback callback)
            {
                _callback = callback;
            }

            #endregion Constructor

            #region Public Methods

            /// <summary>
            /// Obtains a lifetime service object to control the lifetime policy for this instance.
            /// </summary>
            /// <returns>
            /// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease"/> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime"/> property.
            /// </returns>
            /// <exception cref="T:System.Security.SecurityException">
            /// The immediate caller does not have infrastructure permission.
            /// </exception>
            /// <PermissionSet>
            /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure"/>
            /// </PermissionSet>
            public override object InitializeLifetimeService()
            {
                return null;
            }

            /// <summary>
            /// Opens the specified NZB file name.
            /// </summary>
            /// <param name="fileName">Name of the NZB file to open.</param>
            public void OpenFile(string fileName)
            {
                if (_callback != null)
                    _callback(fileName);
            }

            #endregion Public Methods
        }
        #endregion Nested Classes

        /// <summary>
        /// Provides the base interface of the remote object that handles file opens through command line from another instance.
        /// </summary>
        public interface IOpenFileProxy
        {
            /// <summary>
            /// Opens the specified file name.
            /// </summary>
            /// <param name="fileName">Name of the file to open.</param>
            void OpenFile(string fileName);
        }
    }
}
