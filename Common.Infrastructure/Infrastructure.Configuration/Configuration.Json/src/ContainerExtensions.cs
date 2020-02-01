using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Configuration.Json
{
    public static class ContainerExtensions
    {
        public static void UseJsonConfiguration(this Container container)
        {
            container.Register<IConfiguration, JsonConfiguration>(Lifestyle.Singleton);
        }
    }
}
