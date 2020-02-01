using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Infrastructure.Helpers;

namespace Jopalesha.Common.Infrastructure.Cache.Common.Handlers
{
    internal class DeleteCacheItemRequest : IAsyncQueueRequest
    {
        public DeleteCacheItemRequest(string key)
        {
            Key = Check.NotNullOrEmpty(key);
        }

        public string Name => nameof(DeleteCacheItemRequest);

        public string Key { get; }
    }

    internal class DeleteCacheItemRequestHandler : AsyncQueueRequestHandler<DeleteCacheItemRequest>
    {
        private readonly ICache _cache;

        public DeleteCacheItemRequestHandler(ICache cache)
        {
            _cache = cache;
        }

        protected override async Task Handle(DeleteCacheItemRequest request, CancellationToken token)
        {
            await _cache.Delete(request.Key, token);
        }
    }
}
