using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Tests;
using Jopalesha.Common.Client.Http;
using Xunit;

namespace Common.Hosting.Test.Nuget.Tests
{
    public class InheritedStartupTests : IClassFixture<TestWebApplicationFactory<SampleStartup, TestSampleStartup>>
    {
        private readonly HttpClient _client;

        public InheritedStartupTests(TestWebApplicationFactory<SampleStartup,TestSampleStartup> factory)
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

    public class TestSampleStartup : SampleStartup
    {

    }
}
