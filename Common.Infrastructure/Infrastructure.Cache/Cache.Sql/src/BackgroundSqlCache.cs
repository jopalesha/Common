using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Infrastructure.Cache.Sql.Handlers;

namespace Jopalesha.Common.Infrastructure.Cache.Sql
{
    public class BackgroundSqlCache : ICache
    {
        private readonly IAsyncQueue _asyncQueue;
        private readonly ICache _cache;

        public BackgroundSqlCache(IAsyncQueue asyncQueue, ICache cache)
        {
            _asyncQueue = asyncQueue;
            _cache = cache;
        }

        public Task Add<T>(string key, T value, CancellationToken token)
        {
            _asyncQueue.AddJob(new AddSqlCacheItemRequest(key, value));
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
            _asyncQueue.AddJob(new DeleteSqlCacheItemRequest(key));
            return Task.CompletedTask;
        }

        public Task Clear(CancellationToken token)
        {
            _asyncQueue.AddJob(new ClearSqlCacheRequest());
            return Task.CompletedTask;
        }
    }
}
