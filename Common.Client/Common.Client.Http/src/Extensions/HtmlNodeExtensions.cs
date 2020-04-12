using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using Jopalesha.Common.Infrastructure.Logging;
using Jopalesha.Helpers;

namespace Jopalesha.Common.Client.Http.Extensions
{
    public static class HtmlNodeExtensions
    {
        private static readonly ILogger _logger = LoggerFactory.Create();

        public static IList<string> GetListOfString(this HtmlNode node, string search)
        {
            return node.SelectNodes(search)?.Select(htmlNode => htmlNode?.InnerHtml.Trim())
                .Where(innerHtml => !string.IsNullOrWhiteSpace(innerHtml)).ToList();
        }

        public static IList<double> GetListOfDouble(this HtmlNode node, string search)
        {
            var result = new List<double>();

            var nodes = node.SelectNodes(search);

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
                _logger.Error($"Can't parse {node.InnerHtml} by {search}");
            }

            return result;
        }

        public static T GetAsType<T>(this HtmlNode node, string search, bool needLogError = true)
        {
            var stringValue = node.SelectSingleNode(search)?.InnerText.Trim();

            if (!string.IsNullOrEmpty(stringValue))
            {
                return (T) Convert.ChangeType(stringValue, typeof (T), CultureInfo.InvariantCulture);
            }

            if (needLogError)
            {
                _logger.Error($@"Cant retrieve info from: {node.InnerHtml}
                                For search string: {search}");
            }

            return default;
        }

        public static IList<HtmlNode> SelectNodeOrEmpty(this HtmlNode node, string search)
        {
            return node.SelectNodes(search)?.ToList() ?? new List<HtmlNode>();
        }
    }
}
