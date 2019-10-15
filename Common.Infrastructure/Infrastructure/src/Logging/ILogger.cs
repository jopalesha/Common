using System;

namespace Jopalesha.Common.Infrastructure.Logging
{
    public interface ILogger
    {
        void Info(string message, params object[] args);
        
        void Warning(string message, params object[] args);

        void Error(Exception exception);

        void Error(string message, params object[] args);
        
        void Error(string message, Exception exception);

        void Debug(string message, params object[] args);

        void Fatal(string message, Exception exception);
    }
}
