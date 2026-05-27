namespace Lab25
{
    public interface IDataProcessorStrategy
    {
        string Process(string data);
        string GetStrategyName();
    }
}
