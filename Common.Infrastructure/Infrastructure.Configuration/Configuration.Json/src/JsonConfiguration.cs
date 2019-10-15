using System;
using Microsoft.Extensions.Configuration;

namespace Jopalesha.Common.Infrastructure.Configuration.Json
{
    public class JsonConfiguration : IConfiguration
    {
        private readonly Lazy<IConfigurationRoot> _configurationRoot = new Lazy<IConfigurationRoot>(Initialize);

        public T GetSection<T>(string section)
        {
            return _configurationRoot.Value.GetSection(section).Get<T>();
        }

        public string GetConnection(string value)
        {
            return _configurationRoot.Value.GetConnectionString(value);
        }

        private static IConfigurationRoot Initialize()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
