using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using Jopalesha.Helpers;

namespace Jopalesha.Common.Client.Http.Extensions
{
    /// <summary>
    /// <see cref="HtmlNode"/> extensions.
    /// </summary>
    public static class HtmlNodeExtensions
    {
        /// <summary>
        /// Get list of strings from node.
        /// </summary>
        /// <param name="node">Html node.</param>
        /// <param name="path">The XPath expression.</param>
        /// <returns>List of strings.</returns>
        public static IList<string> GetListOfString(this HtmlNode node, string path)
        {
            return node.SelectNodes(path)?.Select(htmlNode => htmlNode?.InnerHtml.Trim())
                .Where(innerHtml => !string.IsNullOrWhiteSpace(innerHtml)).ToList();
        }

        /// <summary>
        /// Get list of double values from node.
        /// </summary>
        /// <param name="node">Html node.</param>
        /// <param name="path">The XPath expression.</param>
        /// <returns>List of values.</returns>
        public static IList<double> GetListOfDouble(this HtmlNode node, string path)
        {
            var result = new List<double>();

            var nodes = node.SelectNodes(path);

            if (nodes == null)
            {
                throw new ArgumentNullException(
                    $"Can't retrieve double nodes from node {node.InnerHtml} by path {path}.");
            }

            foreach (var htmlNode in nodes)
            {
                var innerHtml = htmlNode.InnerHtml.Trim();

                if (StringHelper.TryParseToDouble(innerHtml, out var value))
                {
                    result.Add(value);
                }
            }

            return result;
        }

        /// <summary>
        /// Get value by <see cref="P:HtmlAgilityPack.HtmlNode.XPath" /> expression.
        /// </summary>
        /// <param name="node">Html node.</param>
        /// <param name="path">The XPath expression.</param>
        /// <typeparam name="T">Value type.</typeparam>
        /// <returns>Value.</returns>
        public static T GetValue<T>(this HtmlNode node, string path)
        {
            if (node.TryGetValue<T>(path, out var result))
            {
                return result;
            }

            throw new ArgumentException($"Can't retrieve value from node {node.InnerHtml} by path {path}");
        }

        /// <summary>
        /// Try get value from html node.
        /// </summary>
        /// <param name="node">Html node.</param>
        /// <param name="path">The XPath expression.</param>
        /// <param name="result">Value.</param>
        /// <typeparam name="T">Value type.</typeparam>
        /// <returns>True if value successfully retrieved; otherwise, false.</returns>
        public static bool TryGetValue<T>(this HtmlNode node, string path, out T result)
        {
            var stringValue = node.SelectSingleNode(path)?.InnerText.Trim();

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                result = default;
                return false;
            }

            result = (T)Convert.ChangeType(stringValue, typeof(T), CultureInfo.InvariantCulture);
            return true;
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
