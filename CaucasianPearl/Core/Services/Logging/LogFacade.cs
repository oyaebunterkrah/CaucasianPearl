using System;
using CaucasianPearl.Core.Utilities;
using NLog;

namespace CaucasianPearl.Core.Services.Logging
{
    public class LogFacade : ILogFacade
    {
        private Logger _logger;
  
        public LogFacade()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception exception)
        {
            Error(LogUtility.BuildExceptionMessage(exception));
        }

        public void Error(string message, Exception exception)
        {
            _logger.ErrorException(message, exception);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception exception)
        {
            Fatal(LogUtility.BuildExceptionMessage(exception));
        }
    }
}