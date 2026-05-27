namespace Lab25
{
    public class FileLoggerFactory : LoggerFactory
    {
        private string _filePath;

        public FileLoggerFactory(string filePath = "log.txt")
        {
            _filePath = filePath;
        }

        public override ILogger CreateLogger()
        {
            return new FileLogger(_filePath);
        }
    }
}
