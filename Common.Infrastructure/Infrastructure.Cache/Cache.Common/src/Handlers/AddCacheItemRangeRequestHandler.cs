using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Infrastructure.Helpers;

namespace Jopalesha.Common.Infrastructure.Cache.Common.Handlers
{
    internal class AddCacheItemRangeRequest : IAsyncQueueRequest
    {
        public AddCacheItemRangeRequest(IDictionary<string, object> items)
        {
            Check.NotNullOrEmpty(items);
            Items = items;
        }

        public string Name => nameof(AddCacheItemRangeRequest);

        public IDictionary<string, object> Items { get; }
    }

    internal class AddCacheItemRangeRequestHandler: AsyncQueueRequestHandler<AddCacheItemRangeRequest>
    {
        private readonly ICache _cache;

        public AddCacheItemRangeRequestHandler(ICache cache)
        {
            _cache = cache;
        }

        protected override async Task Handle(AddCacheItemRangeRequest request, CancellationToken token)
        {
            await _cache.AddRange(request.Items, token);
        }
    }
}
