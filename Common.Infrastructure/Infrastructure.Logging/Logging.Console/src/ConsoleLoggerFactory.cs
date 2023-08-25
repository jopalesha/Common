namespace Jopalesha.Common.Infrastructure.Logging.Console
{
    public class ConsoleLoggerFactory : ILoggerFactory
    {
        public ILogger Create()
        {
            return new ConsoleLogger();
        }
    }
}