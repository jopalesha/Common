using System;
using System.Threading.Tasks;
using Jopalesha.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xunit;

namespace Common.Domain.EF.IntegrationTests
{
    public class EntityTests
    {
        private readonly TestContext _context;

        public EntityTests()
        {
            _context = new TestContext(new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        }

        [Fact]
        public async Task Create_ReturnsExpected()
        {
            var entity = new TestEntity("value");
            _context.TestEntities.Add(entity);
            await _context.SaveChangesAsync();

            var id = (await _context.TestEntities.SingleAsync()).Id;

            Assert.True(id != default);
            Assert.NotNull(await _context.TestEntities.FindAsync(id));
        }

        private class TestContext : DbContext
        {
            public TestContext(DbContextOptions context) : base(context)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.ApplyConfiguration(new TestEntityConfiguration());
            }

            public DbSet<TestEntity> TestEntities { get; set; }
        }

        private class TestEntityConfiguration: IEntityTypeConfiguration<TestEntity>
        {
            public void Configure(EntityTypeBuilder<TestEntity> builder)
            {
                builder.Property(it => it.Value).IsRequired();

                builder.Property(c => c.Id).IsRequired()
                    .UsePropertyAccessMode(PropertyAccessMode.Property)
                    .ValueGeneratedOnAdd();

                builder.HasKey(it => it.Id);
            }
        }

        private class TestEntity : Entity<int>
        {
            public TestEntity(string value) :base(Key<int>.Generated)
            {
                Value = value;
            }

            public string Value { get; }
        }
    }
}