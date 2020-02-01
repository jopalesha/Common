using Common.Hosting.Test.Nuget.Components;
using Jopalesha.Common.Hosting;
using SimpleInjector;

namespace Common.Hosting.Test.Nuget
{
    public class SampleStartup : Startup
    {
        public override void SetUpContainer(Container container)
        {
            container.Register<IValueStorage, ValueStorage>(Lifestyle.Singleton);
            container.Register<IControllerService, ControllerService>();
        }
    }
}
