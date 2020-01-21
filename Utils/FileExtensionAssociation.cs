using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Reflection;

namespace Triamun.Log4NetViewer.Utils
{
    /// <summary>
    /// Provide a helper used to manage file extension associations.
    /// </summary>
    public static class FileExtensionAssociation
    {
		#region Constants 

        public const string FILE_EXTENSION = ".log4net";
        private const int SUB_EXTENSION_COUNT = 10;
        private const string FILE_EXTENSION_DESCRIPTION = "Log4Net log file";
        private const string FILE_TYPE_IDENTIFIER = "log4netFile";
        private const string OPEN_COMMAND_SUBKEY = @"shell\open\command";
        private const string ICON_SUBKEY = @"DefaultIcon";
        private const string OPEN_COMMAND_FORMAT = "\"{0}\" \"%1\"";

		#endregion Constants 

		#region Private Members 

        private static string _openCommand;
        private static string _iconFileName;

		#endregion Private Members 

		#region Constructor 

        /// <summary>
        /// Initializes the static members of the <see cref="FileExtensionAssociation"/> class.
        /// </summary>
        static FileExtensionAssociation()
        {
            // Initialize the members
            _iconFileName = Assembly.GetExecutingAssembly().Location;
            _openCommand = String.Format(OPEN_COMMAND_FORMAT, Assembly.GetExecutingAssembly().Location);
        }

		#endregion Constructor 

		#region Public Static Methods 

        /// <summary>
        /// Checks that the application is the default one for the file extension.
        /// </summary>
        /// <returns><c>true</c> if the specified application is registered as default application to open the files with the specified extension; <c>false</c> otherwise.</returns>
        public static bool CheckIsDefault()
        {
            string fileTypeIdentifier = null;

            try
            {
                // Open the registry key for the extension
                using (RegistryKey extensionKey = Registry.ClassesRoot.OpenSubKey(FILE_EXTENSION, false))
                {
                    // Gets the file type identifier
                    if (extensionKey != null)
                        fileTypeIdentifier = (string)extensionKey.GetValue("", null);
                }

                // If the extension is not registered there is no default application
                if (String.IsNullOrEmpty(fileTypeIdentifier))
                    return false;

                // Open the registry key for the open command
                using (RegistryKey openCommandeKey = Registry.ClassesRoot.OpenSubKey(fileTypeIdentifier + "\\" + OPEN_COMMAND_SUBKEY, false))
                {
                    // If found, and the application exe file matches, it's ok.
                    return
                        openCommandeKey != null &&
                        _openCommand.Equals((String)openCommandeKey.GetValue("", String.Empty), StringComparison.InvariantCultureIgnoreCase);
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Registers the application as default application to open the files with the specified extension.
        /// </summary>
        public static void Register()
        {
            // Open the registry key for the extension (s)
            using (RegistryKey extensionKey = Registry.ClassesRoot.CreateSubKey(FILE_EXTENSION))
            {
                // Sets the file type identifier
                extensionKey.SetValue("", FILE_TYPE_IDENTIFIER, RegistryValueKind.String);
            }
            for (int i = 1; i <= SUB_EXTENSION_COUNT; i++)
            {
                using (RegistryKey extensionKey = Registry.ClassesRoot.CreateSubKey(FILE_EXTENSION + "." + i))
                {
                    // Sets the file type identifier
                    extensionKey.SetValue("", FILE_TYPE_IDENTIFIER, RegistryValueKind.String);
                }
            }

            // Open the file type identifier key
            using (RegistryKey identifierKey = Registry.ClassesRoot.CreateSubKey(FILE_TYPE_IDENTIFIER))
            {
                // Set the description
                identifierKey.SetValue("", FILE_EXTENSION_DESCRIPTION, RegistryValueKind.String);

                // Set the icon file
                if (File.Exists(_iconFileName))
                {
                    using (RegistryKey iconKey = identifierKey.CreateSubKey(ICON_SUBKEY))
                    {
                        iconKey.SetValue("", _iconFileName, RegistryValueKind.String);
                    }
                }

                // Set the open command
                using (RegistryKey openCommandKey = identifierKey.CreateSubKey(OPEN_COMMAND_SUBKEY))
                {
                    openCommandKey.SetValue("", _openCommand);
                }
            }
        }

		#endregion Public Static Methods 
    }
}
