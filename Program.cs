using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Log4NetViewer.Utils;
using Triamun.Log4NetViewer.Properties;
using Triamun.Log4NetViewer.Utils;

namespace Log4NetViewer
{
    static class Program
    {
        /// <summary>
        /// Defines the argument to pass to register as default program to open the .log4net files.
        /// </summary>
        public const string REGISTER_ARG = "-r";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                // Performs a file opening if a file is specified on the command line.
                if (args != null && args.Length > 0)
                {
                    if (String.Equals(args[0], REGISTER_ARG, StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            if (!FileExtensionAssociation.CheckIsDefault())
                            {
                                FileExtensionAssociation.Register();
                                Console.WriteLine("Registration OK");
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Registration failed: {0}", ex.Message);
                            Environment.Exit(1);
                        }
                    }
                    else if (File.Exists(args[0]))
                    {
                        try
                        {
                            OpenToMainInstance.OpenFile(args[0]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Resources.Err_FailedToOpenLog, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Start the GUI only for the main instance
                if (OpenToMainInstance.IsMainInstance)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
            }
            catch (Exception ex)
            {
                Exception error = ex.GetBaseException();
                MessageBox.Show(error.Message + Environment.NewLine + Environment.NewLine + error.StackTrace, Resources.Err_FatalStartup, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
