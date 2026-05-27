using System;

namespace Lab24
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №24: Strategy + Observer ===\n");

            var processor = new NumericProcessor(new SquareOperationStrategy());
            var publisher = new ResultPublisher();

            var consoleLogger = new ConsoleLoggerObserver();
            var historyLogger = new HistoryLoggerObserver();
            var thresholdNotifier = new ThresholdNotifierObserver(threshold: 100.0);

            publisher.ResultCalculated += consoleLogger.OnResultCalculated;
            publisher.ResultCalculated += historyLogger.OnResultCalculated;
            publisher.ResultCalculated += thresholdNotifier.OnResultCalculated;

            Console.WriteLine("1. Тестування стратегії 'Квадрат':");
            Console.WriteLine("-----------------------------------");
            double[] testValues1 = { 5.0, 10.0, 15.0 };
            foreach (var value in testValues1)
            {
                double result = processor.Process(value);
                publisher.PublishResult(result, processor.GetCurrentOperationName());
            }

            Console.WriteLine("\n2. Зміна стратегії на 'Куб':");
            Console.WriteLine("-----------------------------------");
            processor.SetStrategy(new CubeOperationStrategy());
            double[] testValues2 = { 3.0, 4.0, 5.0 };
            foreach (var value in testValues2)
            {
                double result = processor.Process(value);
                publisher.PublishResult(result, processor.GetCurrentOperationName());
            }

            Console.WriteLine("\n3. Зміна стратегії на 'Квадратний корінь':");
            Console.WriteLine("-----------------------------------");
            processor.SetStrategy(new SquareRootOperationStrategy());
            double[] testValues3 = { 16.0, 25.0, 100.0 };
            foreach (var value in testValues3)
            {
                double result = processor.Process(value);
                publisher.PublishResult(result, processor.GetCurrentOperationName());
            }

            historyLogger.PrintHistory();
        }
    }
}
