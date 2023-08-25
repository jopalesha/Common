using Jopalesha.Common.Infrastructure.Logging;
using Xunit.Abstractions;

namespace Jopalesha.Common.Tests
{
    /// <summary>
    /// Test output logger.
    /// </summary>
    public class OutputLogger : ILogger
    {
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputLogger"/> class.
        /// </summary>
        /// <param name="output">Output helper.</param>
        public OutputLogger(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <inheritdoc />
        public void Debug(string message)
        {
            _output.WriteLine(message);
        }

        /// <inheritdoc />
        public void Debug(string message, params object[] args)
        {
            _output.WriteLine(message, args);
        }

        /// <inheritdoc />
        public void Info(string message)
        {
            _output.WriteLine(message);
        }

        /// <inheritdoc />
        public void Info(string message, params object[] args)
        {
            _output.WriteLine(message, args);
        }

        /// <inheritdoc />
        public void Warning(string message)
        {
            _output.WriteLine(message);
        }

        /// <inheritdoc />
        public void Warning(string message, params object[] args)
        {
            _output.WriteLine(message, args);
        }

        /// <inheritdoc />
        public void Error(string message)
        {
            _output.WriteLine(message);
        }

        /// <inheritdoc />
        public void Error(string message, params object[] args)
        {
            _output.WriteLine(message, args);
        }

        /// <inheritdoc />
        public void Error(Exception exception, string message)
        {
            _output.WriteLine("Message: {0}. Exception: {1}", message, exception);
        }

        /// <inheritdoc />
        public void Error(Exception exception, string message, params object[] args)
        {
            _output.WriteLine(message, args);
            _output.WriteLine("Exception: {0}", exception);
        }

        /// <inheritdoc />
        public void Fatal(string message)
        {
            _output.WriteLine(message);
        }

        /// <inheritdoc />
        public void Fatal(string message, params object[] args)
        {
            _output.WriteLine(message, args);
        }

        /// <inheritdoc />
        public void Fatal(Exception exception, string message)
        {
            Error(exception, message);
        }

        /// <inheritdoc />
        public void Fatal(Exception exception, string message, params object[] args)
        {
            _output.WriteLine(message, args);
            _output.WriteLine("Exception: {0}", exception);
        }
    }
}
