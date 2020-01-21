using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Triamun.Log4NetViewer.Data.Reader
{
    /// <summary>
    /// Reads and decodes log events from xml data.
    /// </summary>
    public class XmlLogReader : LogReader
    {
        #region Constants

        public const string XML_NAMESPACE = "log4net";

        #endregion Constants

        #region Private Members

        private int _nextEventNumber;
        private Regex _newLineNormRegex;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlLogReader"/> class.
        /// </summary>
        public XmlLogReader()
        {
            _nextEventNumber = 1;
            _newLineNormRegex = new Regex(@"[\r\n]{1,2}", RegexOptions.Compiled);
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Reads all the the events from the specified text reader.
        /// </summary>
        /// <param name="input">The <see cref="TextReader"/> from which to read the XML data.</param>
        /// <param name="progressCallback">The optional progress callback that gets called during .</param>
        /// <returns>
        /// A list of <see cref="LogEvent"/> that contains the read events.
        /// </returns>
        public override List<LogEvent> Read(TextReader input, ReadProgressCallback progressCallback)
        {
            LogEvent ev = null;
            List<LogEvent> events = new List<LogEvent>();
            XmlReaderSettings readerSettings = null;
            XmlNamespaceManager nsManager = null;
            XmlParserContext context = null;
            XmlReader reader = null;

            // Prepares the reader settings
            readerSettings = new XmlReaderSettings();
            readerSettings.ConformanceLevel = ConformanceLevel.Fragment;
            readerSettings.IgnoreComments = true;
            readerSettings.IgnoreWhitespace = true;


            // Setup the lon4net namespaces
            nsManager = new XmlNamespaceManager(new NameTable());
            nsManager.AddNamespace(XML_NAMESPACE, XML_NAMESPACE);
            context = new XmlParserContext(null, nsManager, null, XmlSpace.None);

            // Loads the file
            reader = XmlReader.Create(input, readerSettings, context);
            while ((ev = ReadEventFromXml(reader, _nextEventNumber)) != null)
            {
                _nextEventNumber++;
                events.Add(ev);
                if ((events.Count % 100) == 0 && progressCallback != null)
                    progressCallback(events.Count);
            }

            if (progressCallback != null)
                progressCallback(events.Count);

            // Returns the events
            return events;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Normalizes the new lines character in the specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The text for which to normalize the new lines.</param>
        /// <returns>The value of <paramref name="text"/> with new lines normalized to <see cref="Environment.NewLine"/>.</returns>
        private string NormalizeNewLines(string text)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            return _newLineNormRegex.Replace(text, Environment.NewLine);
        }

        /// <summary>
        /// Reads the next event from the specified reader.
        /// </summary>
        /// <param name="reader">The <see cref="XmlReader"/> from which to read.</param>
        /// <param name="eventNumber">The event number.</param>
        /// <returns>
        /// A new <see cref="LogEvent"/> instance that represents the loaded event; <c>null</c> if there are no more events to load.
        /// </returns>
        private LogEvent ReadEventFromXml(XmlReader reader, int eventNumber)
        {
            string logger = String.Empty;
            DateTime timeStamp = DateTime.MinValue;
            LogLevels level = LogLevels.Debug;
            string thread = String.Empty;
            string domain = String.Empty;
            string userName = String.Empty;
            string message = String.Empty;
            string exception = String.Empty;
            string className = String.Empty;
            string methodName = String.Empty;
            string fileName = String.Empty;
            int lineNumber = -1;

            if (reader == null)
                throw new ArgumentNullException("reader");

            // Moves to the next event.
            //while (reader.Read() && reader.Name != "event" && reader.NodeType != XmlNodeType.Element)
            //    ;
            if (!reader.ReadToFollowing("event", XML_NAMESPACE))
                return null;

            // Reads the attributes of the event tag
            if (reader.MoveToFirstAttribute())
            {
                do
                {
                    switch (reader.Name.ToLower())
                    {
                        case "logger":
                            logger = reader.Value;
                            break;
                        case "timestamp":
                            try { timeStamp = XmlConvert.ToDateTime(reader.Value, XmlDateTimeSerializationMode.Local); }
                            catch { timeStamp = DateTime.MinValue; }
                            break;
                        case "level":
                            try { level = (LogLevels)Enum.Parse(typeof(LogLevels), reader.Value, true); }
                            catch { level = LogLevels.None; }
                            break;
                        case "thread":
                            thread = reader.Value;
                            break;
                        case "domain":
                            domain = reader.Value;
                            break;
                        case "username":
                            userName = reader.Value;
                            break;
                    }
                }
                while (reader.MoveToNextAttribute());
                reader.MoveToElement();
            }

            // Read inner elements            
            reader.Read();
            while (!(reader.LocalName == "event" && reader.NodeType == XmlNodeType.EndElement) && !reader.EOF)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.LocalName == "message")
                        message = NormalizeNewLines(reader.ReadElementContentAsString());
                    else if (reader.LocalName == "exception")
                        exception = NormalizeNewLines(reader.ReadElementContentAsString());
                    else if (reader.LocalName == "locationInfo")
                    {
                        if (reader.MoveToFirstAttribute())
                        {
                            do
                            {
                                switch (reader.Name.ToLower())
                                {
                                    case "class":
                                        className = reader.Value;
                                        break;
                                    case "method":
                                        methodName = reader.Value;
                                        break;
                                    case "file":
                                        fileName = reader.Value;
                                        break;
                                    case "line":
                                        if (!Int32.TryParse(reader.Value, out lineNumber))
                                            lineNumber = -1;
                                        break;
                                }
                            }
                            while (reader.MoveToNextAttribute());
                            reader.MoveToElement();
                            reader.Read();
                        }
                    }
                    else
                        reader.Read();
                }
                else
                    reader.Read();
            }

            return new LogEvent(
                eventNumber,
                logger,
                timeStamp,
                level,
                thread,
                domain,
                userName,
                message,
                exception,
                className,
                methodName,
                fileName,
                lineNumber);
        }

        #endregion Private Methods
    }
}
