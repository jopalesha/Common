using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jopalesha.Common.Client.Http
{
    public static class HttpClientExtensions
    {
        public static async Task<T> Get<T>(this HttpClient client, Uri address, CancellationToken token) =>
            await GetResult<T>(client.GetAsync(address, token), token);

        public static async Task<TResult> Get<TRequest, TResult>(
            this HttpClient client,
            Uri address,
            TRequest data,
            CancellationToken token)
        {
            using var request = new HttpRequestMessage
            {
                RequestUri = address,
                Method = HttpMethod.Get,
                Content = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)))
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await GetResult<TResult>(client.SendAsync(request, token), token);
        }

        public static async Task<TResult> Put<TRequest, TResult>(
            this HttpClient client,
            Uri address,
            TRequest request,
            CancellationToken token) => await GetResult<TResult>(client.PutAsJsonAsync(address, request, token), token);

        public static async Task Post<TRequest>(
            this HttpClient client, 
            Uri address, 
            TRequest request,
            CancellationToken token) => await ProcessTask(client.PostAsJsonAsync(address, request, token));

        public static async Task<TResult> Post<TRequest, TResult>(
            this HttpClient client,
            Uri address,
            TRequest request,
            CancellationToken token) => await GetResult<TResult>(client.PostAsJsonAsync(address, request, token), token);

        public static async Task Delete(
            this HttpClient client,
            Uri address, 
            CancellationToken token) => await ProcessTask(client.DeleteAsync(address, token));

        public static async Task<TResult> Delete<TResult>(
            this HttpClient client,
            Uri address,
            CancellationToken token) => await GetResult<TResult>(client.DeleteAsync(address, token), token);


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
