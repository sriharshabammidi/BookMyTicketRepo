using System;

namespace BookMyTicket.Core.Logger
{
    public class Logger : ILogger
    {
        private Serilog.ILogger logger;

        public Logger(Serilog.ILogger _logger)
        {
            this.logger = _logger;
        }

        public void LogError(Exception ex, string message)
        {
            this.logger.Error(ex, message);
        }

        public void LogInfo(string message)
        {
            this.logger.Information(message);
        }

        public void LogWarning(Exception ex, string message)
        {
            this.logger.Warning(ex, message);
        }

        public void LogDebug(string message)
        {
            this.logger.Debug(message);
        }

        public void LogFatal(Exception ex, string message)
        {
            this.logger.Fatal(ex, message);
        }
    }
}
