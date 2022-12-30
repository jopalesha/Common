using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jopalesha.Common.Client.Http.Extensions
{
    /// <summary>
    /// <see cref="HttpClient"/> extensions.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// GET request, which retrieves data from response.
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="client">Http client.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Response data.</returns>
        public static async Task<T> Get<T>(this HttpClient client, Uri requestUri, CancellationToken token) =>
            await GetResult<T>(client.GetAsync(requestUri, token), token);

        /// <summary>
        /// GET request, which retrieves data from response.
        /// </summary>
        /// <typeparam name="TRequest">Request data type.</typeparam>
        /// <typeparam name="TResponse">Response data type.</typeparam>
        /// <param name="client">Http client.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="data">Request data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Response data.</returns>
        public static async Task<TResponse> Get<TRequest, TResponse>(
            this HttpClient client,
            Uri requestUri,
            TRequest data,
            CancellationToken token)
        {
            using var request = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Get,
                Content = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)))
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await GetResult<TResponse>(client.SendAsync(request, token), token);
        }

        /// <summary>
        /// PUT request, which retrieves data from response.
        /// </summary>
        /// <typeparam name="TRequest">Request data type.</typeparam>
        /// <typeparam name="TResponse">Response data type.</typeparam>
        /// <param name="client">Http client.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="request">Request data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Response data.</returns>
        public static async Task<TResponse> Put<TRequest, TResponse>(
            this HttpClient client,
            Uri requestUri,
            TRequest request,
            CancellationToken token) => await GetResult<TResponse>(client.PutAsJsonAsync(requestUri, request, token), token);

        /// <summary>
        /// POST request.
        /// </summary>
        /// <typeparam name="TRequest">Request data type.</typeparam>
        /// <param name="client">Http client.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="request">Request data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Post<TRequest>(
            this HttpClient client,
            Uri requestUri,
            TRequest request,
            CancellationToken token) => await ProcessTask(client.PostAsJsonAsync(requestUri, request, token));

        /// <summary>
        /// POST request, which retrieves data from json.
        /// </summary>
        /// <typeparam name="TRequest">Request data type.</typeparam>
        /// <typeparam name="TResponse">Response data type.</typeparam>
        /// <param name="client">Http client.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="request">Request data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Response data.</returns>
        public static async Task<TResponse> Post<TRequest, TResponse>(
            this HttpClient client,
            Uri requestUri,
            TRequest request,
            CancellationToken token) => await GetResult<TResponse>(client.PostAsJsonAsync(requestUri, request, token), token);

        /// <summary>
        /// DELETE request.
        /// </summary>
        /// <param name="client">Http client.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Delete(
            this HttpClient client,
            Uri requestUri,
            CancellationToken token) => await ProcessTask(client.DeleteAsync(requestUri, token));

        /// <summary>
        /// DELETE request and retrieve data from response.
        /// </summary>
        /// <typeparam name="TResponse">Response data type.</typeparam>
        /// <param name="client">Http client.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Response data.</returns>
        public static async Task<TResponse> Delete<TResponse>(
            this HttpClient client,
            Uri requestUri,
            CancellationToken token) => await GetResult<TResponse>(client.DeleteAsync(requestUri, token), token);

        private static async Task<T> GetResult<T>(
            Task<HttpResponseMessage> task,
            CancellationToken token)
        {
            using var response = await task.ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>(token);
        }

        private static async Task ProcessTask(Task<HttpResponseMessage> task)
        {
            using var response = await task.ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
