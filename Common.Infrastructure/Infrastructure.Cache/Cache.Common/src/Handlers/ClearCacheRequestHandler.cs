using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;

namespace Jopalesha.Common.Infrastructure.Cache.Common.Handlers
{
    internal class ClearCacheRequest : IAsyncQueueRequest
    {
        public string Name => nameof(ClearCacheRequest);
    }

    internal class ClearCacheRequestHandler : AsyncQueueRequestHandler<ClearCacheRequest>
    {
        private readonly ICache _cache;

        public ClearCacheRequestHandler(ICache cache)
        {
            _cache = cache;
        }

        protected override async Task Handle(ClearCacheRequest request, CancellationToken token)
        {
            await _cache.Clear(token);
        }
    }
}
