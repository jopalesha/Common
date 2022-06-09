using System.Net.Http;

namespace Jopalesha.Common.Client.Http.Extensions
{
    internal static class HttpMessageHandlerExtensions
    {
        public static HttpMessageHandler GetMostInnerHandler(this HttpMessageHandler self)
        {
            return self is not DelegatingHandler delegatingHandler
                ? self
                : delegatingHandler.InnerHandler.GetMostInnerHandler();
        }
    }
}
