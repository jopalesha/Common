using System.Net;

namespace Jopalesha.Common.Client.Http
{
    public interface IProxyFactory
    {
        IWebProxy Create(ProxyOptions options);
    }
}