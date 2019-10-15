using Jopalesha.Common.Infrastructure.Logging;

namespace Jopalesha.Common.Hosting.Test.Sample.Components
{
    internal class ConsoleLoggerFactory : ILoggerFactory
    {
        public ILogger Create()
        {
            return new ConsoleLogger();
        }
    }
}