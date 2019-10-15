namespace Jopalesha.Common.Infrastructure.Logging
{
    internal class DefaultLoggerFactory : ILoggerFactory
    {
        public ILogger Create()
        {
            return new DefaultLogger();
        }
    }
}
