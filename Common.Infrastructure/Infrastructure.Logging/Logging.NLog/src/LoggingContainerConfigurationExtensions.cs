using JetBrains.Annotations;
using SimpleInjector;

namespace Jopalesha.Common.Infrastructure.Logging.NLog
{
    [UsedImplicitly]
    public static class LoggingContainerConfigurationExtensions
    {
        public static void AddNLogLogging(this Container container)
        {
            LoggerFactory.SetCurrent(new NLogFactory());
            container.Register(LoggerFactory.Create);
        }
    }
}
