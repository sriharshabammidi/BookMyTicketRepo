using System;

namespace BookMyTicket.Core.Logger
{
    public class Logger : ILogger
    {
        private Serilog.ILogger logger;

        public Logger(Serilog.ILogger _logger)
        {
            logger = _logger;
        }

        public void LogError(Exception ex, string message)
        {
            logger.Error(ex, message);
        }

        public void LogInfo(string message)
        {
            logger.Information(message);
        }

        public void LogWarning(Exception ex, string message)
        {
            logger.Warning(ex, message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogFatal(Exception ex, string message)
        {
            logger.Fatal(ex, message);
        }
    }
}
