using System;
using DasMulli.Win32.ServiceUtils;
using Microsoft.AspNetCore.Hosting;

namespace Jopalesha.Common.Hosting.Components
{
    internal class WebHostService : IWin32Service
    {
        private readonly IWebHost _webHost;

        public WebHostService(Func<IWebHost> factory, string serviceName) 
        {
            _webHost = factory();
            ServiceName = serviceName;
        }

        public string ServiceName { get; }

        public void Start(string[] startupArguments, ServiceStoppedCallback serviceStoppedCallback)
        {
            _webHost.Start();
        }

        public void Stop()
        {
            _webHost.StopAsync().Wait();
            _webHost.Dispose();
        }
    }
}
