using System;

namespace Triamun.Log4NetViewer.Data
{
    /// <summary>
    /// Defines the possible Log4Net event severities
    /// </summary>
    [Flags]
    public enum LogLevels
    {
        /// <summary>
        /// Defines no log severity.
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Defines the TRACE log severity.
        /// </summary>
        Trace = 0x01,
        /// <summary>
        /// Defines the DEBUG log severity.
        /// </summary>
        Debug = 0x02,
        /// <summary>
        /// Defines the INFO log severity.
        /// </summary>
        Info = 0x04,
        /// <summary>
        /// Defines the WARN log severity.
        /// </summary>
        Warn = 0x08,
        /// <summary>
        /// Defines the ERROR log severity.
        /// </summary>
        Error = 0x10,
        /// <summary>
        /// Defines the FATAL log severity.
        /// </summary>
        Fatal = 0x20,
        /// <summary>
        /// Defines all the log levels.
        /// </summary>
        All = Trace | Debug | Info | Warn | Error | Fatal
    }
}
