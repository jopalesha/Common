using System;

namespace Jopalesha.Common.Infrastructure.Logging
{
    /// <summary>
    /// Default logger, which do nothing
    /// </summary>
    internal class DefaultLogger : ILogger
    {
        public void Info(string message, params object[] args)
        {
        }

        public void Warning(string message, params object[] args)
        {
        }

        public void Error(Exception exception)
        {
        }

        public void Error(string message, params object[] args)
        {
        }

        public void Error(string message, Exception exception)
        {
        }

        public void Debug(string message, params object[] args)
        {
        }

        public void Fatal(string message, Exception exception)
        {
        }
    }
}
