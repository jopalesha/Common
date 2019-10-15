using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;

namespace Jopalesha.Common.Infrastructure.Cache.Sql.Handlers
{
    internal class ClearSqlCacheRequest : IAsyncQueueRequest
    {
        public string Name => nameof(ClearSqlCacheRequest);
    }

    internal class ClearSqlCacheRequestHandler : AsyncQueueRequestHandler<ClearSqlCacheRequest>
    {
        private readonly ICache _cache;

        public ClearSqlCacheRequestHandler(ICache cache)
        {
            _cache = cache;
        }

        protected override async Task Handle(ClearSqlCacheRequest request, CancellationToken token)
        {
            await _cache.Clear(token);
        }
    }
}
