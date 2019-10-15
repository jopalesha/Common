using System;
using Jopalesha.Common.Infrastructure.Configuration;

namespace Jopalesha.Common.Client.Http
{
    public class ProxyOptionsProvider : IProxyOptionsProvider
    {
        private readonly Lazy<ProxyOptions> _proxyOptions;

        public ProxyOptionsProvider(IConfiguration configuration)
        {
            _proxyOptions = new Lazy<ProxyOptions>(() => Initialize(configuration));
        }

        public ProxyOptions GetOptions() =>
            _proxyOptions.Value ?? throw new ArgumentException("Can't retrieve options from config");

        private static ProxyOptions Initialize(IConfiguration configuration)
        {
            return configuration.GetSection<ProxyOptions>("proxy");
        }
    }
}