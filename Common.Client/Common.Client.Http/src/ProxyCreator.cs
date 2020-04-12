using System.Net;
using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Client.Http
{
    public class ProxyCreator : IProxyCreator
    {
        private readonly string _address;

        public ProxyCreator(string address)
        {
            _address = Check.NotEmpty(address);
        }

        public virtual WebProxy Create()
        {
            return new WebProxy(_address);
        }

        public static ProxyCreator Create(ProxyOptions options)
        {
            Check.NotNull(options);

            if (string.IsNullOrEmpty(options.Login) || string.IsNullOrEmpty(options.Password))
            {
                return new ProxyCreator(options.Address);
            }

            return new ProxyCreatorWithCredentials(options.Address, options.Login, options.Password);
        }
    }
}
