using System.Linq;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Application.Mediator;
using Jopalesha.Common.Hosting.Middlewares;
using Jopalesha.Common.Infrastructure.Configuration.Json;
using Jopalesha.Common.Infrastructure.Extensions;
using Jopalesha.Common.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Swagger;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Jopalesha.Common.Hosting
{
    public abstract class Startup
    {
        private readonly StartupOptions _options;
        private readonly ILogger _logger;

        protected Startup(StartupOptions options)
        {
            Container = new Container();
            _logger = LoggerFactory.Create();
            _options = options;
        }

        public virtual ScopedLifestyle DefaultScopedLifestyle => new AsyncScopedLifestyle();

        public virtual bool AllowOverridingRegistrations => false;

        protected Container Container { get; }

        public abstract void SetUpContainer(Container container);

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSimpleInjector(Container, options =>
            {
                options.Container.Options.DefaultScopedLifestyle = DefaultScopedLifestyle;
                options.Container.Options.AllowOverridingRegistrations = AllowOverridingRegistrations;

                options.AddAspNetCore().AddControllerActivation();

                options.Services.AddHttpContextAccessor();
                options.Services.EnableSimpleInjectorCrossWiring(Container);
                options.Services.UseSimpleInjectorAspNetRequestScoping(Container);
            });

            ApplyOptions(services);

            services.AddSingleton(_ =>
            {
                var currentRegistrations = Container.GetCurrentRegistrations();

                if (currentRegistrations.Any(it => it.ServiceType.Implements<IBackgroundService>()))
                {
                    return Container.GetAllInstances<IHostedService>();
                }

                return Enumerable.Empty<IHostedService>();
            });
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSimpleInjector(Container, options =>
            {
                options.UseMiddleware<ExceptionLoggingMiddleware>(app);
                options.AutoCrossWireFrameworkComponents = true;
            });

            Container.RegisterSingleton(LoggerFactory.Create);
            Container.UseJsonConfiguration();
            Container.AddMediator();

            SetUpContainer(Container);
            ApplyOptions(app);
            Container.InitializeBackgroundServices();

            Container.Verify();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            _logger.Info("Application started");
        }

        private void ApplyOptions(IServiceCollection services)
        {
            if (_options.IsSwaggerEnabled)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Version = "v1" });
                });
            }
        }

        private void ApplyOptions(IApplicationBuilder app)
        {
            if (_options.IsSwaggerEnabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "api_v1");
                });
            }
        }
    }
}
