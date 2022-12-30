using System;
using System.Threading.Tasks;
using Jopalesha.Common.Domain;
using Jopalesha.Common.Domain.Exceptions;
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
            var expected = new TestEntity(32323, "value");
            await _context.TestEntities.AddAsync(expected);
            await _context.SaveChangesAsync();

            var actual = await _context.TestEntities.SingleAsync(it => it.Id == expected.Id);

            Assert.Equal(actual.Id, expected.Id);
        }

        [Fact]
        public void GetId_ForNotGeneratedId_ThrowsIdNotGeneratedException() =>
            Assert.Throws<IdNotGeneratedException>(() => new TestEntity("test").Id);

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

            public DbSet<TestEntity> TestEntities { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.ApplyConfiguration(new TestEntityConfiguration());
            }
        }

        private class TestEntityConfiguration: IEntityTypeConfiguration<TestEntity>
        {
            public void Configure(EntityTypeBuilder<TestEntity> builder)
            {
                builder.HasKey(it => it.Id);
                builder.Property(c => c.Id)
                    .IsRequired()
                    .HasField("_id")
                    .UsePropertyAccessMode(PropertyAccessMode.PreferField)
                    .ValueGeneratedOnAdd();

                builder.Property(it => it.Value).IsRequired()
                    .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
            }
        }

        private class TestEntity : Entity<int>
        {
            public TestEntity(string value) : this(Id<int>.Generated, value)
            {
            }

            public TestEntity(Id<int> id, string value) : base(id)
            {
                Value = value;
            }

            public string Value { get; }
        }
    }
}
