using System;
using Jopalesha.Common.Infrastructure.Configuration.Json;
using SimpleInjector;
using Xunit;
using HttpClientOptions = Jopalesha.Common.Client.Http.Models.HttpClientOptions;

namespace Jopalesha.Common.Client.Http.Tests
{
    public class HttpClientFactoryTests
    {
        private readonly IHttpClientFactory _sut;

        public HttpClientFactoryTests()
        {
            var container = new Container();
            container.UseHttpClient();
            container.UseJsonConfiguration();

            _sut = container.GetInstance<IHttpClientFactory>();
        }

        [Fact]
        public void Create_ConstructsClientWithPassedOption()
        {
            var uri = new Uri("https://ya.ru");
            var timeout = TimeSpan.FromSeconds(30);
            var options = new HttpClientOptions(uri, timeout);

            var client = _sut.Create(options);

            Assert.Equal(uri, client.BaseAddress);
            Assert.Equal(timeout, client.Timeout);
        }

        [Fact]
        public void Create_FromConfig_ConstructsClientWithConfigOptions()
        {
            var client = _sut.Create(HttpClientOptions.FromConfig("hui"));
        }
    }
}
