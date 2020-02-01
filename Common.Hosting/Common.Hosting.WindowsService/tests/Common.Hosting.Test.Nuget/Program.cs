using Jopalesha.Common.Hosting;
using Microsoft.Extensions.Hosting;
using Host = Jopalesha.Common.Hosting.Host;

namespace Common.Hosting.Test.Nuget
{
    public class Program
    {
        private const string ServiceName = "Hosting.Sample";

        public static void Main(string[] args)
        {
            Host.Run(new HostingOptions(ServiceName, args), CreateHostBuilder(args));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Jopalesha.Common.Hosting.HostBuilder.Create<SampleStartup>(args);
    }
}
