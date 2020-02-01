using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Client.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Common.Hosting.Test.Nuget.Tests
{
    public class ControllerTests : IClassFixture<WebApplicationFactory<SampleStartup>>
    {
        private readonly HttpClient _client;

        public ControllerTests(WebApplicationFactory<SampleStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ControllerMethods_ReturnsExpected()
        {
            var uri = new Uri("http://localhost/api/test");
            const int expected = 5;

            await _client.Post(uri, expected, CancellationToken.None);
            var actual = await _client.Get<int>(uri, CancellationToken.None);

            Assert.Equal(expected, actual);
        }
    }
}
