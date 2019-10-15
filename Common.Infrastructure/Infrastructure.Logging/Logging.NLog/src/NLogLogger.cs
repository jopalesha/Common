using System;
using NLog;

namespace Jopalesha.Common.Infrastructure.Logging.NLog
{
    public class NLogLogger : ILogger
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Warning(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error(Exception exception)
        {
            _logger.Error(exception);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Fatal(string message, Exception exception)
        {
            _logger.Fatal(exception, message);
        }
    }
}
