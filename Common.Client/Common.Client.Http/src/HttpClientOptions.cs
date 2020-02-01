using System;
using Jopalesha.Common.Infrastructure.Helpers;

namespace Jopalesha.Common.Client.Http
{
    public class HttpClientOptions
    {
        public HttpClientOptions(Uri uri) : this(uri, TimeSpan.FromMinutes(5), false)
        {
        }

        public HttpClientOptions(Uri url, TimeSpan timeOut, bool useProxy)
        {
            Url = Check.NotNull(url, nameof(url));
            TimeOut = Check.IsTrue(timeOut, it => it.TotalSeconds > 0, nameof(timeOut));
            UseProxy = useProxy;
        }

        public Uri Url { get; }

        public TimeSpan TimeOut { get; }

        public bool UseProxy { get; set; }
    }
}
