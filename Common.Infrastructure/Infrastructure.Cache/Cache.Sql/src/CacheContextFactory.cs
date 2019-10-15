using System;
using Jopalesha.Common.Infrastructure.Configuration.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Jopalesha.Common.Infrastructure.Cache.Sql
{
    internal class CacheContextFactory : IDesignTimeDbContextFactory<CacheContext>
    {
        private readonly SqlCacheOptions _options;

        public CacheContextFactory() : this(new SqlCacheOptions("CacheConnection"))
        {
        }

        public CacheContextFactory(SqlCacheOptions options)
        {
            _options = Check.NotNull(options);
        }

        public CacheContext CreateDbContext() => CreateDbContext(Array.Empty<string>());

        public CacheContext CreateDbContext(string[] args)
        {
            var configuration = new JsonConfiguration();

            var builder = new DbContextOptionsBuilder<CacheContext>();
            var connectionString = configuration.GetConnection(_options.ConnectionName);
            builder.UseSqlServer(connectionString);

            var context = new CacheContext(builder.Options);
            context.Database.Migrate();
            return context;
        }
    }
}
