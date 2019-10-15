using System.Net;

namespace Jopalesha.Common.Client.Http
{
    public interface IProxyCreator
    {
        WebProxy Create();
    }
}
