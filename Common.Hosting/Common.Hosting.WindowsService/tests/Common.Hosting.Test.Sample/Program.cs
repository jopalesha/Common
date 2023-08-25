using Jopalesha.Common.Infrastructure.Logging;
using Jopalesha.Common.Infrastructure.Logging.Console;
using Microsoft.Extensions.Hosting;

namespace Jopalesha.Common.Hosting.Test.Sample
{
    internal class Program
    {
        private const string ServiceName = "Hosting.Sample";

        public static void Main(string[] args)
        {
            LoggerFactory.SetCurrent(new ConsoleLoggerFactory());
            Host.Run(new HostingOptions(ServiceName, args), CreateHostBuilder(args));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            HostBuilder.Create<SampleStartup>(args);
    }
}
