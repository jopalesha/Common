using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Mapper.AutoMapper
{
    public static class ContainerExtensions
    {
        public static void RegisterAutoMapper(this Container container)
        {
            container.Register<AutoMapperFactory>();
            container.Register<IMapper>(() => new AutoMapper(container.GetInstance<AutoMapperFactory>().Create()),
                Lifestyle.Singleton);
        }
    }
}
