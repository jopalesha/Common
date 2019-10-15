using System.Net;

namespace Jopalesha.Common.Client.Http
{
    public class NullProxyCreator : IProxyCreator
    {
        public WebProxy Create()
        {
            return null;
        }
    }
}
