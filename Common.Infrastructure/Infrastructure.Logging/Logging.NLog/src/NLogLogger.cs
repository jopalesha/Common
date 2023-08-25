using System;
using System.Globalization;
using Jopalesha.CheckWhenDoIt;
using NLog;

namespace Jopalesha.Common.Infrastructure.Logging.NLog
{
    /// <summary>
    /// NLog Logger.
    /// </summary>
    internal class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogLogger"/> class.
        /// </summary>
        /// <param name="logger">Internal logger.</param>
        public NLogLogger(Logger logger) => _logger = Check.NotNull(logger);

        /// <inheritdoc />
        public void Debug(string message) => _logger.Debug(message);

        /// <inheritdoc />

        public void Debug(string message, params object[] args) => _logger.Debug(CultureInfo.InvariantCulture, message, args);

        /// <inheritdoc />

        public void Info(string message) => _logger.Info(message);

        /// <inheritdoc />
        public void Info(string message, params object[] args) => _logger.Info(CultureInfo.InvariantCulture, message, args);

        /// <inheritdoc />
        public void Warning(string message) => _logger.Warn(message);

        /// <inheritdoc />
        public void Warning(string message, params object[] args) => _logger.Warn(CultureInfo.InvariantCulture, message, args);

        /// <inheritdoc />
        public void Error(string message) => _logger.Error(message);

        /// <inheritdoc />
        public void Error(string message, params object[] args) => _logger.Error(CultureInfo.InvariantCulture, message, args);

        /// <inheritdoc />
        public void Error(Exception exception, string message) => _logger.Error(exception, message);

        /// <inheritdoc />
        public void Error(Exception exception, string message, params object[] args) => _logger.Error(exception, message, args);

        /// <inheritdoc />
        public void Fatal(string message) => _logger.Fatal(message);

        /// <inheritdoc />
        public void Fatal(string message, params object[] args) => _logger.Fatal(CultureInfo.InvariantCulture, message, args);

        /// <inheritdoc />
        public void Fatal(Exception exception, string message) => _logger.Fatal(exception, message);

        /// <inheritdoc />
        public void Fatal(Exception exception, string message, params object[] args) => _logger.Fatal(exception, message, args);
    }
}
