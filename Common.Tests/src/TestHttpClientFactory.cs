using System;
using Jopalesha.Common.Client.Http;

namespace Jopalesha.Common.Tests
{
    public class TestHttpClientFactory : IHttpClientFactory
    {
        private readonly Func<System.Net.Http.HttpClient> _factory;

        public TestHttpClientFactory(Func<System.Net.Http.HttpClient> factory)
        {
            _factory = factory;
        }

        public System.Net.Http.HttpClient Create(HttpClientOptions options)
        {
            return _factory();
        }
    }
}
