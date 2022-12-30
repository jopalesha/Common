using System.Net.Http;

namespace Jopalesha.Common.Client.Http.Extensions
{
    /// <summary>
    /// <see cref="HttpMessageHandler"/> extensions.
    /// </summary>
    internal static class HttpMessageHandlerExtensions
    {
        /// <summary>
        /// Get most inner handler.
        /// </summary>
        /// <param name="handler">Complex handler.</param>
        /// <returns>Handler.</returns>
        public static HttpMessageHandler GetMostInnerHandler(this HttpMessageHandler handler)
        {
            return handler is not DelegatingHandler delegatingHandler
                ? handler
                : delegatingHandler.InnerHandler.GetMostInnerHandler();
        }
    }
}
