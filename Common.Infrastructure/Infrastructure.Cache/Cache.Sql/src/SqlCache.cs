using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.CheckWhenDoIt;
using Newtonsoft.Json;

namespace Jopalesha.Common.Infrastructure.Cache.Sql
{
    public class SqlCache : ICache
    {
        private readonly CacheContext _context;

        public SqlCache(CacheContext context)
        {
            _context = context;
        }

        public async Task Add<T>(string key, T item, CancellationToken token)
        {
            Check.NotEmpty(key, nameof(key));
            Check.NotNull(item, nameof(item));

            if (await FindItemAsync(key, token) != null)
            {
                throw new CacheItemAlreadyExistsException($"Key {key} already exists");
            }

            await _context.AddAsync(CreateCacheItem(key, item), token);
            await SaveChanges(token);
        }

        public async Task AddRange(IDictionary<string, object> items, CancellationToken token)
        {
            Check.NotEmpty(items, nameof(items));

            await _context.AddRangeAsync(items.Select(it => CreateCacheItem(it.Key, it.Value)), token);
            await SaveChanges(token);
        }

        public async Task<T> Get<T>(string key, CancellationToken token)
        {
            var cacheItem = await GetItemAsync(key, token);
            return JsonConvert.DeserializeObject<T>(cacheItem.Value);
        }

        public async Task<T> Find<T>(string key, CancellationToken token)
        {
            var cacheItem = await FindItemAsync(key, token);
            return cacheItem != null ? JsonConvert.DeserializeObject<T>(cacheItem.Value) : default;
        }

        public async Task Delete(string key, CancellationToken token)
        {
            _context.Remove(await GetItemAsync(key, token));
            await SaveChanges(token);
        }

        public async Task Clear(CancellationToken token)
        {
            _context.Set<CacheItem>().RemoveRange(_context.Set<CacheItem>());
            await SaveChanges(token);
        }

        private async Task<CacheItem> GetItemAsync(string key, CancellationToken token)
        {
            var cacheItem = await FindItemAsync(key, token);

            if (cacheItem == null)
            {
                throw new CacheItemNotFoundException($"Cant find data with key {key}");
            }

            return cacheItem;
        }

        private async Task<CacheItem> FindItemAsync(string key, CancellationToken token)
        {
            return await _context.FindAsync<CacheItem>(new object[] { key }, token);
        }

        private static CacheItem CreateCacheItem<T>(string key, T item)
        {
            return new CacheItem
            {
                Key = key,
                Value = JsonConvert.SerializeObject(item)
            };
        }

        private async Task SaveChanges(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}
