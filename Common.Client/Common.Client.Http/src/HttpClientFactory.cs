using System.Net.Http;
using Jopalesha.Common.Infrastructure.Helpers;

namespace Jopalesha.Common.Client.Http
{
    public class HttpClientFactory: IHttpClientFactory
    {
        private readonly IProxyCreator _proxyCreator;

        public HttpClientFactory(IProxyCreator proxyCreator)
        {
            _proxyCreator = proxyCreator;
        }

        public HttpClient Create(HttpClientOptions options)
        {
            Check.NotNull(options);

            HttpClient client;

            if (options.UseProxy)
            {
                client = System.Net.Http.HttpClientFactory.Create(new HttpClientHandler
                {
                    Proxy = _proxyCreator.Create()
                });
            }
            else
            {
                client = System.Net.Http.HttpClientFactory.Create();
            }

            client.Timeout = options.TimeOut;
            client.BaseAddress = options.Url;
            return client;
        }
    }
}
