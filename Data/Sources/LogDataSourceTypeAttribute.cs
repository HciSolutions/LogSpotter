using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triamun.Log4NetViewer.Data.Sources
{
    /// <summary>
    /// Identifies a type of log datasource.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class LogDataSourceTypeAttribute : Attribute
    {
		#region Private Members 

        private string _name;

		#endregion Private Members 

		#region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="LogDataSourceTypeAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the datasource type.</param>
        public LogDataSourceTypeAttribute(string name)
        {
            _name = name;
        }

		#endregion Constructor 

		#region Public Properties 

        /// <summary>
        /// Gets the name of the datasource type.
        /// </summary>
        /// <value>The name of the datasource type.</value>
        public string Name
        {
            get { return _name; }
        }

		#endregion Public Properties 
    }
}
