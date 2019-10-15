using System.ComponentModel;
using System.Net;
using Jopalesha.Common.Infrastructure;
using MihaZupan;

namespace Jopalesha.Common.Client.Http
{
    public class ProxyFactory : IProxyFactory
    {
        private readonly IProxyOptionsProvider _optionsProvider;

        public ProxyFactory(IProxyOptionsProvider optionsProvider)
        {
            _optionsProvider = optionsProvider;
        }

        public IWebProxy Create(ProxyOptions options)
        {
            Check.NotNull(options);

            if (options is ConfigProxyOptions)
            {
                options = _optionsProvider.GetOptions();
            }

            IWebProxy proxy;

            switch (options.Type)
            {
                case ProxyType.Http:
                    proxy = new WebProxy(options.Address);
                    break;
                case ProxyType.Socks5:
                    var address = options.Address.Split(':');
                    proxy = new HttpToSocks5Proxy(address[0], int.Parse(address[1]));
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(options.Type));
            }

            if (string.IsNullOrEmpty(options.Login) || string.IsNullOrEmpty(options.Password))
            {
                return proxy;
            }

            proxy.Credentials = new NetworkCredential(options.Login, options.Password);
            return proxy;
        }
    }
}