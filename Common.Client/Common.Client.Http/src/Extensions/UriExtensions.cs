using System;
using System.Linq;
using System.Web;

namespace Jopalesha.Common.Client.Http.Extensions
{
    /// <summary>
    /// <see cref="Uri"/> extensions.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Combine query paths with uri.
        /// </summary>
        /// <param name="uri">Original uri.</param>
        /// <param name="paths">Additional query paths.</param>
        /// <returns>Combined uri.</returns>
        public static Uri Append(this Uri uri, params object[] paths)
        {
            return new Uri(paths.Aggregate(uri.ToString(), (current, path) =>
                $"{current.TrimEnd('/')}/{path.ToString().TrimStart('/')}"), UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Combine <see cref="Uri"/> with query parameter.
        /// </summary>
        /// <param name="uri">Uri.</param>
        /// <param name="key">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        /// <returns>Updated uri.</returns>
        public static Uri AddParameter(this Uri uri, string key, string value)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            var parameter = $"{(query.AllKeys.Any() ? "&" : "?")}{key}={HttpUtility.UrlEncode(value)}";
            return new Uri(uri + parameter);
        }

        /// <summary>
        /// Create uri from ip address.
        /// </summary>
        /// <param name="ip">IP address.</param>
        /// <returns>Uri.</returns>
        public static Uri CreateFromIp(string ip) => new($"https://{ip}", UriKind.Absolute);
    }
}
