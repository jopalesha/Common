using Jopalesha.Common.Client.Http.Models;

namespace Jopalesha.Common.Client.Http
{
    /// <summary>
    /// Http client factory.
    /// </summary>
    public interface IHttpClientFactory
    {
        /// <summary>
        /// Create <see cref="HttpClientOptions"/> with passed options.
        /// </summary>
        /// <param name="options">Client options.</param>
        /// <returns>Http client.</returns>
        System.Net.Http.HttpClient Create(HttpClientOptions options);
    }
}
