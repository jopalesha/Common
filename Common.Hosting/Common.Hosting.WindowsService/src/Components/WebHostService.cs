using DasMulli.Win32.ServiceUtils;
using Microsoft.Extensions.Hosting;

namespace Jopalesha.Common.Hosting.Components
{
    internal class HostService : IWin32Service
    {
        private readonly IHost _host;

        public HostService(IHost host, string serviceName)
        {
            _host = host;
            ServiceName = serviceName;
        }

        public string ServiceName { get; }

        public void Start(string[] startupArguments, ServiceStoppedCallback serviceStoppedCallback)
        {
            _host.Start();
        }

        public void Stop()
        {
            _host.StopAsync().Wait();
            _host.Dispose();
        }
    }
}
