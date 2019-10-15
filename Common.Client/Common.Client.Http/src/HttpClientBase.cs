using System;
using System.Net.Http;
using Jopalesha.Common.Infrastructure;

namespace Jopalesha.Common.Client.Http
{
    public abstract class HttpClientBase : IDisposable
    {
        private readonly Lazy<HttpClient> _httpClient;

        protected HttpClientBase(Func<HttpClient> clientFactory)
        {
            Check.NotNull(clientFactory, nameof(clientFactory));
            _httpClient = new Lazy<HttpClient>(clientFactory);
        }

        protected HttpClient Client => _httpClient.Value;

        public void Dispose()
        {
            if (_httpClient.IsValueCreated)
            {
                _httpClient.Value?.Dispose();
            }
        }
    }
}
