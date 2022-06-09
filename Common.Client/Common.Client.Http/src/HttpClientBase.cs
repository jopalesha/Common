using System;
using System.Net.Http;
using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Client.Http
{
    /// <summary>
    /// Base client.
    /// </summary>
    public abstract class HttpClientBase : IDisposable
    {
        private readonly Lazy<HttpClient> _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientBase"/> class with default factory client.
        /// </summary>
        protected HttpClientBase() : this(CreateDefaultClient)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientBase"/> class.
        /// </summary>
        /// <param name="clientFactory">Http client factory.</param>
        protected HttpClientBase(Func<HttpClient> clientFactory)
        {
            Check.NotNull(clientFactory, nameof(clientFactory));
            _httpClient = new Lazy<HttpClient>(clientFactory);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Http client.
        /// </summary>
        protected HttpClient Client => _httpClient.Value;

        /// <summary>
        /// Base dispose.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_httpClient.IsValueCreated)
            {
                _httpClient.Value?.Dispose();
            }
        }

        private static HttpClient CreateDefaultClient() => new();
    }
}
