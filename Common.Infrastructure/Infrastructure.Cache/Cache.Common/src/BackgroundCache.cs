using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Infrastructure.Cache.Common.Handlers;

namespace Jopalesha.Common.Infrastructure.Cache.Common
{
    /// <inheritdoc/>
    public class BackgroundCache : ICache
    {
        private readonly IAsyncQueue _asyncQueue;
        private readonly ICacheTempStorage _tempStorage;
        private readonly ICache _cache;

        public BackgroundCache(
            IAsyncQueue asyncQueue,
            ICache cache,
            ICacheTempStorage tempStorage)
        {
            _asyncQueue = asyncQueue;
            _cache = cache;
            _tempStorage = tempStorage;
        }

        /// <inheritdoc/>
        public Task Add<T>(string key, T value, CancellationToken token)
        {
            _tempStorage.Add(key, value);
            return Task.CompletedTask;
        }

        public Task AddRange(IDictionary<string, object> items, CancellationToken token = default)
        {
            _tempStorage.AddRange(items);
            return Task.CompletedTask;
        }

        public async Task<T> Get<T>(string key, CancellationToken token)
        {
            return await _cache.Get<T>(key, token);
        }

        public async Task<T> Find<T>(string key, CancellationToken token)
        {
            return await _cache.Find<T>(key, token);
        }

        public Task Delete(string key, CancellationToken token)
        {
            _asyncQueue.AddJob(new DeleteCacheItemRequest(key));
            return Task.CompletedTask;
        }

        public Task Clear(CancellationToken token)
        {
            _asyncQueue.AddJob(new ClearCacheRequest());
            return Task.CompletedTask;
        }
    }
}
