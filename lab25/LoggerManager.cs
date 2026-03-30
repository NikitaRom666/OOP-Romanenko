namespace Lab25
{
    public class LoggerManager
    {
        private static LoggerManager _instance;
        private static object _lock = new object();
        private ILogger _logger;
        private LoggerFactory _factory;

        private LoggerManager(LoggerFactory factory)
        {
            _factory = factory;
            _logger = factory.CreateLogger();
        }

        public static LoggerManager GetInstance(LoggerFactory factory)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new LoggerManager(factory);
                    }
                }
            }
            return _instance;
        }

        public void SetFactory(LoggerFactory factory)
        {
            _factory = factory;
            _logger = factory.CreateLogger();
        }

        public ILogger GetLogger()
        {
            return _logger;
        }

        public void Log(string message)
        {
            _logger.Log(message);
        }
    }
}
