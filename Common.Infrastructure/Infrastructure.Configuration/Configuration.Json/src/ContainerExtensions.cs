using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Configuration.Json
{
    /// <summary>
    /// Container extensions.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Set up json configuration with default settings.
        /// <br></br>
        /// Use "appsettings.json" file by default.
        /// </summary>
        /// <param name="container">DI container.</param>
        public static void UseJsonConfiguration(this Container container)
        {
            container.RegisterSingleton<IConfiguration>(() => new JsonConfiguration());
        }

        /// <summary>
        /// Set up json configuration with custom settings.
        /// </summary>
        /// <param name="container">DI container.</param>
        /// <param name="root">Configuration root.</param>
        public static void UseJsonConfiguration(this Container container, IConfigurationRoot root)
        {
            container.RegisterSingleton<IConfiguration>(() => new JsonConfiguration(root));
        }
    }
}
