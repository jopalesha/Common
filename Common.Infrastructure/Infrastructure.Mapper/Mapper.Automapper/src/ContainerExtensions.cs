using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Mapper.AutoMapper
{
    /// <summary>
    /// Container extensions.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Add automapper implementation for <see cref="IMapper"/> interface.
        /// </summary>
        /// <param name="container">DI container.</param>
        public static void RegisterAutoMapper(this Container container)
        {
            container.Register<AutoMapperFactory>();
            container.Register<IMapper>(() => new AutoMapper(container.GetInstance<AutoMapperFactory>().Create()),
                Lifestyle.Singleton);
        }
    }
}
