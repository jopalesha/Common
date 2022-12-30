using System;
using Jopalesha.CheckWhenDoIt;

// ReSharper disable UnusedMember.Global
namespace Jopalesha.Common.Client.Http.Models
{
    /// <summary>
    /// Http client options.
    /// </summary>
    public partial class HttpClientOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientOptions"/> class.
        /// </summary>
        /// <param name="uriString">A string that identifies the resource to be represented by the <see cref="T:System.Uri" /> instance.
        /// Note that an IPv6 address in string form must be enclosed within brackets. For example, "http://[2607:f8b0:400d:c06::69]".</param>
        public HttpClientOptions(string uriString) : this(new Uri(uriString))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientOptions"/> class.
        /// </summary>
        /// <param name="url">Base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.</param>
        public HttpClientOptions(Uri url) : this(url, TimeSpan.FromMinutes(5))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientOptions"/> class with proxy settings.
        /// </summary>
        /// <param name="url">Url.</param>
        /// <param name="timeout">Timeout.</param>
        /// <param name="options">Options.</param>
        public HttpClientOptions(
            Uri url,
            TimeSpan timeout,
            ProxyOptions options) : this(url, timeout)
        {
            ProxyOptions = Check.NotNull(options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientOptions"/> class.
        /// </summary>
        /// <param name="url">Base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.</param>
        /// <param name="timeout">The timespan to wait before the request times out.</param>
        public HttpClientOptions(Uri url, TimeSpan timeout) : this()
        {
            Url = Check.NotNull(url, nameof(url));
            Timeout = Check.True(timeout, it => it.TotalSeconds > 0, nameof(timeout));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientOptions"/> class.
        /// Used in base classes.
        /// </summary>
        protected HttpClientOptions()
        {
        }

        /// <summary>
        /// Gets url.
        /// </summary>
        public Uri Url { get; }

        /// <summary>
        /// Gets timeout.
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Gets proxy options.
        /// </summary>
        public ProxyOptions ProxyOptions { get; }
    }
}
