using System;

namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Identifies a type of log datasource.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class LogDataSourceTypeAttribute : Attribute
    {
        #region Private Members

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LogDataSourceTypeAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the datasource type.</param>
        public LogDataSourceTypeAttribute(string name)
        {
            Name = name;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of the datasource type.
        /// </summary>
        /// <value>The name of the datasource type.</value>
        public string Name { get; }

        #endregion
    }
}
