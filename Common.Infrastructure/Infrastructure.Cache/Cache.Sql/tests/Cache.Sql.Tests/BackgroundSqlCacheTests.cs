using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Infrastructure.Cache.Sql.Handlers;
using MediatR;
using Xunit;

namespace Jopalesha.Common.Infrastructure.Cache.Sql.Tests
{
    public class BackgroundSqlCacheTests
    {
        private readonly ICache _sut;

        public BackgroundSqlCacheTests()
        {
            var context = new CacheContext(ContextOptions.InMemory<CacheContext>());
            var sqlCache = new SqlCache(context);
            var asyncQueue = new TestAsyncQueue(sqlCache);

            _sut = new BackgroundSqlCache(asyncQueue, sqlCache);
        }

        [Fact]
        public async Task Crud_IsCorrect()
        {
            const string key = "key";
            const string value = "value";

            await _sut.Add(key, value);

            Assert.Equal(value, await _sut.Get<string>(key));

            await _sut.Delete(key);

            Assert.Null(await _sut.Find<string>(key));
        }

        private class TestAsyncQueue : IAsyncQueue
        {
            private readonly ICache _cache;

            public TestAsyncQueue(ICache cache)
            {
                _cache = cache;
            }

            private readonly ConcurrentQueue<IAsyncQueueRequest> _requests = new ConcurrentQueue<IAsyncQueueRequest>();

            public async void AddJob(IAsyncQueueRequest job)
            {
                _requests.Enqueue(job);

                switch (job)
                {
                    case AddSqlCacheItemRequest addRequest:
                        await ((IRequestHandler<AddSqlCacheItemRequest>) new AddSqlCacheItemRequestHandler(_cache))
                            .Handle(addRequest, CancellationToken.None);
                        break;
                    case DeleteSqlCacheItemRequest deleteRequest:
                        await ((IRequestHandler<DeleteSqlCacheItemRequest>) new DeleteSqlCacheItemRequestHandler(_cache))
                            .Handle(deleteRequest, CancellationToken.None);
                        break;
                    case ClearSqlCacheRequest clearRequest:
                        await ((IRequestHandler<ClearSqlCacheRequest>)new ClearSqlCacheRequestHandler(_cache))
                            .Handle(clearRequest, CancellationToken.None);
                        break;
                }
            }

            public IAsyncQueueRequest DequeueJob()
            {
                _requests.TryDequeue(out var request);
                return request;
            }
        }
    }
}
