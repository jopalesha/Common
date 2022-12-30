using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using JetBrains.Annotations;
using Jopalesha.Common.Domain;
using Jopalesha.Common.Domain.Exceptions;
using Jopalesha.Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

// ReSharper disable UnusedAutoPropertyAccessor.Local
#pragma warning disable CA1063 // Implement IDisposable Correctly

namespace Jopalesha.Common.Data.EntityFramework.Tests
{
    public class RepositoryTests : IDisposable
    {
        private readonly Guid _unknownId = Guid.NewGuid();
        private readonly Guid _existentId;
        private readonly IFixture _fixture;
        private readonly TestContext _context;
        private readonly IRepository<TestEntity, Guid> _sut;
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

            _existentId = _items.First().Id;
        }

        [Fact]
        public void Find_WhenEntityExists_ReturnsEntity()
        {
            var expected = _items.First();

            var actual = _sut.Find(expected.Id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Find_WhenEntityDoesNotExists_ReturnsNull() => Assert.Null(_sut.Find(_unknownId));

        [Fact]
        public async Task FindAsync_ForExistentItem_ReturnsEntity()
        {
            var expected = _items.First();

            var actual = await _sut.FindAsync(expected.Id);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task FindAsync_ForNonexistentEntity_ReturnsNull() =>
            Assert.Null(await _sut.FindAsync(_unknownId));

        [Fact]
        public void Get_ForExistentItem_ReturnsEntity() => Assert.NotNull(_sut.Get(_existentId));

        [Fact]
        public void Get_WhenEntityDoesNotExists_ThrowsEntityNotFoundException() =>
            Assert.Throws<EntityNotFoundException<TestEntity>>(() => _sut.Get(_unknownId));

        [Fact]
        public async Task GetAsync_WhenEntityExists_ReturnsEntity()
        {
            var expected = _items.First();

            var actual = await _sut.GetAsync(expected.Id);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task GetAsync_WhenEntityDoesNotExist_ThrowsEntityNotFoundException() =>
            await Assert.ThrowsAsync<EntityNotFoundException<TestEntity>>(() => _sut.GetAsync(_unknownId));

        [Fact]
        public void Single_ForExistentItem_ReturnsEntity() => Assert.NotNull(_sut.Single(it => it.Id == _existentId));

        [Fact]
        public void Single_ForNonExistentItem_ReturnsEntity_ThrowsEntityNotFoundException() =>
            Assert.NotNull(_sut.Single(it => it.Id == _existentId));


        [Fact]
        public async Task SingleAsync_ReturnsItem() =>
            Assert.NotNull(await _sut.SingleAsync(it => it.Id == _existentId));


        [Fact]
        public async Task SingleAsync_ForNonexistentItem_ThrowsEntityNotFoundException() =>
            await Assert.ThrowsAsync<EntityNotFoundException<TestEntity>>(() => _sut.SingleAsync(it => it.Id == _unknownId));

        [Fact]
        public void SingleOrDefault_ForExistentItem_ReturnsEntity() =>
            Assert.NotNull(_sut.SingleOrDefault(it => it.Id == _existentId));

        [Fact]
        public void SingleOrDefault_ForNonexistentItem_ReturnsNull() =>
            Assert.Null(_sut.SingleOrDefault(it => it.Id == _unknownId));

        [Fact]
        public async Task SingleOrDefaultAsync_ForExistentItem_ReturnsEntity() =>
            Assert.NotNull(await _sut.SingleOrDefaultAsync(it => it.Id == _existentId));

        [Fact]
        public async Task SingleOrDefaultAsync_ForNonexistentItem_ReturnsNull() =>
            Assert.Null(await _sut.SingleOrDefaultAsync(it => it.Id == _unknownId));

        [Fact]
        public void Add_AddsItemToRepository()
        {
            var newItem = _fixture.Create<TestEntity>();

            _sut.Add(newItem);
            _context.SaveChanges();

            Assert.NotNull(_context.Find<TestEntity>(newItem.Id));
        }

        [Fact]
        public async Task AddAsync_AddsItemToRepository()
        {
            var newItem = _fixture.Create<TestEntity>();
            await _sut.AddAsync(newItem);
            await _context.SaveChangesAsync();

            Assert.NotNull(_context.Find<TestEntity>(newItem.Id));
        }

        [Fact]
        public async Task ExistsAsync_WhenItemsExists_ReturnsTrue()
        {
            var item = _items.First();

            Assert.True(await _sut.ExistsAsync(item.Id));
        }

        [Fact]
        public void Exists_WhenItemsExists_ReturnsTrue()
        {
            var item = _items.First();

            Assert.True(_sut.Exists(item.Id));
        }

        [Fact]
        public async Task ExistsAsync_WhenItemNotExists_ReturnsFalse() =>
            Assert.False(await _sut.ExistsAsync(_unknownId));

        [Fact]
        public async Task AnyAsync_WhenItemExists_ReturnsTrue()
        {
            var item = _items.First();

            Assert.True(await _sut.AnyAsync(it => it.Value == item.Value));
        }

        [Fact]
        public async Task AnyAsync_WhenItemNotExists_ReturnsFalse()
        {
            Assert.False(await _sut.AnyAsync(it => it.Value == "notFound"));
        }

        [Fact]
        public void Any_WhenItemExists_ReturnsTrue()
        {
            var item = _items.First();

            Assert.True(_sut.Any(it => it.Value == item.Value));
        }

        [Fact]
        public void Any_WhenItemNotExists_ReturnsFalse() => Assert.False(_sut.Any(it => it.Value == "notFound"));


        [Fact]
        public void Exists_WhenItemNotExists_ReturnsFalse() => Assert.False(_sut.Exists(_unknownId));

        [Fact]
        public async Task GetAll_WithQuery_ReturnsEntities()
        {
            var expected = _items.First();
            var list = await _sut.GetAll(q =>
                q.Where(it => it.Value.Contains(expected.Value) && it.Id == expected.Id).Take(1));

            Assert.Equal(1, list.TotalCount);
            var actual = Assert.Single(list.Items);
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
        }

        [Fact]
        public async Task GetAll_WithEmptyResult_ReturnsEmptyObject()
        {
            var actual = await _sut.GetAll(q => q.Where(it => it.Value == "not_existed"));

            Assert.Empty(actual.Items);
            Assert.Equal(0, actual.TotalCount);
        }

        [Fact]
        public void Remove_DeletesEntityFromRepository()
        {
            var itemForRemove = _items.First();

            _sut.Remove(itemForRemove);
            _context.SaveChanges();

            Assert.Null(_sut.Find(_existentId));
        }

        [Fact]
        public void RemoveBydId_DeletesEntityFromRepository()
        {
            _sut.Remove(_existentId);
            _context.SaveChanges();

            Assert.Null(_sut.Find(_existentId));
        }

        [Fact]
        public void RemoveById_WhenEntityDoesNotExist_ThrowsEntityNotFoundException() =>
            Assert.Throws<EntityNotFoundException<TestEntity>>(() => _sut.Remove(_unknownId));

        [Fact]
        public async Task RemoveByIdAsync_DeletesEntityFromRepository()
        {
            await _sut.RemoveAsync(_existentId);
            await _context.SaveChangesAsync();

            Assert.Null(await _sut.FindAsync(_existentId));
        }

        [Fact]
        public async Task RemoveByIdAsync_WhenEntityDoesNotExist_ThrowsEntityNotFoundException() =>
            await Assert.ThrowsAsync<EntityNotFoundException<TestEntity>>(() => _sut.RemoveAsync(_unknownId));

        [Fact]
        public void AddRange_AddsListOfItems()
        {
            var items = _fixture.CreateMany<TestEntity>().ToList();

            _sut.AddRange(items);
            _context.SaveChanges();

            Assert.Equal(items.Count, _context.TestEntities.Count(it => items.Select(i => i.Id).Contains(it.Id)));
        }

        [Fact]
        public async Task AddRangeAsync_AddsListOfItems()
        {
            var items = _fixture.CreateMany<TestEntity>().ToList();

            await _sut.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            Assert.Equal(items.Count, _context.TestEntities.Count(it => items.Select(i => i.Id).Contains(it.Id)));
        }

        [Fact]
        public void GetAll_ReturnsAllEntities()
        {
            var actual = _sut.GetAll();

            Assert.Equal(_items.Count, actual.Count());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            var actual = await _sut.GetAllAsync();

            Assert.Equal(_items.Count, actual.Count());
        }

        [Fact]
        public void Count_ReturnsCountOfItems() => Assert.Equal(_items.Count, _sut.Count());

        [Fact]
        public void CountByPredicate_ReturnsCountOfItems() =>
            Assert.Equal(_items.Count, _sut.Count(it => _items.Contains(it)));

        [Fact]
        public async Task CountAsync_ReturnsCountOfItems() => Assert.Equal(_items.Count, await _sut.CountAsync());

        [Fact]
        public async Task CountAsyncByPredicate_ReturnsCountOfItems() =>
            Assert.Equal(_items.Count, await _sut.CountAsync(it => _items.Contains(it)));


        private class TestContext : DbContext
        {
            public TestContext(DbContextOptions context) : base(context)
            {
            }

            public DbSet<TestEntity> TestEntities { get; set; }
        }

        [UsedImplicitly]
        private class TestEntity : IEntity<Guid>
        {
            [Key]
            public Guid Id { get; set; }

            public string Value { get; set; }

            public bool Equals(IEntity<Guid> other)
            {
                return other?.Id == Id;
            }
        }

        private class TestRepository : Repository<TestEntity, Guid>
        {
            public TestRepository(DbContext context) : base(context)
            {
            }
        }

        public void Dispose() => _context?.Dispose();
    }
}
