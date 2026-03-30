namespace Lab24
{
    public class HistoryLoggerObserver
    {
        private List<string> _history;

        public HistoryLoggerObserver()
        {
            _history = new List<string>();
        }

        public void OnResultCalculated(double result, string operationName)
        {
            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {operationName}: {result:F4}";
            _history.Add(entry);
        }

        public List<string> GetHistory()
        {
            return new List<string>(_history);
        }

        public void PrintHistory()
        {
            Console.WriteLine("\n=== Історія обчислень ===");
            if (_history.Count == 0)
                Console.WriteLine("Історія порожня");
            else
                foreach (var entry in _history)
                    Console.WriteLine(entry);
            Console.WriteLine("==========================\n");
        }

        public void ClearHistory()
        {
            _history.Clear();
        }
    }
}
