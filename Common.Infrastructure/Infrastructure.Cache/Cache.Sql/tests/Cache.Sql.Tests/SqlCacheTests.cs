using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Jopalesha.Common.Infrastructure.Cache.Sql.Tests
{
    public class SqlCacheTests
    {
        private const string Key = "key";
        private readonly ICache _cache;

        public SqlCacheTests()
        {
            var context = new CacheContext(ContextOptions.InMemory<CacheContext>());
            _cache = new SqlCache(context);
        }

        [Fact]
        public async Task Add_AddsItemToCache()
        {
            var expected = CreateItem(5);

            await _cache.Add(Key, expected);

            Assert.Equal(expected, await _cache.Get<TestClass>(Key));
        }

        [Fact]
        public async Task AddRange_AddsSeveralItemsToCache()
        {
            var expected = new Dictionary<string, object>
            {
                {"1", CreateItem(1)},
                {"2", CreateItem(2)}
            };

            await _cache.AddRange(expected);

            foreach (var (key, value) in expected)
            {
                Assert.Equal(value, await _cache.Get<TestClass>(key));
            }
        }

        [Fact]
        public async Task Delete_RemovesItemFromCache()
        {
            await Add_AddsItemToCache();

            await _cache.Delete(Key);

            Assert.Null(await _cache.Find<TestClass>(Key));
        }

        [Fact]
        public async Task Clear_DeleteAllItemsFromCache()
        {
            await Add_AddsItemToCache();

            await _cache.Clear();
         
            Assert.Null(await _cache.Find<TestClass>(Key));
        }

        [Fact]
        public async Task Get_AlreadyExistKey_ThrowsCacheItemAlreadyExistsException()
        {
            var item = CreateItem(5);
            await _cache.Add(Key, item);

            await Assert.ThrowsAsync<CacheItemAlreadyExistsException>(async () => await _cache.Add(Key, item));
        }

        [Fact]
        public async Task Get_NotExistedItem_ThrowsCacheItemNotFoundException()
        {
            await Assert.ThrowsAsync<CacheItemNotFoundException>(async () => await _cache.Get<TestClass>(Key));
        }


        private static TestClass CreateItem(int value)
        {
            return new TestClass(value);
        }

        private class TestClass : IEquatable<TestClass>
        {
            public TestClass(int value)
            {
                Value = value;
            }

            // ReSharper disable once MemberCanBePrivate.Local
            public int Value { get; }

            public bool Equals(TestClass other)
            {
                if (other is null) return false;
                if (ReferenceEquals(this, other)) return true;
                return Value == other.Value;
            }

            public override bool Equals(object obj)
            {
                if (obj is null) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((TestClass) obj);
            }

            public override int GetHashCode() => Value.GetHashCode();
        }
    }

    public static class ContextOptions
    {
        public static DbContextOptions<T> InMemory<T>() where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }
    }
}
