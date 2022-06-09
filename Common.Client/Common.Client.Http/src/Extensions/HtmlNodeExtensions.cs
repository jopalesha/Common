using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using Jopalesha.Common.Infrastructure.Logging;
using Jopalesha.Helpers;

namespace Jopalesha.Common.Client.Http.Extensions
{
    /// <summary>
    /// <see cref="HtmlNode"/> extensions.
    /// </summary>
    public static class HtmlNodeExtensions
    {
        // TODO REMOVE LOGGER.
        private static readonly ILogger _logger = LoggerFactory.Create();

        public static IList<string> GetListOfString(this HtmlNode node, string path)
        {
            return node.SelectNodes(path)?.Select(htmlNode => htmlNode?.InnerHtml.Trim())
                .Where(innerHtml => !string.IsNullOrWhiteSpace(innerHtml)).ToList();
        }

        public static IList<double> GetListOfDouble(this HtmlNode node, string path)
        {
            var result = new List<double>();

            var nodes = node.SelectNodes(path);

            if (nodes != null)
            {
                foreach (var htmlNode in nodes)
                {
                    var innerHtml = htmlNode.InnerHtml.Trim();

                    if (StringHelper.TryParseToDouble(innerHtml, out var value))
                    {
                        result.Add(value);
                    }
                    else
                    {
                        _logger.Error($"Can't convert {innerHtml} to double");
                    }
                }
            }
            else
            {
                _logger.Error($"Can't parse {node.InnerHtml} by {path}");
            }

            return result;
        }

        /// <summary>
        /// Get value by <see cref="P:HtmlAgilityPack.HtmlNode.XPath" /> expression.
        /// </summary>
        /// <param name="node">Html node.</param>
        /// <param name="path">The XPath expression.</param>
        /// <param name="needLogError">Flag, which indicates should we log problem nods or not.</param>
        /// <typeparam name="T">Value type.</typeparam>
        /// <returns>Value.</returns>
        public static T GetValue<T>(this HtmlNode node, string path, bool needLogError = true)
        {
            var stringValue = node.SelectSingleNode(path)?.InnerText.Trim();

            if (!string.IsNullOrEmpty(stringValue))
            {
                return (T)Convert.ChangeType(stringValue, typeof(T), CultureInfo.InvariantCulture);
            }

            if (needLogError)
            {
                _logger.Error($@"Cant retrieve info from: {node.InnerHtml}
                                For search string: {path}");
            }

            return default;
        }

        /// <summary>
        /// Selects a list of nodes matching the <see cref="P:HtmlAgilityPack.HtmlNode.XPath" /> expression,
        /// or empty list for non-existent node.
        /// </summary>
        /// <param name="node">Html node.</param>
        /// <param name="path">The XPath expression.</param>
        /// <returns>A list of <see cref="HtmlNode"/> matching the <see cref="P:HtmlAgilityPack.HtmlNode.XPath" /> query,
        /// or <c>empty</c> if no node matched the XPath expression.</returns>
        public static IList<HtmlNode> SelectNodeOrEmpty(this HtmlNode node, string path) =>
            node.SelectNodes(path)?.ToList() ?? new List<HtmlNode>();
    }
}
