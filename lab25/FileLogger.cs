namespace Lab25
{
    public class FileLogger : ILogger
    {
        private string _filePath;

        public FileLogger(string filePath = "log.txt")
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            string logEntry = "[FileLogger] " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message;
            File.AppendAllText(_filePath, logEntry + Environment.NewLine);
            Console.WriteLine("[FileLogger] Записано в файл: " + logEntry);
        }
    }
}
