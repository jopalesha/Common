using System;
using Jopalesha.Common.Infrastructure.Configuration;
using Jopalesha.Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;

namespace Jopalesha.Common.Data.EntityFramework
{
    public static class ContainerExtensions
    {
        public static void AddDbContext<T>(this Container container, string connectionName) where T : DbContext
        {
            void OptionsFactory(DbContextOptionsBuilder builder)
            {
                var configuration = container.GetInstance<IConfiguration>();
                var connection = configuration.GetConnection(connectionName);

                builder.UseSqlServer(connection);
            }

            AddDbContext<T>(container, OptionsFactory);
        }

        public static void AddDbContext<T>(this Container container, Action<DbContextOptionsBuilder> setOptions) where T : DbContext
        {
            DbContextOptions<T> OptionsFactory()
            {
                var builder = new DbContextOptionsBuilder<T>();
                setOptions(builder);
                return builder.Options;
            }

            var optionsRegistration = Lifestyle.Singleton.CreateRegistration(OptionsFactory, container);
            container.AddRegistration<DbContextOptions>(optionsRegistration);
            container.AddRegistration<DbContextOptions<T>>(optionsRegistration);

            var contextRegistration = Lifestyle.Scoped.CreateRegistration<T>(container);
            container.AddRegistration<T>(contextRegistration);
            container.AddRegistration<DbContext>(contextRegistration);

            container.RegisterInstance<Func<T>>(container.GetInstance<T>);
        }

        public static void AddUnitOfWork(this Container container)
        {
            var unitOfWorkRegistration = Lifestyle.Scoped.CreateRegistration<UnitOfWork>(container);

            container.AddRegistration<IUnitWork>(unitOfWorkRegistration);
            container.AddRegistration<ISaveContext>(unitOfWorkRegistration);
        }
    }
}
