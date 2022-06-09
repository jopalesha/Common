using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Jopalesha.Common.Client.Http.Extensions
{
    /// <summary>
    /// <see cref="Cookie"/> extensions.
    /// </summary>
    internal static class CookieExtensions
    {
        public static string ToHeaderValue(this Cookie cookie)
        {
            return string.Format("{0}={1}", new object[]
            {
                cookie.Name,
                cookie.Value
            });
        }

        public static IEnumerable<Cookie> GetCookiesByName(this CookieContainer container, Uri uri,
            params string[] names)
        {
            return container.GetCookies(uri).Cast<Cookie>()
                .Where(c => names.Contains(c.Name))
                .ToList();
        }
    }
}
