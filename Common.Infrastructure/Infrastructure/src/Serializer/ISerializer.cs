namespace Jopalesha.Common.Infrastructure.Serializer
{
    /// <summary>
    /// Provides methods for converting between .NET types and serializable string
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Deserializes string to the specified .NET type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="value">The string to deserialize.</param>
        /// <returns>The deserialized object from string.</returns>
        T DeserializeObject<T>(string value);

        /// <summary>
        /// Serializes the specified object to string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>A string representation of the object.</returns>
        public string SerializeObject(object value);
    }
}
