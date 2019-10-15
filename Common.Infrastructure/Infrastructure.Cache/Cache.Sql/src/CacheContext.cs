using Microsoft.EntityFrameworkCore;

namespace Jopalesha.Common.Infrastructure.Cache.Sql
{
    public class CacheContext : DbContext
    {
        public CacheContext(DbContextOptions context) : base(context)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CacheItem>().ToTable("Cache");
        }
    }
}
