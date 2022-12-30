using Microsoft.EntityFrameworkCore;

namespace Jopalesha.Common.Data.EntityFramework.Integration
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions context) : base(context)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TestEntity>().HasKey(it => it.Id);
            builder.Entity<TestEntity>().Property(c => c.Id)
                .IsRequired()
                .HasField("_id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .ValueGeneratedOnAdd();

            builder.Entity<TestEntity>().Property(it => it.Value).IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
        }
    }
}
