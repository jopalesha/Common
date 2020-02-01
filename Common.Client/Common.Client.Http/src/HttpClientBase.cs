using System;
using System.Net.Http;
using Jopalesha.Common.Infrastructure.Helpers;

namespace Jopalesha.Common.Client.Http
{
    public abstract class HttpClientBase : IDisposable
    {
        private readonly Lazy<HttpClient> _httpClient;

        protected HttpClientBase() : this(CreateDefaultClient)
        {
        }

        protected HttpClientBase(Func<HttpClient> clientFactory)
        {
            Check.NotNull(clientFactory, nameof(clientFactory));
            _httpClient = new Lazy<HttpClient>(clientFactory);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected HttpClient Client => _httpClient.Value;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_httpClient.IsValueCreated)
                {
                    _httpClient.Value?.Dispose();
                }
            }
        }

        private static HttpClient CreateDefaultClient() => new HttpClient();
    }
}
