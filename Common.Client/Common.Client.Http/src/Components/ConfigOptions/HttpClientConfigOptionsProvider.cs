using System;
using System.Net;
using Jopalesha.Common.Client.Http.Models;
using Jopalesha.Common.Infrastructure.Configuration;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable ClassNeverInstantiated.Local

namespace Jopalesha.Common.Client.Http.Components.ConfigOptions
{
    /// <summary>
    /// Http options provider from configuration file.
    /// </summary>
    internal class HttpClientConfigOptionsProvider : IHttpClientConfigOptionsProvider
    {
        private readonly IConfiguration _configuration;

        public HttpClientConfigOptionsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        ///<inheritdoc />
        public HttpClientOptions Get(string section)
        {
            var options = _configuration.GetSection<HttpOptionsConfig>(section);
            var url = new Uri(options.Url);
            var timeout = options.Timeout.HasValue
                ? TimeSpan.FromSeconds(options.Timeout.Value)
                : TimeSpan.FromMinutes(5);

            return options.Proxy != null
                ? new HttpClientOptions(url, timeout, CreateProxyOptions(options.Proxy))
                : new HttpClientOptions(url, timeout);
        }

        private static ProxyOptions CreateProxyOptions(ProxyOptionsConfig config)
        {
            if (config.Credentials != null)
            {
                return new ProxyOptions(new Uri(config.Url), config.Type,
                    new NetworkCredential(config.Credentials.Login, config.Credentials.Password));
            }

            return new ProxyOptions(new Uri(config.Url), config.Type);
        }

        private class HttpOptionsConfig
        {
            public string Url { get; set; }

            public int? Timeout { get; set; }

            public ProxyOptionsConfig Proxy { get; set; }
        }

        private class ProxyOptionsConfig
        {
            public string Url { get; set; }

            public ProxyType Type { get; set; }

            public Credentials Credentials { get; set; }
        }

        private class Credentials
        {
            public string Login { get; set; }

            public string Password { get; set; }
        }
    }
}
