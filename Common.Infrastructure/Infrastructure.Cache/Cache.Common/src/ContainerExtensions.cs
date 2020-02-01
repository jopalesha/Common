using System;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Application.Mediator;
using Jopalesha.Common.Infrastructure.Cache.Common.Handlers;
using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Cache.Common
{
    internal static class ContainerExtensions
    {
        internal static void UseCache<TCache>(this Container container, ICacheOptions options) where TCache: class, ICache
        {
            var cacheType = typeof(TCache);

            if (options.IsBackground)
            {
                container.AddHandler<AddCacheItemRequestHandler, AddCacheItemRequest>();
                container.AddHandler<AddCacheItemRangeRequestHandler, AddCacheItemRangeRequest>();
                container.AddHandler<DeleteCacheItemRequestHandler, DeleteCacheItemRequest>();
                container.AddHandler<ClearCacheRequestHandler, ClearCacheRequest>();

                container.UseCacheFor<BackgroundCache>(cacheType);
                container.UseCacheFor<AddCacheItemRequestHandler>(cacheType);
                container.UseCacheFor<AddCacheItemRangeRequestHandler>(cacheType);
                container.UseCacheFor<DeleteCacheItemRequestHandler>(cacheType);
                container.UseCacheFor<ClearCacheRequestHandler>(cacheType);

                container.RegisterConditional(typeof(ICache),
                    c => typeof(BackgroundCache),
                    Lifestyle.Scoped,
                    c => !c.Handled);

                container.RegisterSingleton<ICacheTempStorage, CacheTempStorage>();
                container.AddBackgroundService(() => new CacheBackgroundService(
                    container.GetInstance<IAsyncQueue>(),
                    container.GetInstance<ICacheTempStorage>()));
            }
            else
            {
                container.Register<ICache, TCache>(Lifestyle.Scoped);
            }
        }

        private static void UseCacheFor<T>(this Container container, Type cacheType)
        {
            container.RegisterConditional(typeof(ICache),
                c => cacheType,
                Lifestyle.Scoped,
                WhenInjectedInto<T>());
        }

        private static Predicate<PredicateContext> WhenInjectedInto<TImplementation>() =>
            c => c.Consumer?.ImplementationType == typeof(TImplementation);
    }
}
