using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Jopalesha.Common.Client.Http
{
    public static class HtmlDownloader
    {
        public static async Task<HtmlDocument> GetHtmlSource(string url, WebProxy webProxy = null)
        {
            var doc = new HtmlDocument();

            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;

                if (webProxy != null)
                    wc.Proxy = webProxy;

                doc.LoadHtml(await wc.DownloadStringTaskAsync(new Uri(url)).ConfigureAwait(false));
            }

            return doc;
        }
    }
}
