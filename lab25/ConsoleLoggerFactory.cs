namespace Lab25
{
    public class ConsoleLoggerFactory : LoggerFactory
    {
        public override ILogger CreateLogger()
        {
            return new ConsoleLogger();
        }
    }
}
