using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Infrastructure.Logging.Serilog
{
    /// <summary>
    /// Serilog logger.
    /// </summary>
    internal sealed class Serilog : ILogger
    {
        private readonly global::Serilog.ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Serilog"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public Serilog(global::Serilog.ILogger logger) => _logger = Check.NotNull(logger);

        /// <inheritdoc/>
        public void Debug(string message) => _logger.Debug(message);

        /// <inheritdoc/>
        public void Debug(string message, params object[] args) => _logger.Debug(message, args);

        /// <inheritdoc/>
        public void Info(string message) => _logger.Information(message);

        /// <inheritdoc/>
        public void Info(string message, params object[] args) => _logger.Information(message, args);

        /// <inheritdoc/>
        public void Warning(string message) => _logger.Warning(message);

        /// <inheritdoc/>
        public void Warning(string message, params object[] args) => _logger.Warning(message, args);

        /// <inheritdoc/>
        public void Error(string message) => _logger.Error(message);

        /// <inheritdoc/>
        public void Error(string message, params object[] args) => _logger.Error(message, args);

        /// <inheritdoc/>
        public void Error(Exception exception, string message) => _logger.Error(exception, message);

        /// <inheritdoc/>
        public void Error(Exception exception, string message, params object[] args) =>
            _logger.Error(exception, message, args);

        /// <inheritdoc/>
        public void Fatal(string message) => _logger.Fatal(message);

        /// <inheritdoc/>
        public void Fatal(string message, params object[] args) => _logger.Fatal(message, args);

        /// <inheritdoc/>
        public void Fatal(Exception exception, string message) => _logger.Fatal(exception, message);

        /// <inheritdoc/>
        public void Fatal(Exception exception, string message, params object[] args) =>
            _logger.Fatal(exception, message, args);
    }
}
