using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triamun.Log4NetViewer.Data
{
    /// <summary>
    /// Defines the possible Log4Net event serverities
    /// </summary>
    [Flags]
    public enum LogLevels
    {
        /// <summary>
        /// Defines the DEBUG log severity.
        /// </summary>
        Debug = 0x01,
        /// <summary>
        /// Defines the INFO log severity.
        /// </summary>
        Info = 0x02,
        /// <summary>
        /// Defines the WARN log severity.
        /// </summary>
        Warn = 0x04,
        /// <summary>
        /// Defines the ERROR log severity.
        /// </summary>
        Error = 0x08,
        /// <summary>
        /// Defines the FATAL log severity.
        /// </summary>
        Fatal = 0x10
    }
}
