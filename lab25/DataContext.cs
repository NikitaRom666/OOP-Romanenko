namespace Lab25
{
    public class DataContext
    {
        private IDataProcessorStrategy _strategy;

        public DataContext(IDataProcessorStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IDataProcessorStrategy strategy)
        {
            _strategy = strategy;
        }

        public string ProcessData(string data)
        {
            return _strategy.Process(data);
        }

        public string GetStrategyName()
        {
            return _strategy.GetStrategyName();
        }
    }
}
