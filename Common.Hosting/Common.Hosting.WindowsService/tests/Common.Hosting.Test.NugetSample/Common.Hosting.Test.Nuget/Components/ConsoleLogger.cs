using System;
using Jopalesha.Common.Infrastructure.Logging;

namespace Common.Hosting.Test.Nuget.Components
{
    internal class ConsoleLogger : ILogger
    {
        public void Info(string message, params object[] args)
        {
            Console.WriteLine(message);
        }

        public void Warning(string message, params object[] args)
        {
            Console.WriteLine(message);
        }

        public void Error(Exception exception)
        {
            Console.WriteLine(exception);
        }

        public void Error(string message, params object[] args)
        {
            Console.WriteLine(message);
        }

        public void Error(string message, Exception exception)
        {
            Console.WriteLine($"{message} - {exception}");
        }

        public void Debug(string message, params object[] args)
        {
            Console.WriteLine(message);
        }

        public void Fatal(string message, Exception exception)
        {
            Console.WriteLine($"{message} - {exception}");
        }
    }
}