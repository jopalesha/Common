using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Application.Mediator;
using Jopalesha.Common.Infrastructure.Cache.Common;
using Jopalesha.Common.Infrastructure.Logging;
using Jopalesha.Common.Infrastructure.Logging.Console;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Jopalesha.Common.Infrastructure.Cache.Sql.Tests.Sample
{
    internal class Program
    {
        private static async Task Main()
        {
            var container = new Container();
            LoggerFactory.SetCurrent(new ConsoleLoggerFactory());
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.UseSqlCache(new SqlCacheOptions("CacheConnection", true));
            container.AddMediator();
            container.AddAsyncQueue();
            container.Register(LoggerFactory.Create, Lifestyle.Singleton);

            container.Verify();

            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var services = container.GetInstance<IEnumerable<IBackgroundService>>().ToList();
                var cacheService = services.OfType<CacheBackgroundService>().Single();
                var asyncQueueService = services.OfType<AsyncQueueConsumer>().Single();

                await Task.Factory.StartNew(() => cacheService.ExecuteAsync(CancellationToken.None));
                await Task.Factory.StartNew(() => asyncQueueService.ExecuteAsync(CancellationToken.None));

                var cache = container.GetInstance<ICache>();

                var key = Guid.NewGuid().ToString();

                await cache.Add(key, "value");

                await Task.Delay(25000);
                Console.ReadKey();

                var value = await cache.Get<string>(key);
                Console.WriteLine(value);
            }
        }
    }
}
