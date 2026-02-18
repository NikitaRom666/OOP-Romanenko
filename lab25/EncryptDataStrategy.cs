using System.Text;

namespace Lab25
{
    public class EncryptDataStrategy : IDataProcessorStrategy
    {
        public string GetStrategyName()
        {
            return "Encrypt";
        }

        public string Process(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }
    }
}
