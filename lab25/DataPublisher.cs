namespace Lab25
{
    public class DataPublisher
    {
        public event Action<string, string> DataProcessed;

        public void PublishDataProcessed(string processedData, string strategyName)
        {
            if (DataProcessed != null)
            {
                DataProcessed(processedData, strategyName);
            }
        }
    }
}
