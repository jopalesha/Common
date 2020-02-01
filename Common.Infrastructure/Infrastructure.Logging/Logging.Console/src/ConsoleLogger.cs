using System;

namespace Jopalesha.Common.Infrastructure.Logging.Console
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message, params object[] args)
        {
            System.Console.WriteLine(message);
        }

        public void Warning(string message, params object[] args)
        {
            System.Console.WriteLine(message);
        }

        public void Error(Exception exception)
        {
            System.Console.WriteLine(exception);
        }

        public void Error(string message, params object[] args)
        {
            System.Console.WriteLine(message);
        }

        public void Error(string message, Exception exception)
        {
            System.Console.WriteLine($"{message} - {exception}");
        }

        public void Debug(string message, params object[] args)
        {
            System.Console.WriteLine(message);
        }

        public void Fatal(string message, Exception exception)
        {
            System.Console.WriteLine($"{message} - {exception}");
        }
    }
}