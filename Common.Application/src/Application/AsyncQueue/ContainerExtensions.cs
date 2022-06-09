using Jopalesha.CheckWhenDoIt;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Infrastructure.Logging;
using MediatR;
using SimpleInjector;

namespace Jopalesha.Common.Application.AsyncQueue
{
    public static class ContainerExtensions
    {
        public static void AddAsyncQueue(this Container container) => container.AddAsyncQueue(AsyncQueueOptions.Default);

        public static void AddAsyncQueue(this Container container, AsyncQueueOptions options)
        {
            Check.NotNull(options);
            container.Register<IAsyncQueue, AsyncQueue>(Lifestyle.Singleton);

            for (var i = 0; i < options.ConsumersCount; i++)
            {
                container.AddBackgroundService(() =>
                    new AsyncQueueConsumer(
                        container.GetInstance<IMediator>(),
                        container.GetInstance<IAsyncQueue>(),
                        container.GetInstance<ILogger>(),
                        new BackgroundServiceOptions(options.Interval)));
            }
        }
    }
}
