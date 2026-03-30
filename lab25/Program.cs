using System;

namespace Lab25
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Лабораторна робота №25: Інтеграція патернів\n");

            Console.WriteLine("Сценарій 1: Повна інтеграція");
            Console.WriteLine("----------------------------");

            var consoleFactory = new ConsoleLoggerFactory();
            var loggerManager = LoggerManager.GetInstance(consoleFactory);

            var dataContext = new DataContext(new EncryptDataStrategy());
            var dataPublisher = new DataPublisher();
            var processingObserver = new ProcessingLoggerObserver(loggerManager);

            dataPublisher.DataProcessed += processingObserver.OnDataProcessed;

            string testData1 = "Hello World";
            Console.WriteLine("Початкові дані: " + testData1);
            string processedData1 = dataContext.ProcessData(testData1);
            Console.WriteLine("Оброблені дані: " + processedData1);
            dataPublisher.PublishDataProcessed(processedData1, dataContext.GetStrategyName());

            Console.WriteLine();

            Console.WriteLine("Сценарій 2: Динамічна зміна логера");
            Console.WriteLine("----------------------------");

            var fileFactory = new FileLoggerFactory("lab25_log.txt");
            loggerManager.SetFactory(fileFactory);

            string testData2 = "Test Data for File Logger";
            Console.WriteLine("Початкові дані: " + testData2);
            string processedData2 = dataContext.ProcessData(testData2);
            Console.WriteLine("Оброблені дані: " + processedData2);
            dataPublisher.PublishDataProcessed(processedData2, dataContext.GetStrategyName());

            Console.WriteLine();

            Console.WriteLine("Сценарій 3: Динамічна зміна стратегії");
            Console.WriteLine("----------------------------");

            dataContext.SetStrategy(new CompressDataStrategy());

            string testData3 = "AAAABBBCCCCDDD";
            Console.WriteLine("Початкові дані: " + testData3);
            string processedData3 = dataContext.ProcessData(testData3);
            Console.WriteLine("Оброблені дані: " + processedData3);
            dataPublisher.PublishDataProcessed(processedData3, dataContext.GetStrategyName());
        }
    }
}
