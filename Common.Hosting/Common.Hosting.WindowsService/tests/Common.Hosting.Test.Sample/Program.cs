using System;
using Jopalesha.Common.Hosting.Test.Sample.Components;
using Jopalesha.Common.Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
// ReSharper disable UnusedMember.Global

namespace Jopalesha.Common.Hosting.Test.Sample
{
    internal class Program
    {
        private const string ServiceName = "Hosting.Sample";

        public static void Main(string[] args)
        {
            LoggerFactory.SetCurrent(new ConsoleLoggerFactory());
            Host.Run<SampleStartup>(new HostingOptions(ServiceName, args), SetUpOptions);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateWebHostBuilder<Startup>(args, SetUpOptions);

        private static Action<StartUpOptionsBuilder> SetUpOptions => builder =>
        {
            builder.EnableSwagger();
        };
    }
}
