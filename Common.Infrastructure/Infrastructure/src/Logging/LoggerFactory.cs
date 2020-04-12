using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Infrastructure.Logging
{
    public static class LoggerFactory
    {
        private static ILoggerFactory _logFactory = new DefaultLoggerFactory();

        public static void SetCurrent(ILoggerFactory logFactory)
        {
            _logFactory = Check.NotNull(logFactory);
        }

        public static ILogger Create() => _logFactory.Create();
    }
}
