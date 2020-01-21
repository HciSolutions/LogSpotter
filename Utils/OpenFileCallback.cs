using System;
using System.Collections.Generic;
using System.Text;

namespace Log4NetViewer.Utils
{
    /// <summary>
    /// Defines the signature of the callback method to use when a file must be opened from another instance or the current instance.
    /// </summary>
    public delegate void OpenFileCallback(string fileName);
}
