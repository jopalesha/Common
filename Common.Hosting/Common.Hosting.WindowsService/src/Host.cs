using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using DasMulli.Win32.ServiceUtils;
using Jopalesha.Common.Hosting.Components;
using Jopalesha.Common.Infrastructure.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Jopalesha.Common.Hosting
{
    public class Host
    {
        public static IWebHostBuilder CreateWebHostBuilder<T>(string[] args, Action<StartUpOptionsBuilder> setupAction)
            where T : Startup
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", true)
                .AddCommandLine(args)
                .Build();

            var optionsBuilder = new StartUpOptionsBuilder();
            setupAction(optionsBuilder);

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .ConfigureServices(services => { services.AddSingleton(it => optionsBuilder.Build()); })
                .UseStartup<T>();
        }

        public static void Run<T>(HostingOptions options, Action<StartUpOptionsBuilder> setupAction) where T : Startup 
        {
            try
            {
                switch (options.Operation)
                {
                    case HostingOperation.Install:
                    {
                        InstallService(options.Name);
                        break;
                    }
                    case HostingOperation.Uninstall:
                    {
                        Uninstall(options.Name);
                        break;
                    }
                    case HostingOperation.Start:
                    {
                        StartService(options.Name);
                        break;
                    }
                    case HostingOperation.Stop:
                    {
                        StopService(options.Name);
                        break;
                    }
                    case HostingOperation.Run:
                    {
                        switch (options.Type)
                        {
                            case HostingType.WindowsService:
                            {
                                RunWindowsService<T>(options.Name, options.Arguments, setupAction);
                                break;
                            }
                            case HostingType.Process:
                            {
                                CreateWebHostBuilder<T>(options.Arguments.ToArray(), setupAction).Build().Run();
                                break;
                            }
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                LoggerFactory.Create().Fatal("Error while starting app", e);
                throw;
            }
        }

        private static void RunWindowsService<T>(
            string serviceName, 
            IEnumerable<string> args,
            Action<StartUpOptionsBuilder> setupAction) where T : Startup
        {
            Directory.SetCurrentDirectory(PlatformServices.Default.Application.ApplicationBasePath);

            var serviceArgs = args.Except(new[]
            {
                HostingArguments.RunAsServiceFlag,
                HostingArguments.ServiceNameFlag,
                serviceName
            }).ToArray();

            var service = new WebHostService(() => CreateWebHostBuilder<T>(serviceArgs, setupAction).Build(), serviceName);
            var serviceHost = new Win32ServiceHost(service);
            serviceHost.Run();
        }

        private static void InstallService(string serviceName)
        {
            // Environment.GetCommandLineArgs() includes the current DLL from a "dotnet my.dll install" call,
            // which is not passed to Main()
            var commandLineArgs = Environment.GetCommandLineArgs();

            var serviceArgs = commandLineArgs
                .Where(arg => arg != HostingArguments.InstallFlag)
                .Select(EscapeCommandLineArgument)
                .Append(HostingArguments.RunAsServiceFlag);

            var host = Process.GetCurrentProcess().MainModule?.FileName;

            if (host != null && !host.EndsWith("dotnet.exe", StringComparison.OrdinalIgnoreCase))
            {
                // For self-contained apps, skip the dll path
                serviceArgs = serviceArgs.Skip(1);
            }

            var fullServiceCommand = host + " " + string.Join(" ", serviceArgs);

            // Note that when the service is already registered and running, it will be reconfigured but not restarted
            var serviceDefinition = new ServiceDefinitionBuilder(serviceName)
                .WithDisplayName(serviceName)
                .WithDescription(serviceName)
                .WithBinaryPath(fullServiceCommand)
                .WithCredentials(Win32ServiceCredentials.LocalSystem)
                .WithAutoStart(true)
                .Build();

            new Win32ServiceManager().CreateService(serviceDefinition, startImmediately: true);

            Console.WriteLine($"Successfully installed service {serviceName}");
        }

        private static string EscapeCommandLineArgument(string arg)
        {
            // http://stackoverflow.com/a/6040946/784387
            arg = Regex.Replace(arg, @"(\\*)" + "\"", @"$1$1\" + "\"");
            arg = "\"" + Regex.Replace(arg, @"(\\+)$", @"$1$1") + "\"";
            return arg;
        }

        private static void Uninstall(string serviceName)
        {
            new Win32ServiceManager().DeleteService(serviceName);
            Console.WriteLine($"Service {serviceName} successfully uninstalled");
        }

        private static void StartService(string serviceName)
        {
            using var controller = new ServiceController(serviceName);
            controller.Start();
            Console.WriteLine($"Started service {serviceName}");
        }

        private static void StopService(string serviceName)
        {
            using var controller = new ServiceController(serviceName);
            controller.Stop();
            Console.WriteLine($"Stopped service {serviceName}");
        }
    }
}
