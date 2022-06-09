using System.Net.Http;
using Jopalesha.CheckWhenDoIt;
using Jopalesha.Common.Client.Http.Models;

namespace Jopalesha.Common.Client.Http.Components.ConfigOptions
{
    /// <inheritdoc />
    internal class HttpClientFactory : IHttpClientFactory
    {
        private readonly IProxyFactory _proxyFactory;
        private readonly IHttpClientConfigOptionsProvider _optionsProvider;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="proxyFactory"><see cref="IProxyFactory"/></param>
        /// <param name="optionsProvider">Options provider.</param>
        public HttpClientFactory(
            IProxyFactory proxyFactory, 
            IHttpClientConfigOptionsProvider optionsProvider)
        {
            _proxyFactory = proxyFactory;
            _optionsProvider = optionsProvider;
        }

        /// <inheritdoc />
        public HttpClient Create(HttpClientOptions options)
        {
            Check.NotNull(options);

            HttpClient client;

            if (options is HttpClientConfigOptions configOptions)
            {
                options = _optionsProvider.Get(configOptions.Section);
            }

            if (options.ProxyOptions != null)
            {
                client = System.Net.Http.HttpClientFactory.Create(new HttpClientHandler
                {
                    Proxy = _proxyFactory.Create(options.ProxyOptions),
                    UseProxy = true
                });
            }
            else
            {
                client = System.Net.Http.HttpClientFactory.Create();
            }

            client.Timeout = options.Timeout;
            client.BaseAddress = options.Url;
            return client;
        }
    }
}
