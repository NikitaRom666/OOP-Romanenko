using Xunit;
using Lab24;

namespace Lab24.Tests
{
    public class StrategyTests
    {
        [Fact]
        public void SquareOperationStrategy_Execute_ReturnsCorrectSquare()
        {
            var strategy = new SquareOperationStrategy();
            double input = 5.0;
            double expected = 25.0;

            double result = strategy.Execute(input);

            Assert.Equal(expected, result);
            Assert.Equal("Квадрат", strategy.OperationName);
        }

        [Theory]
        [InlineData(2.0, 4.0)]
        [InlineData(3.0, 9.0)]
        [InlineData(10.0, 100.0)]
        [InlineData(0.0, 0.0)]
        [InlineData(-5.0, 25.0)]
        public void SquareOperationStrategy_Execute_WithVariousInputs_ReturnsCorrectSquare(double input, double expected)
        {
            var strategy = new SquareOperationStrategy();
            double result = strategy.Execute(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CubeOperationStrategy_Execute_ReturnsCorrectCube()
        {
            var strategy = new CubeOperationStrategy();
            double input = 3.0;
            double expected = 27.0;

            double result = strategy.Execute(input);

            Assert.Equal(expected, result);
            Assert.Equal("Куб", strategy.OperationName);
        }

        [Theory]
        [InlineData(2.0, 8.0)]
        [InlineData(4.0, 64.0)]
        [InlineData(5.0, 125.0)]
        [InlineData(0.0, 0.0)]
        [InlineData(-2.0, -8.0)]
        public void CubeOperationStrategy_Execute_WithVariousInputs_ReturnsCorrectCube(double input, double expected)
        {
            var strategy = new CubeOperationStrategy();
            double result = strategy.Execute(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SquareRootOperationStrategy_Execute_ReturnsCorrectSquareRoot()
        {
            var strategy = new SquareRootOperationStrategy();
            double input = 16.0;
            double expected = 4.0;

            double result = strategy.Execute(input);

            Assert.Equal(expected, result);
            Assert.Equal("Квадратний корінь", strategy.OperationName);
        }

        [Theory]
        [InlineData(25.0, 5.0)]
        [InlineData(100.0, 10.0)]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.0)]
        public void SquareRootOperationStrategy_Execute_WithValidInputs_ReturnsCorrectSquareRoot(double input, double expected)
        {
            var strategy = new SquareRootOperationStrategy();
            double result = strategy.Execute(input);
            Assert.Equal(expected, result, precision: 5);
        }

        [Fact]
        public void SquareRootOperationStrategy_Execute_WithNegativeInput_ThrowsArgumentException()
        {
            var strategy = new SquareRootOperationStrategy();
            double input = -1.0;
            Assert.Throws<ArgumentException>(() => strategy.Execute(input));
        }
    }
}
