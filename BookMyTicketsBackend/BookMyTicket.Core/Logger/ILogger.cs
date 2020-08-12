using System;

namespace BookMyTicket.Core.Logger
{
    public interface ILogger
    {
        void LogError(Exception ex, string message);

        void LogInfo(string message);

        void LogWarning(Exception ex, string message);

        void LogDebug(string message);

        void LogFatal(Exception ex, string message);
    }
}
