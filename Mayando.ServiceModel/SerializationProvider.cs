using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Mayando.ServiceModel
{
    /// <summary>
    /// Provides serialization services.
    /// </summary>
    public static class SerializationProvider
    {
        /// <summary>
        /// The format string for an ISO 8601 date.
        /// </summary>
        public const string Iso8601FormatString = "yyyy-MM-ddTHH:mm:ss.fffZ";

        /// <summary>
        /// Deserializes a string to an object graph.
        /// </summary>
        /// <typeparam name="T">The type of the root object to deserialize.</typeparam>
        /// <param name="value">The serialized value.</param>
        /// <returns>The deserialized value.</returns>
        public static T Deserialize<T>(string value)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var stringReader = new StringReader(value))
            using (var xmlReader = XmlReader.Create(stringReader))
            {
                return (T)serializer.ReadObject(xmlReader);
            }
        }

        /// <summary>
        /// Serializes an object graph to a string.
        /// </summary>
        /// <typeparam name="T">The type of the root object to serialize.</typeparam>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The serialized value.</returns>
        public static string Serialize<T>(T value)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var output = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(output))
            {
                serializer.WriteObject(xmlWriter, value);
            }
            return output.ToString();
        }

        /// <summary>
        /// Serializes a <see cref="DateTimeOffset"/> to an ISO 8601 format.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The serialized value.</returns>
        public static string FormatDateIso8601(DateTimeOffset value)
        {
            return value.UtcDateTime.ToString(Iso8601FormatString, CultureInfo.InvariantCulture);
        }
    }
}