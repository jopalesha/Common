using System;
using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Infrastructure.Logging;
using MediatR;

namespace Jopalesha.Common.Application.AsyncQueue
{
    public class AsyncQueueConsumer : RepeatableBackgroundService
    {
        private readonly IMediator _mediator;
        private readonly IAsyncQueue _asyncQueue;
        private readonly ILogger _logger;

        public AsyncQueueConsumer(
            IMediator mediator,
            IAsyncQueue asyncQueue,
            ILogger logger,
            BackgroundServiceOptions options) : base(options)
        {
            _mediator = mediator;
            _asyncQueue = asyncQueue;
            _logger = logger;
        }

        public override string ServiceName => nameof(AsyncQueueConsumer);

        protected override async Task Execute(CancellationToken token)
        {
            IAsyncQueueRequest request = null;

            try
            {
                request = _asyncQueue.DequeueJob();

                if (request != null)
                {
                    await _mediator.Send(request, token);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{nameof(AsyncQueueConsumer)} - Error occurred executing {request?.Name}.", ex);
            }
        }
    }
}
