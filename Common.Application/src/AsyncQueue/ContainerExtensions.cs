using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Infrastructure.Logging;
using MediatR;
using SimpleInjector;

namespace Jopalesha.Common.Application.AsyncQueue
{
    public static class ContainerExtensions
    {
        public static void AddAsyncQueue(this Container container)
        {
            container.AddAsyncQueue(new AsyncQueueOptions(1));
        }

        public static void AddAsyncQueue(this Container container, AsyncQueueOptions options)
        {
            container.Register<IAsyncQueue, AsyncQueue>(Lifestyle.Singleton);

            for (var i = 0; i < options.ConsumersCount; i++)
            {
                container.AddBackgroundService(() =>
                    new AsyncQueueConsumer(
                        container.GetInstance<IMediator>(),
                        container.GetInstance<IAsyncQueue>(),
                        container.GetInstance<ILogger>()));
            }
        }
    }
}
