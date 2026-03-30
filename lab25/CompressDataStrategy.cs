using System.Text;

namespace Lab25
{
    public class CompressDataStrategy : IDataProcessorStrategy
    {
        public string GetStrategyName()
        {
            return "Compress";
        }

        public string Process(string data)
        {
            StringBuilder compressed = new StringBuilder();
            int count = 1;

            for (int i = 0; i < data.Length; i++)
            {
                if (i + 1 < data.Length && data[i] == data[i + 1])
                {
                    count++;
                }
                else
                {
                    if (count > 1)
                    {
                        compressed.Append(data[i] + count.ToString());
                    }
                    else
                    {
                        compressed.Append(data[i]);
                    }
                    count = 1;
                }
            }

            return compressed.ToString();
        }
    }
}
