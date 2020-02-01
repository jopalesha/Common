using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Infrastructure.Cache.Common.Handlers;
using MediatR;
using Xunit;

namespace Jopalesha.Common.Infrastructure.Cache.Common.Tests
{
    public class BackgroundCacheTests
    {
        private readonly ICache _sut;
        private readonly IRepeatableBackgroundService _service;

        public BackgroundCacheTests()
        {
            var cache = new MemoryCache();
            var tempStorage = new CacheTempStorage();
            var asyncQueue = new TestAsyncQueue(cache);

            _sut = new BackgroundCache(asyncQueue, cache, tempStorage);
            _service = new CacheBackgroundService(asyncQueue, tempStorage, new BackgroundServiceOptions(TimeSpan.FromSeconds(1)));
        }

        [Fact]
        public async Task Crud_IsCorrect()
        {
            await Task.Factory.StartNew(() => _service.ExecuteAsync(CancellationToken.None));

            const string key = "key";
            const string value = "value";

            await _sut.Add(key, value);
            await Task.Delay(_service.Interval * 2);
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
                    case AddCacheItemRequest addRequest:
                        await ((IRequestHandler<AddCacheItemRequest>) new AddCacheItemRequestHandler(_cache))
                            .Handle(addRequest, CancellationToken.None);
                        break;
                    case AddCacheItemRangeRequest addRangeRequest:
                        await ((IRequestHandler<AddCacheItemRangeRequest>) new AddCacheItemRangeRequestHandler(_cache))
                            .Handle(addRangeRequest, CancellationToken.None);
                        break;
                    case DeleteCacheItemRequest deleteRequest:
                        await ((IRequestHandler<DeleteCacheItemRequest>) new DeleteCacheItemRequestHandler(_cache))
                            .Handle(deleteRequest, CancellationToken.None);
                        break;
                    case ClearCacheRequest clearRequest:
                        await ((IRequestHandler<ClearCacheRequest>)new ClearCacheRequestHandler(_cache))
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


        private class MemoryCache : ICache
        {
            private readonly ConcurrentDictionary<string, object> _storage = new ConcurrentDictionary<string, object>();

            public Task Add<T>(string key, T value, CancellationToken token)
            {
                _storage.TryAdd(key, value);
                return Task.CompletedTask;
            }

            public Task AddRange(IDictionary<string, object> items, CancellationToken token)
            {
                foreach (var (key, value) in items)
                {
                    Add(key, value, token);
                }

                return Task.CompletedTask;
            }

            public Task<T> Get<T>(string key, CancellationToken token)
            {
                _storage.TryGetValue(key, out var result);
                return Task.FromResult((T) result);
            }

            public Task<T> Find<T>(string key, CancellationToken token)
            {
                return Task.FromResult(_storage.TryGetValue(key, out var result) ? (T) result : default);
            }

            public Task Delete(string key, CancellationToken token)
            {
                _storage.TryRemove(key, out _);
                return Task.CompletedTask;
            }

            public Task Clear(CancellationToken token)
            {
                _storage.Clear();
                return Task.CompletedTask;
            }
        }
    }
}
