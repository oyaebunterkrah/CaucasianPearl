using System;

namespace CaucasianPearl.Core.Services.LoggingService
{
    public interface ILogService
    {
        void Info(string message);

        void Warning(string message);

        void Debug(string message);

        void Error(string message);
        void Error(Exception exception);

        void Fatal(string message);
        void Fatal(Exception exception);
    }
}