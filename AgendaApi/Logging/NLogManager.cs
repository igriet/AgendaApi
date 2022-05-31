using NLog;

namespace AgendaApi.Logging
{
    public class NLogManager : ILogManager
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public void Debug(string message)
        {
            Logger logger = LogManager.GetLogger("EventLogTarget");
            var logEventInfo = new LogEventInfo(NLog.LogLevel.Error, "EventLogMessage", $"{message}, generated at {DateTime.UtcNow}.");
            logger.Log(logEventInfo);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}
