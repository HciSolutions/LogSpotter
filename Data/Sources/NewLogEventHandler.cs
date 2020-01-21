using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Triamun.Log4NetViewer.Data.Sources
{
    /// <summary>
    /// Defines the signature of the method that will handle the <see cref="ILogDataSource.NewLog"/> event.
    /// </summary>
    public delegate void NewLogEventHandler(object sender, NewLogEventArgs e);
}
