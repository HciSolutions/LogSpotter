﻿namespace HciSolutions.LogSpotter.Data.Reader
{
    /// <summary>
    /// Defines the delegate that can be specified to a <see cref="LogReader"/> so that it can provide progression informations.
    /// </summary>
    /// <param name="eventCount">The number of events read so far.</param>
    public delegate void ReadProgressCallback(int eventCount);
}
