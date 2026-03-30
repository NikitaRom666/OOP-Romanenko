using Xunit;
using Lab24;
using System;
using System.Collections.Generic;

namespace Lab24.Tests
{
    public class ObserverTests
    {
        [Fact]
        public void ResultPublisher_PublishResult_InvokesEvent()
        {
            var publisher = new ResultPublisher();
            bool eventInvoked = false;
            double receivedResult = 0;
            string receivedOperationName = string.Empty;

            publisher.ResultCalculated += (result, operationName) =>
            {
                eventInvoked = true;
                receivedResult = result;
                receivedOperationName = operationName;
            };

            publisher.PublishResult(42.0, "Тестова операція");

            Assert.True(eventInvoked);
            Assert.Equal(42.0, receivedResult);
            Assert.Equal("Тестова операція", receivedOperationName);
        }

        [Fact]
        public void ResultPublisher_PublishResult_WithNoSubscribers_DoesNotThrow()
        {
            var publisher = new ResultPublisher();
            publisher.PublishResult(10.0, "Операція");
        }

        [Fact]
        public void ResultPublisher_PublishResult_WithMultipleSubscribers_InvokesAll()
        {
            var publisher = new ResultPublisher();
            int invocationCount = 0;

            publisher.ResultCalculated += (r, o) => invocationCount++;
            publisher.ResultCalculated += (r, o) => invocationCount++;
            publisher.ResultCalculated += (r, o) => invocationCount++;

            publisher.PublishResult(5.0, "Тест");
            Assert.Equal(3, invocationCount);
        }

        [Fact]
        public void HistoryLoggerObserver_OnResultCalculated_AddsToHistory()
        {
            var observer = new HistoryLoggerObserver();

            observer.OnResultCalculated(10.0, "Квадрат");
            observer.OnResultCalculated(27.0, "Куб");
            observer.OnResultCalculated(4.0, "Квадратний корінь");

            var history = observer.GetHistory();
            Assert.Equal(3, history.Count);
            Assert.Contains("Квадрат", history[0]);
            Assert.Contains("Куб", history[1]);
            Assert.Contains("Квадратний корінь", history[2]);
        }

        [Fact]
        public void HistoryLoggerObserver_ClearHistory_RemovesAllEntries()
        {
            var observer = new HistoryLoggerObserver();
            observer.OnResultCalculated(10.0, "Тест");
            observer.OnResultCalculated(20.0, "Тест2");

            observer.ClearHistory();

            var history = observer.GetHistory();
            Assert.Empty(history);
        }

        [Fact]
        public void ThresholdNotifierObserver_OnResultCalculated_WithResultBelowThreshold_DoesNotNotify()
        {
            var observer = new ThresholdNotifierObserver(threshold: 100.0);

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                observer.OnResultCalculated(50.0, "Тест");
                string output = sw.ToString();
                Assert.DoesNotContain("ПОПЕРЕДЖЕННЯ", output);
            }
            Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        [Fact]
        public void ThresholdNotifierObserver_SetThreshold_UpdatesThreshold()
        {
            var observer = new ThresholdNotifierObserver(threshold: 100.0);
            observer.SetThreshold(200.0);

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                observer.OnResultCalculated(150.0, "Тест");
                string output = sw.ToString();
                Assert.DoesNotContain("ПОПЕРЕДЖЕННЯ", output);
            }
            Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        [Fact]
        public void ConsoleLoggerObserver_OnResultCalculated_OutputsToConsole()
        {
            var observer = new ConsoleLoggerObserver();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                observer.OnResultCalculated(42.5, "Тестова операція");
                string output = sw.ToString();
                Assert.Contains("Консольний логер", output);
                Assert.Contains("Тестова операція", output);
                Assert.Contains("42.5", output);
            }
            Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }
    }
}
