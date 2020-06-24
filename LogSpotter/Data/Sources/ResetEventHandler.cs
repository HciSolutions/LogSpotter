namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Defines the signature of the method that will handle the <see cref="ILogDataSource.Reset"/> event.
    /// </summary>
    public delegate void ResetEventHandler(object sender, ResetEventArgs e);
}
