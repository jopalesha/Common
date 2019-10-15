using Jopalesha.Common.Application.AsyncQueue;
using Jopalesha.Common.Application.Mediator;
using Jopalesha.Common.Hosting.Test.Sample.Components;
using Jopalesha.Common.Hosting.Test.Sample.Handlers;
using SimpleInjector;

namespace Jopalesha.Common.Hosting.Test.Sample
{
    public class SampleStartup : Startup
    {
        public SampleStartup(StartupOptions options) : base(options)
        {
        }

        public override void SetUpContainer(Container container)
        {
            container.AddAsyncQueue(new AsyncQueueOptions(2));

            container.Register<IValueStorage, ValueStorage>(Lifestyle.Singleton);
            container.Register<IHistoryStorage, HistoryStorage>(Lifestyle.Scoped);
            container.AddHandler<PutRequestHandler, PutRequest>();
            container.AddHandler<DeleteRequestHandler, DeleteRequest>();
            container.Register<IControllerService, ControllerService>();
        }
    }
}
