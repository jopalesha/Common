using Jopalesha.Common.Infrastructure.Logging;

namespace Common.Hosting.Test.Nuget.Components
{
    internal class ConsoleLoggerFactory : ILoggerFactory
    {
        public ILogger Create()
        {
            return new ConsoleLogger();
        }
    }
}