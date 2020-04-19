using System;
using Jopalesha.Common.Infrastructure.Logging;
using Xunit.Abstractions;

namespace Jopalesha.Common.Tests
{
    public class OutputLogger : ILogger
    {
        private readonly ITestOutputHelper _output;

        public OutputLogger(ITestOutputHelper output)
        {
            _output = output;
        }

        public void Info(string message, params object[] args)
        {
            _output.WriteLine(message, args);
        }

        public void Warning(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, params object[] args)
        {
            _output.WriteLine(message, args);
        }

        public void Error(string message, Exception exception)
        {
            _output.WriteLine($"{message}. {exception}");
        }

        public void Debug(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
