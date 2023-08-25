using Jopalesha.Common.Client.Http.Components;
using Jopalesha.Common.Client.Http.Components.ConfigOptions;
using SimpleInjector;

namespace Jopalesha.Common.Client.Http
{
    /// <summary>
    /// Container extensions.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Setup http client.
        /// </summary>
        /// <param name="container">Container.</param>
        public static void UseHttpClient(this Container container)
        {
            container.RegisterSingleton<IProxyFactory, ProxyFactory>();
            container.RegisterSingleton<IHttpClientFactory, HttpClientFactory>();
            container.RegisterSingleton<IHttpClientConfigOptionsProvider, HttpClientConfigOptionsProvider>();
        }
    }
}
