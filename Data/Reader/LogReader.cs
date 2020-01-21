using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Triamun.Log4NetViewer.Data.Reader
{
    /// <summary>
    /// Defines the base functionalities of a class that can read and decode log events.
    /// </summary>
    public abstract class LogReader
    {
		#region Public Methods 

        /// <summary>
        /// Reads all the the events from the specified stream.
        /// </summary>
        /// <param name="input">The <see cref="Stream"/> from which to read the XML data.</param>
        /// <returns>A list of <see cref="LogEvent"/> that contains the read events.</returns>
        public virtual List<LogEvent> Read(Stream input)
        {
            return Read(input, null);
        }

        /// <summary>
        /// Reads all the the events from the specified text reader.
        /// </summary>
        /// <param name="input">The <see cref="TextReader"/> from which to read the XML data.</param>
        /// <returns>A list of <see cref="LogEvent"/> that contains the read events.</returns>
        public virtual List<LogEvent> Read(TextReader input)
        {
            return Read(input, null);
        }

        /// <summary>
        /// Reads all the the events from the specified stream.
        /// </summary>
        /// <param name="input">The <see cref="Stream"/> from which to read the XML data.</param>
        /// <param name="progressCallback">The optional progress callback that gets called during .</param>
        /// <returns>
        /// A list of <see cref="LogEvent"/> that contains the read events.
        /// </returns>
        public virtual List<LogEvent> Read(Stream input, ReadProgressCallback progressCallback)
        {
            return Read(new StreamReader(input, Encoding.Default), progressCallback);
        }

        /// <summary>
        /// Reads all the the events from the specified text reader.
        /// </summary>
        /// <param name="input">The <see cref="TextReader"/> from which to read the XML data.</param>
        /// <param name="progressCallback">The optional progress callback that gets called during .</param>
        /// <returns>A list of <see cref="LogEvent"/> that contains the read events.</returns>
        public abstract List<LogEvent> Read(TextReader input, ReadProgressCallback progressCallback);

		#endregion Public Methods 
    }
}
