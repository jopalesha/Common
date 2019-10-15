namespace Jopalesha.Common.Infrastructure.Logging
{
    public class LoggerFactory
    {
        private static ILoggerFactory _logFactory = new DefaultLoggerFactory();

        public static void SetCurrent(ILoggerFactory logFactory)
        {
            _logFactory = Check.NotNull(logFactory);
        }

        public static ILogger Create()
        {
            Check.NotNull(_logFactory, message:"Logger factory doesn't initialized");
            return _logFactory.Create();
        }
    }
}
