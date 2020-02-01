using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Jopalesha.Common.Hosting
{
    public static class HostBuilder
    {
        public static IHostBuilder Create<T>(string[] args)
            where T : Startup
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("hosting.json", true) 
                .Build();

            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<T>(); })
                .ConfigureWebHost(it => { it.UseConfiguration(config); });
        }
    }
}
