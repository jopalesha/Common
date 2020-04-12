using System.Linq;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Helpers.Extensions;
using Microsoft.Extensions.Hosting;
using SimpleInjector;

namespace Jopalesha.Common.Hosting
{
    internal static class ContainerExtensions
    {
        internal static void InitializeBackgroundServices(this Container container)
        {
            var types = container.GetCurrentRegistrations()
                .Where(it => it.ServiceType.Implements<IBackgroundService>());

            foreach (var type in types)
            {
                container.Collection.Append<IHostedService>(
                    () => new HostedBackgroundService(type.GetInstance() as IBackgroundService),
                    Lifestyle.Singleton);
            }
        }
    }
}
