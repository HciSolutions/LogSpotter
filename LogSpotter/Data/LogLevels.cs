using System;

namespace HciSolutions.LogSpotter.Data
{
    /// <summary>
    /// Defines the possible Log4Net event serverities
    /// </summary>
    [Flags]
    public enum LogLevels
    {
        /// <summary>
        /// Defines the TRACE log severity.
        /// </summary>
        Trace,
        /// <summary>
        /// Defines the DEBUG log severity.
        /// </summary>
        Debug,
        /// <summary>
        /// Defines the INFO log severity.
        /// </summary>
        Info,
        /// <summary>
        /// Defines the WARN log severity.
        /// </summary>
        Warn,
        /// <summary>
        /// Defines the ERROR log severity.
        /// </summary>
        Error,
        /// <summary>
        /// Defines the FATAL log severity.
        /// </summary>
        Fatal
    }
}
