using System;
using System.Text;
using System.Threading.Tasks;
using Jopalesha.Common.Infrastructure.Logging;
using Microsoft.AspNetCore.Http;

namespace Jopalesha.Common.Hosting.Middlewares
{
    internal class ExceptionLoggingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionLoggingMiddleware(ILogger logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while request: {await FormatRequest(context.Request)}", ex);
                throw;
            }
        }

        private static async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }
    }
}
