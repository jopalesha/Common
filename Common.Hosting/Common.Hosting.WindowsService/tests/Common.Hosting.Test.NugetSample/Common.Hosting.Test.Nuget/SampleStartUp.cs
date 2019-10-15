using Common.Hosting.Test.Nuget.Components;
using Jopalesha.Common.Hosting;
using SimpleInjector;

namespace Common.Hosting.Test.Nuget
{
    public class SampleStartup : Startup
    {
        public SampleStartup(StartupOptions options) : base(options)
        {
        }

        public override void SetUpContainer(Container container)
        {
            container.Register<IValueStorage, ValueStorage>(Lifestyle.Singleton);
            container.Register<IControllerService, ControllerService>();
        }
    }
}
