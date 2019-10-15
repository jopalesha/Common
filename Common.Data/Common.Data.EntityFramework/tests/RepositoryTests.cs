using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using JetBrains.Annotations;
using Jopalesha.Common.Domain;
using Jopalesha.Common.Domain.Models;
using Jopalesha.Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Jopalesha.Common.Data.EntityFramework.Tests
{
    public class RepositoryTests : IDisposable
    {
        private readonly IFixture _fixture;
        private readonly TestContext _context;
        private readonly IRepository<TestEntity, int> _sut;
        private readonly List<TestEntity> _items;

        public RepositoryTests()
        {
            _fixture = new Fixture();
            _context = new TestContext(new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

            _sut = new TestRepository(_context);

            _items = _fixture.CreateMany<TestEntity>(3).ToList();

            _context.TestEntities.AddRange(_items);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetById_IsOk()
        {
            var expected = _items.First();

            var actual = await _sut.Get(expected.Id);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task Add_IsOk()
        {
            var newItem = _fixture.Create<TestEntity>();
            _sut.Add(newItem);
            await _context.SaveChangesAsync();

            Assert.NotNull(_sut.Get(newItem.Id));
        }

        [Fact]
        public async Task GetAll_IsOk()
        {
            var expected = _items.First();
            var query = new Query<TestEntity>()
                .Where(it => it.Value.Contains(expected.Value) && it.Id == expected.Id)
                .Take(1);

            var list = await _sut.GetAll(query);

            Assert.Equal(1, list.TotalCount);
            var actual = Assert.Single(list.Items);
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
        }

        private class TestContext : DbContext
        {
            public TestContext(DbContextOptions context) : base(context)
            {
            }

            public DbSet<TestEntity> TestEntities { get; set; }
        }

        [UsedImplicitly]
        private class TestEntity : IHasId<int>
        {
            [Key]
            public int Id { get; set; }

            public string Value { get; set; }
        }

        private class TestRepository : Repository<TestEntity, int>
        {
            public TestRepository(DbContext context) : base(context)
            {
            }
        }

        public void Dispose() => _context?.Dispose();
    }
}
