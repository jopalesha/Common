using Jopalesha.Common.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jopalesha.Common.Tests
{
    public class TestWebApplicationFactory<TStartUp, TTestStartUp> 
        : WebApplicationFactory<TStartUp>
        where TStartUp : Startup
        where TTestStartUp : Startup
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureTestServices(services =>
                    {
                        services.AddMvc().AddApplicationPart(typeof(TStartUp).Assembly);
                    });

                    webBuilder.UseStartup<TTestStartUp>();
                });
        }
    }
}
