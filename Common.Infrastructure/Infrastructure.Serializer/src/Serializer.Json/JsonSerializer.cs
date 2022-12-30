using Jopalesha.CheckWhenDoIt;
using Newtonsoft.Json;

namespace Jopalesha.Common.Infrastructure.Serializer.Json
{
    /// <summary>
    /// Newtonsoft json serializer.
    /// </summary>
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public JsonSerializer()
        {
            _settings = null;
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="settings">Specifies the settings on a <see cref="JsonSerializer"/> object.</param>
        public JsonSerializer(JsonSerializerSettings settings)
        {
            _settings = Check.NotNull(settings);
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, _settings);
        }

        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, _settings);
        }
    }
}
