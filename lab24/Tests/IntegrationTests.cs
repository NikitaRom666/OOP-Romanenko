using Xunit;
using Lab24;
using System;
using System.Collections.Generic;

namespace Lab24.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void NumericProcessor_WithResultPublisher_WorksTogether()
        {
            var processor = new NumericProcessor(new SquareOperationStrategy());
            var publisher = new ResultPublisher();
            var historyLogger = new HistoryLoggerObserver();

            publisher.ResultCalculated += historyLogger.OnResultCalculated;

            double result = processor.Process(5.0);
            publisher.PublishResult(result, processor.GetCurrentOperationName());

            var history = historyLogger.GetHistory();
            Assert.Single(history);
            Assert.Contains("Квадрат", history[0]);
            Assert.Contains("25", history[0]);
        }

        [Fact]
        public void FullWorkflow_WithMultipleStrategiesAndObservers_WorksCorrectly()
        {
            var processor = new NumericProcessor(new SquareOperationStrategy());
            var publisher = new ResultPublisher();
            var consoleLogger = new ConsoleLoggerObserver();
            var historyLogger = new HistoryLoggerObserver();
            var thresholdNotifier = new ThresholdNotifierObserver(threshold: 100.0);

            publisher.ResultCalculated += consoleLogger.OnResultCalculated;
            publisher.ResultCalculated += historyLogger.OnResultCalculated;
            publisher.ResultCalculated += thresholdNotifier.OnResultCalculated;

            processor.SetStrategy(new SquareOperationStrategy());
            double result1 = processor.Process(10.0);
            publisher.PublishResult(result1, processor.GetCurrentOperationName());

            processor.SetStrategy(new CubeOperationStrategy());
            double result2 = processor.Process(5.0);
            publisher.PublishResult(result2, processor.GetCurrentOperationName());

            processor.SetStrategy(new SquareRootOperationStrategy());
            double result3 = processor.Process(64.0);
            publisher.PublishResult(result3, processor.GetCurrentOperationName());

            var history = historyLogger.GetHistory();
            Assert.Equal(3, history.Count);
            Assert.Contains("Квадрат", history[0]);
            Assert.Contains("Куб", history[1]);
            Assert.Contains("Квадратний корінь", history[2]);
        }

        [Fact]
        public void DynamicStrategyChange_WithObserver_NotifiesCorrectly()
        {
            var processor = new NumericProcessor(new SquareOperationStrategy());
            var publisher = new ResultPublisher();
            var historyLogger = new HistoryLoggerObserver();

            publisher.ResultCalculated += historyLogger.OnResultCalculated;

            double input = 4.0;

            processor.SetStrategy(new SquareOperationStrategy());
            double result1 = processor.Process(input);
            publisher.PublishResult(result1, processor.GetCurrentOperationName());

            processor.SetStrategy(new CubeOperationStrategy());
            double result2 = processor.Process(input);
            publisher.PublishResult(result2, processor.GetCurrentOperationName());

            processor.SetStrategy(new SquareRootOperationStrategy());
            double result3 = processor.Process(input);
            publisher.PublishResult(result3, processor.GetCurrentOperationName());

            Assert.Equal(16.0, result1);
            Assert.Equal(64.0, result2);
            Assert.Equal(2.0, result3);
            Assert.Equal(3, historyLogger.GetHistory().Count);
        }
    }
}
