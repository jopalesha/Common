using Jopalesha.Common.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Jopalesha.Common.Tests
{
    public class TestWebApplicationFactory<TStartUp, TTestStartUp> 
        : WebApplicationFactory<TStartUp>
        where TStartUp : Startup
        where TTestStartUp : Startup
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) => builder
            .ConfigureServices(services => { services.AddSingleton(opt => new StartupOptions(false)); })
            .UseStartup<TTestStartUp>();
    }
}
