using Jopalesha.Common.Infrastructure.Cache.Common;
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
            container.UseCache<SqlCache>(options);
        }
    }
}
