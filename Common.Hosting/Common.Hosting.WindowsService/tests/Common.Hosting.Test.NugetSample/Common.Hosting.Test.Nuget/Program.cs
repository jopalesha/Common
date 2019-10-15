using System;
using Jopalesha.Common.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace Common.Hosting.Test.Nuget
{
    internal class Program
    {

        private const string ServiceName = "Hosting.Sample";

        public static void Main(string[] args)
        {
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
