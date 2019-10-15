using System.Configuration;

namespace Jopalesha.Common.Infrastructure.Configuration.Xml
{
    internal class XmlConfiguration : IConfiguration
    {
        public string GetConnection(string value)
        {
            return ConfigurationManager.ConnectionStrings[value]
                .ConnectionString;
        }

        public T GetSection<T>(string section)
        {
            return ConfigurationManagerHelper.ReadAppSetting<T>(section);
        }
    }
}
