namespace Lab25
{
    public class ProcessingLoggerObserver
    {
        private LoggerManager _loggerManager;

        public ProcessingLoggerObserver(LoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        public void OnDataProcessed(string processedData, string strategyName)
        {
            string message = "Дані оброблено стратегією '" + strategyName + "': " + processedData;
            _loggerManager.Log(message);
        }
    }
}
