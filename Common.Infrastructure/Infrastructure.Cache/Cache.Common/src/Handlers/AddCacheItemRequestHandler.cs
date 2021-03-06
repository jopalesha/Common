﻿using System.Threading;
using System.Threading.Tasks;
using Jopalesha.CheckWhenDoIt;
using Jopalesha.Common.Application.AsyncQueue;

namespace Jopalesha.Common.Infrastructure.Cache.Common.Handlers
{
    internal class AddCacheItemRequest : IAsyncQueueRequest
    {
        public AddCacheItemRequest(string key, object value)
        {
            Key = Check.NotEmpty(key);
            Value = value;
        }

        public string Name => nameof(AddCacheItemRequest);

        public string Key { get; }
        
        public object Value { get; }
    }

    internal class AddCacheItemRequestHandler : AsyncQueueRequestHandler<AddCacheItemRequest>
    {
        private readonly ICache _cache;

        public AddCacheItemRequestHandler(ICache cache)
        {
            _cache = cache;
        }

        protected override async Task Handle(AddCacheItemRequest itemRequest, CancellationToken token)
        {
            await _cache.Add(itemRequest.Key, itemRequest.Value, token);
        }
    }
}
