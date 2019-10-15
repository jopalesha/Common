using System;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Application.Mediator;
using Jopalesha.Common.Infrastructure.Cache.Sql.Handlers;
using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Cache.Sql
{
    public static class ContainerExtensions
    {
        public static void UseSqlCache(this Container container)
        {
            container.UseSqlCache(new SqlCacheOptions("CacheConnection"));
        }

        public static void UseSqlCache(this Container container, SqlCacheOptions options)
        {
            container.Register(() => new CacheContextFactory(options).CreateDbContext(), Lifestyle.Scoped);

            if (options.IsBackground)
            {
                container.AddHandler<AddSqlCacheItemRequestHandler, AddSqlCacheItemRequest>();
                container.AddHandler<DeleteSqlCacheItemRequestHandler, DeleteSqlCacheItemRequest>();
                container.AddHandler<ClearSqlCacheRequestHandler, ClearSqlCacheRequest>();

                container.UseSqlCacheFor<BackgroundSqlCache>();
                container.UseSqlCacheFor<AddSqlCacheItemRequestHandler>();
                container.UseSqlCacheFor<DeleteSqlCacheItemRequestHandler>();
                container.UseSqlCacheFor<ClearSqlCacheRequestHandler>();

                container.RegisterConditional(typeof(ICache),
                    c => typeof(BackgroundSqlCache),
                    Lifestyle.Scoped,
                    c => !c.Handled);
            }
            else
            {
                container.Register<ICache, SqlCache>(Lifestyle.Scoped);
            }
        }

        private static void UseSqlCacheFor<T>(this Container container)
        {
            container.RegisterConditional(typeof(ICache),
                c => typeof(SqlCache),
                Lifestyle.Scoped,
                WhenInjectedInto<T>());
        }

        private static Predicate<PredicateContext> WhenInjectedInto<TImplementation>() =>
            c => c.Consumer?.ImplementationType == typeof(TImplementation);
    }
}
