using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;

namespace Jopalesha.Common.Infrastructure.Cache.Sql.Handlers
{
    internal class DeleteSqlCacheItemRequest : IAsyncQueueRequest
    {
        public DeleteSqlCacheItemRequest(string key)
        {
            Key = Check.NotNullOrEmpty(key);
        }

        public string Name => nameof(DeleteSqlCacheItemRequest);

        public string Key { get; }
    }

    internal class DeleteSqlCacheItemRequestHandler : AsyncQueueRequestHandler<DeleteSqlCacheItemRequest>
    {
        private readonly ICache _cache;

        public DeleteSqlCacheItemRequestHandler(ICache cache)
        {
            _cache = cache;
        }

        protected override async Task Handle(DeleteSqlCacheItemRequest request, CancellationToken token)
        {
            await _cache.Delete(request.Key, token);
        }
    }
}
