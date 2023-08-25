using System.Linq;
using Jopalesha.Common.Application.BackgroundServices;
using Jopalesha.Common.Application.Mediator;
using Jopalesha.Common.Hosting.Middlewares;
using Jopalesha.Common.Infrastructure.Configuration.Json;
using Jopalesha.Common.Infrastructure.Logging;
using Jopalesha.Helpers.Extensions;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
// ReSharper disable UnusedMember.Global

namespace Jopalesha.Common.Hosting
{
    public abstract class Startup
    {
        private readonly IStartupOptions _options;
        private readonly ILogger _logger;

        protected Startup() : this(StartupOptions.Default)
        {
        }

        protected Startup(IStartupOptions options)
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
            services.AddMvc();
            services.AddRazorPages();
            services.AddControllersWithViews();

            Container.Options.DefaultScopedLifestyle = DefaultScopedLifestyle;
            Container.Options.AllowOverridingRegistrations = AllowOverridingRegistrations;

            services.AddSimpleInjector(Container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation()
                    .AddViewComponentActivation()
                    .AddPageModelActivation()
                    .AddTagHelperActivation();

                options.AutoCrossWireFrameworkComponents = true;

                InitializeContainer(Container);

                var types = options.Container.GetCurrentRegistrations()
                    .Where(it => it.ServiceType.Implements<IBackgroundService>());

                if (types.Any())
                {
                    options.AddHostedService<CompositeBackgroundService>();
                }
            });

            services.AddHttpContextAccessor();
            services.UseSimpleInjectorAspNetRequestScoping(Container);

            ApplyOptions(services);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ApplyOptions(app);

            app.UseMiddleware<ExceptionLoggingMiddleware>(Container);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(it =>
            {
                it.MapRazorPages();
                it.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            Container.Verify();

            _logger.Info("Application started");
        }

        private void InitializeContainer(Container container)
        {
            container.RegisterSingleton(LoggerFactory.Create);
            container.UseJsonConfiguration();
            container.AddMediator();

            SetUpContainer(container);
        }

        private void ApplyOptions(IServiceCollection services)
        {
            if (_options.IsSwaggerEnabled)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
        }
    }
}
