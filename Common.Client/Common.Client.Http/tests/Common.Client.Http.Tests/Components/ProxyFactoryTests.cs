using System;
using System.Net;
using Jopalesha.Common.Client.Http.Components;
using Jopalesha.Common.Client.Http.Extensions;
using Jopalesha.Common.Client.Http.Models;
using Xunit;

namespace Jopalesha.Common.Client.Http.Tests.Components
{
    public class ProxyFactoryTests
    {
        private readonly IProxyFactory _sut;

        public ProxyFactoryTests()
        {
            _sut = new ProxyFactory();
        }

        [Fact]
        public void Create_WitNullOptions_ThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => _sut.Create(null));

        [Fact]
        public void Create_HttpProxyType_ReturnsWebProxy()
        {
            var url = UriExtensions.CreateFromIp("178.159.99.50:1085");
            var credentials = new NetworkCredential("login", "password");
            var options = new ProxyOptions(url, ProxyType.Http, credentials);

            var actual = Assert.IsType<WebProxy>(_sut.Create(options));

            Assert.Equal(actual.Address, url);
            Assert.Equal(actual.Credentials, credentials);
        }
    }
}
