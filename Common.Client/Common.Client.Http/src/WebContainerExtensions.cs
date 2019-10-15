using SimpleInjector;

namespace Jopalesha.Common.Client.Http
{
    public static class WebContainerExtensions
    {
        public static void UseHttpClient(this Container container)
        {
            container.Register<IProxyFactory, ProxyFactory>(Lifestyle.Singleton);
            container.Register<IProxyOptionsProvider, ProxyOptionsProvider>(Lifestyle.Singleton);
        }
    }
}
