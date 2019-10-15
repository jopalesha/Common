using System;
using System.Linq;
using System.Web;

namespace Jopalesha.Common.Client.Http.Extensions
{
    public static class UriExtensions
    {
        public static Uri Append(this Uri uri, params object[] paths)
        {
            return new Uri(paths.Aggregate(uri.ToString(), (current, path) =>
                $"{current.TrimEnd('/')}/{path.ToString().TrimStart('/')}"), UriKind.RelativeOrAbsolute);
        }

        public static Uri AddParameter(this Uri uri, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            var parameter = $"{(query.AllKeys.Any() ? "&" : "?")}{paramName}={HttpUtility.UrlEncode(paramValue)}";
            return new Uri(uri + parameter);
        }
    }
}
