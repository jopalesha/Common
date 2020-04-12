using System.Net;
using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Client.Http
{
    public class ProxyCreatorWithCredentials : ProxyCreator
    {
        private readonly string _userName;
        private readonly string _password;

        public ProxyCreatorWithCredentials(string address, string userName, string password) : base(address)
        {
            _userName = Check.NotEmpty(userName);
            _password = Check.NotEmpty(password);
        }

        public override WebProxy Create()
        {
            var proxy = base.Create();
            proxy.Credentials = new NetworkCredential(_userName, _password);

            return proxy;
        }
    }
}
