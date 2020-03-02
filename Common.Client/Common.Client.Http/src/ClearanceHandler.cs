using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Client.Http.Components;
using Jopalesha.Common.Client.Http.Extensions;

namespace Jopalesha.Common.Client.Http
{
    public class ClearanceHandler : DelegatingHandler
    {
        private readonly CookieContainer _cookies = new CookieContainer();
        private const string CloudFlareServerName = "cloudflare-nginx";
        private const string IdCookieName = "__cfduid";
        private const string ClearanceCookieName = "cf_clearance";
        private readonly HttpClient _client;

        public ClearanceHandler(IWebProxy proxy = null)
            : this(new HttpClientHandler(), proxy)
        {
        }

        public ClearanceHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
            var httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                UseCookies = true,
                CookieContainer = _cookies
            };

            if (innerHandler is HttpClientHandler handler && handler.Proxy != null)
            {
                httpClientHandler.Proxy = handler.Proxy;
                httpClientHandler.UseProxy = true;
            }

            _client = new HttpClient(httpClientHandler)
            {
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        public ClearanceHandler(HttpMessageHandler innerHandler, IWebProxy proxy = null)
            : base(innerHandler)
        {
            var httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                UseCookies = true,
                CookieContainer = _cookies,
            };

            if (proxy != null)
            {
                httpClientHandler.Proxy = proxy;
                httpClientHandler.UseProxy = true;
            }

            _client = new HttpClient(httpClientHandler)
            {
                Timeout = TimeSpan.FromSeconds(60.0)
            };
        }

        private HttpClientHandler ClientHandler => InnerHandler.GetMostInnerHandler() as HttpClientHandler;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            EnsureClientHeader(request);
            InjectCookies(request);
            var httpResponseMessage1 = await base.SendAsync(request, cancellationToken);
            var response = httpResponseMessage1;
            if (IsClearanceRequired(response))
            {
                await PassClearance(response, cancellationToken);
                InjectCookies(request);
            }

            var httpResponseMessage2 = await base.SendAsync(request, cancellationToken);
            response = httpResponseMessage2;
            return response;
        }

        private static void EnsureClientHeader(HttpRequestMessage request)
        {
            if (request.Headers.UserAgent.Any())
            {
                return;
            }

            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Client", "1.0"));
        }

        private static bool IsClearanceRequired(HttpResponseMessage response)
        {
            return response.StatusCode == HttpStatusCode.ServiceUnavailable &
                   response.Headers.Server.Any(i => i.Product.Name == CloudFlareServerName) &
                   response.Headers.Contains("Refresh");
        }

        private void InjectCookies(HttpRequestMessage request)
        {
            var list = _cookies.GetCookies(request.RequestUri).Cast<Cookie>().ToList();
            var cookie1 = list.FirstOrDefault(c => c.Name == IdCookieName);
            var cookie2 = list.FirstOrDefault(c => c.Name == ClearanceCookieName);
            if (cookie1 == null || cookie2 == null)
                return;
            if (ClientHandler.UseCookies)
            {
                ClientHandler.CookieContainer.Add(request.RequestUri, cookie1);
                ClientHandler.CookieContainer.Add(request.RequestUri, cookie2);
            }
            else
            {
                request.Headers.Add("Cookie", cookie1.ToHeaderValue());
                request.Headers.Add("Cookie", cookie2.ToHeaderValue());
            }
        }

        private async Task PassClearance(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            SaveIdCookie(response);
            var str = await response.Content.ReadAsStringAsync();
            var pageContent = str;
            var scheme = response.RequestMessage.RequestUri.Scheme;
            var host = response.RequestMessage.RequestUri.Host;
            var solution = ChallengeSolver.Solve(pageContent, host);
            var clearanceUri = $"{scheme}://{host}{solution.ClearanceQuery}";
            await Task.Delay(5000, cancellationToken);
            using var clearanceRequest = new HttpRequestMessage(HttpMethod.Get, clearanceUri);
            if (response.RequestMessage.Headers.TryGetValues("User-Agent", out var userAgent))
            {
                clearanceRequest.Headers.Add("User-Agent", userAgent);
            }

            await _client.SendAsync(clearanceRequest, cancellationToken);
        }

        private void SaveIdCookie(HttpResponseMessage response)
        {
            foreach (var cookieHeader in response.Headers.Where(pair => pair.Key == "Set-Cookie")
                .SelectMany(pair => pair.Value).Where(cookie => cookie.StartsWith("__cfduid=")))
                _cookies.SetCookies(response.RequestMessage.RequestUri, cookieHeader);
        }
    }
}
