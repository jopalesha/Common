using Microsoft.Extensions.Configuration;

namespace Jopalesha.Common.Infrastructure.Configuration.Json
{
    /// <summary>
    /// Json configuration.
    /// </summary>
    internal class JsonConfiguration : IConfiguration
    {
        private readonly IConfigurationRoot _configurationRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConfiguration"/> class with default initialization.
        /// <br></br>
        /// Use "appsettings.json" file by default.
        /// </summary>
        public JsonConfiguration() : this(DefaultInitialization())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConfiguration"/> class with custom initialization.
        /// </summary>
        /// <param name="root">Configuration root.</param>
        public JsonConfiguration(IConfigurationRoot root)
        {
            _configurationRoot = root;
        }

        /// <inheritdoc />
        public string GetConnection(string value)
        {
            return _configurationRoot.GetConnectionString(value);
        }

        /// <inheritdoc />
        public T GetValue<T>(string section)
        {
            return _configurationRoot.GetSection(section).Get<T>();
        }

        private static IConfigurationRoot DefaultInitialization()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
