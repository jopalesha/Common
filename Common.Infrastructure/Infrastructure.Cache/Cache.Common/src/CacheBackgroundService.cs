using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Infrastructure.Cache.Common.Handlers;

namespace Jopalesha.Common.Infrastructure.Cache.Common
{
    public class CacheBackgroundService : RepeatableBackgroundService
    {
        private readonly IAsyncQueue _asyncQueue;
        private readonly ICacheTempStorage _tempStorage;

        public CacheBackgroundService(
            IAsyncQueue asyncQueue,
            ICacheTempStorage tempStorage): this(asyncQueue, tempStorage, BackgroundServiceOptions.Default)
        {
        }

        public CacheBackgroundService(
            IAsyncQueue asyncQueue,
            ICacheTempStorage tempStorage,
            BackgroundServiceOptions options) : base(options)
        {
            _asyncQueue = asyncQueue;
            _tempStorage = tempStorage;
        }

        public override string ServiceName => nameof(CacheBackgroundService);

        protected override Task Execute(CancellationToken token)
        {
            var items = _tempStorage.GetAll().ToDictionary(it => it.Key, it => it.Value);

            if (items.Count > 0)
            {
                _asyncQueue.AddJob(new AddCacheItemRangeRequest(items));
            }

            return Task.CompletedTask;
        }
    }
}
