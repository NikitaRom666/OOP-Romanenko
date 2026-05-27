namespace Lab25
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("[ConsoleLogger] " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + message);
        }
    }
}
