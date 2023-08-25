using System.ComponentModel;
using System.Net;
using Jopalesha.CheckWhenDoIt;
using Jopalesha.Common.Client.Http.Models;
using MihaZupan;

namespace Jopalesha.Common.Client.Http.Components
{
    /// <inheritdoc />
    internal class ProxyFactory : IProxyFactory
    {
        /// <inheritdoc />
        public IWebProxy Create(ProxyOptions options)
        {
            Check.NotNull(options);

            IWebProxy proxy = options.Type switch
            {
                ProxyType.Http => new WebProxy(options.Address),
                ProxyType.Socks5 => new HttpToSocks5Proxy(options.Address.Host, options.Address.Port),
                _ => throw new InvalidEnumArgumentException(nameof(options.Type))
            };

            proxy.Credentials = options.Credentials;
            return proxy;
        }
    }
}
