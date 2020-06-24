namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Defines the signature of the method that will handle the <see cref="LogDataSource.OpenProgress"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="OpenProgressEventArgs"/> that contains the data of the event.</param>
    public delegate void OpenProgressEventHandler(object sender, OpenProgressEventArgs e);
}
