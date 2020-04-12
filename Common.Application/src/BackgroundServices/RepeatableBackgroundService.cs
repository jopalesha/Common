using System;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.CheckWhenDoIt;
using Jopalesha.Common.Infrastructure.Logging;

namespace Jopalesha.Common.Application.BackgroundServices
{
    public abstract class RepeatableBackgroundService : IRepeatableBackgroundService
    {
        private readonly ILogger _logger;

        protected RepeatableBackgroundService() : this(BackgroundServiceOptions.Default)
        {
        }

        protected RepeatableBackgroundService(BackgroundServiceOptions options)
        {
            Check.NotNull(options);

            _logger = LoggerFactory.Create();
            Interval = options.Interval;
        }

        public TimeSpan Interval { get; }

        public abstract string ServiceName { get; }

        public async Task ExecuteAsync(CancellationToken token)
        {
            _logger.Info($"{ServiceName} started");

            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Execute(token);
                }
                catch (Exception e)
                {
                    _logger.Error($"Error in {ServiceName}:", e);
                }

                await Task.Delay(Interval, token);
            }

            _logger.Info($"{ServiceName} stopped.");
        }

        protected abstract Task Execute(CancellationToken token);
    }
}
