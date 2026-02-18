using Xunit;
using Lab24;

namespace Lab24.Tests
{
    public class NumericProcessorTests
    {
        [Fact]
        public void NumericProcessor_Constructor_WithNullStrategy_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new NumericProcessor(null!));
        }

        [Fact]
        public void NumericProcessor_Process_WithSquareStrategy_ReturnsCorrectResult()
        {
            var strategy = new SquareOperationStrategy();
            var processor = new NumericProcessor(strategy);
            double input = 5.0;
            double expected = 25.0;

            double result = processor.Process(input);

            Assert.Equal(expected, result);
            Assert.Equal("Квадрат", processor.GetCurrentOperationName());
        }

        [Fact]
        public void NumericProcessor_SetStrategy_ChangesStrategy()
        {
            var initialStrategy = new SquareOperationStrategy();
            var processor = new NumericProcessor(initialStrategy);
            var newStrategy = new CubeOperationStrategy();

            processor.SetStrategy(newStrategy);
            double result = processor.Process(3.0);

            Assert.Equal(27.0, result);
            Assert.Equal("Куб", processor.GetCurrentOperationName());
        }

        [Fact]
        public void NumericProcessor_SetStrategy_WithNull_ThrowsArgumentNullException()
        {
            var strategy = new SquareOperationStrategy();
            var processor = new NumericProcessor(strategy);
            Assert.Throws<ArgumentNullException>(() => processor.SetStrategy(null!));
        }

        [Fact]
        public void NumericProcessor_Process_WithDifferentStrategies_WorksCorrectly()
        {
            var processor = new NumericProcessor(new SquareOperationStrategy());

            Assert.Equal(16.0, processor.Process(4.0));
            Assert.Equal("Квадрат", processor.GetCurrentOperationName());

            processor.SetStrategy(new CubeOperationStrategy());
            Assert.Equal(64.0, processor.Process(4.0));
            Assert.Equal("Куб", processor.GetCurrentOperationName());

            processor.SetStrategy(new SquareRootOperationStrategy());
            Assert.Equal(4.0, processor.Process(16.0));
            Assert.Equal("Квадратний корінь", processor.GetCurrentOperationName());
        }
    }
}
