using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Application.Mediator;
using Jopalesha.Common.Infrastructure.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Jopalesha.Common.Infrastructure.Cache.Sql.Tests.Sample
{
    internal class Program
    {
        private static async Task Main()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.UseSqlCache(new SqlCacheOptions("CacheConnection", true));
            container.AddMediator();
            container.AddAsyncQueue();
            container.Register(LoggerFactory.Create, Lifestyle.Singleton);

            container.Verify();


            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var service = container.GetInstance<AsyncQueueConsumer>();
                await Task.Factory.StartNew(() => service.ExecuteAsync(CancellationToken.None));

                var cache = container.GetInstance<ICache>();
                
                
                
                
                
                await cache.Add("key", "value");

                await Task.Delay(25000);

                var value = await cache.Get<string>("key");
            }
        }
    }
}
