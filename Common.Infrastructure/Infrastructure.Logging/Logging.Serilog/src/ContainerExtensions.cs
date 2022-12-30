using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Logging.Serilog
{
    /// <summary>
    /// Container extensions.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Add <see cref="global::Serilog.ILogger"/> logger.
        /// </summary>
        /// <param name="container">Container.</param>
        /// <param name="logger">Logger.</param>
        public static void AddSerilog(this Container container, global::Serilog.ILogger logger)
        {
            container.RegisterSingleton<ILogger>(() => new Serilog(logger));
        }
    }
}
