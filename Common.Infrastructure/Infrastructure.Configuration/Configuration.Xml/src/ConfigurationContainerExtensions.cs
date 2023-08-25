using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Configuration.Xml
{
    public static class ConfigurationContainerExtensions
    {
        public static void UseXmlConfiguration(this Container container)
        {
            container.Register<IConfiguration, XmlConfiguration>(Lifestyle.Singleton);
        }
    }
}
