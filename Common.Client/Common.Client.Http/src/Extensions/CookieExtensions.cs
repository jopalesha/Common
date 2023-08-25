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
        /// <summary>
        /// Convert cookie to header string.
        /// </summary>
        /// <param name="cookie">Cookie.</param>
        /// <returns>Cookie as string.</returns>
        public static string ToHeaderValue(this Cookie cookie)
        {
            return string.Format("{0}={1}", new object[]
            {
                cookie.Name,
                cookie.Value
            });
        }

        /// <summary>
        /// Get cookie by name.
        /// </summary>
        /// <param name="container">Cookie container.</param>
        /// <param name="uri">Uri.</param>
        /// <param name="names">Cookie name.</param>
        /// <returns>List of cookies.</returns>
        public static IList<Cookie> GetCookiesByName(
            this CookieContainer container,
            Uri uri,
            params string[] names)
        {
            return container.GetCookies(uri)
                .Where(c => names.Contains(c.Name))
                .ToList();
        }
    }
}
