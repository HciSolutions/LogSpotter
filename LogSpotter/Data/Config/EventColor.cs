using System;
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;

namespace HciSolutions.LogSpotter.Data.Config
{
    /// <summary>
    /// Represents the configuration for the event colors.
    /// </summary>
    public class EventColor : IXmlSerializable
    {
        #region Private Members
        private Color _backgroundColor;
        private Color _foregroundColor;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EventColor"/> class.
        /// </summary>
        public EventColor()
            : this(Color.White, Color.Black)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventColor"/> class.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="foregroundColor">Color of the foreground.</param>
        public EventColor(Color backgroundColor, Color foregroundColor)
        {
            _backgroundColor = backgroundColor;
            _foregroundColor = foregroundColor;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => _backgroundColor = value;
        }

        /// <summary>
        /// Gets or sets the color of the foreground.
        /// </summary>
        /// <value>The color of the foreground.</value>
        public Color ForegroundColor
        {
            get => _foregroundColor;
            set => _foregroundColor = value;
        }
        #endregion

        #region IXmlSerializable Members
        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute("background"))
                _backgroundColor = Color.FromArgb(Int32.Parse(reader.Value, NumberStyles.HexNumber));
            if (reader.MoveToAttribute("foreground"))
                _foregroundColor = Color.FromArgb(Int32.Parse(reader.Value, NumberStyles.HexNumber));
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("background", _backgroundColor.ToArgb().ToString("X8"));
            writer.WriteAttributeString("foreground", _foregroundColor.ToArgb().ToString("X8"));
        }
        #endregion
    }
}
