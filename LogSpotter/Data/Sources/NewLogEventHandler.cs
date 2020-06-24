namespace HciSolutions.LogSpotter.Data.Sources
{
    /// <summary>
    /// Defines the signature of the method that will handle the <see cref="ILogDataSource.NewLog"/> event.
    /// </summary>
    public delegate void NewLogEventHandler(object sender, NewLogEventArgs e);
}
