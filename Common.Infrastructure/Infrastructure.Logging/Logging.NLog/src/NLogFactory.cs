namespace Jopalesha.Common.Infrastructure.Logging.NLog
{
    public class NLogFactory : ILoggerFactory
    {
        public ILogger Create() => new NLogLogger();
    }
}
