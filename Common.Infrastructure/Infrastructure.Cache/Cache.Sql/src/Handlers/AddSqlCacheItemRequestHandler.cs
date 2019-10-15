using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;

namespace Jopalesha.Common.Infrastructure.Cache.Sql.Handlers
{
    internal class AddSqlCacheItemRequest : IAsyncQueueRequest
    {
        public AddSqlCacheItemRequest(string key, object value)
        {
            Key = Check.NotNullOrEmpty(key);
            Value = value;
        }

        public string Name => nameof(AddSqlCacheItemRequest);

        public string Key { get; set; }
        
        public object Value { get; }
    }

    internal class AddSqlCacheItemRequestHandler : AsyncQueueRequestHandler<AddSqlCacheItemRequest>
    {
        private readonly ICache _cache;

        public AddSqlCacheItemRequestHandler(ICache cache)
        {
            _cache = cache;
        }

        protected override async Task Handle(AddSqlCacheItemRequest itemRequest, CancellationToken token)
        {
            await _cache.Add(itemRequest.Key, itemRequest.Value, token);
        }
    }
}
