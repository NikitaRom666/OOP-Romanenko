namespace Lab24
{
    public class ConsoleLoggerObserver
    {
        public void OnResultCalculated(double result, string operationName)
        {
            Console.WriteLine($"[Консольний логер] Операція: {operationName}, Результат: {result:F4}");
        }
    }
}
