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
        public async Task Entity_WithGeneratedId_HasExpectedBehaviour() =>
            await VerifyCreate(new TestEntity(Id<int>.Generated, "value"));

        [Fact]
        public async Task Entity_WithSetId_HasExpectedBehaviour() => await VerifyCreate(new TestEntity(22, "value"));


        [Fact]
        public async Task Create_ReturnsExpected()
        {
            var expected = new TestEntity(Id<int>.Generated, "value");
            await _context.TestEntities.AddAsync(expected);
            await _context.SaveChangesAsync();

            var actual = await _context.TestEntities.SingleAsync(it => it.Id == expected.Id);

            Assert.Equal(actual.Id, expected.Id);
        }

        private async Task VerifyCreate(TestEntity entity)
        {
            await _context.TestEntities.AddAsync(entity);
            await _context.SaveChangesAsync();

            Assert.NotNull(await _context.TestEntities.FindAsync(entity.Id));
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

                builder.HasKey(it => it.Id);
                builder.Property(c => c.Id).IsRequired()
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .ValueGeneratedOnAdd();
            }
        }

        private class TestEntity : Entity<int>
        {
            public TestEntity(Id<int> id, string value) : base(id)
            {
                Value = value;
            }

            private TestEntity() : base(Id<int>.Generated)
            {
            }

            public string Value { get; }
        }
    }
}