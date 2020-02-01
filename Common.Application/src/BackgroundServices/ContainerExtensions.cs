using System;
using SimpleInjector;

namespace Jopalesha.Common.Application.BackgroundServices
{
    public static class ContainerExtensions
    {
        public static void AddBackgroundService<T>(this Container container)
            where T : class, IBackgroundService
        {
            var registration = Lifestyle.Singleton.CreateProducer<T>(typeof(T), container);
            container.Collection.Append<IBackgroundService>(() => registration.GetInstance(), Lifestyle.Singleton);
        }

        public static void AddBackgroundService<T>(this Container container, Func<T> factory)
            where T : class, IBackgroundService
        {
            var registration = Lifestyle.Singleton.CreateProducer(factory, container);
            container.Collection.Append<IBackgroundService>(() => registration.GetInstance(), Lifestyle.Singleton);
        }
    }
}
