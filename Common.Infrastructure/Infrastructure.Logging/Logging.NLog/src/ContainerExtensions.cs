using JetBrains.Annotations;
using NLog;
using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Logging.NLog
{
    /// <summary>
    /// DI extension.
    /// </summary>
    [UsedImplicitly]
    public static class ContainerExtensions
    {
        /// <summary>
        /// Register logger method.
        /// </summary>
        /// <param name="container">Container.</param>
        /// <param name="logger">Logger.</param>
        [UsedImplicitly]
        public static void AddNLogLogging(this Container container, Logger logger)
        {
            container.Register<ILogger>(() => new NLogLogger(logger), Lifestyle.Singleton);
        }
    }
}
