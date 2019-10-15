namespace Jopalesha.Common.Client.Http
{
    public interface IHttpClientFactory
    {
        System.Net.Http.HttpClient Create(HttpClientOptions options);
    }
}
