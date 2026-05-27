using Xunit;
using MyProject;

public class CalculatorTests
{
    [Fact]
    public void Add_TwoNumbers_ReturnsCorrectSum()
    {
        var calc = new Calculator();
        int result = calc.Add(5, 3);
        Assert.Equal(8, result);
    }

    [Fact]
    public void Add_NegativeNumbers_ReturnsCorrectSum()
    {
        var calc = new Calculator();
        int result = calc.Add(-5, -3);
        Assert.Equal(-8, result);
    }

    [Fact]
    public void Divide_ValidNumbers_ReturnsResult()
    {
        var calc = new Calculator();
        int result = calc.Divide(10, 2);
        Assert.Equal(5, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        var calc = new Calculator();
        Assert.Throws<ArgumentException>(() => calc.Divide(10, 0));
    }

    [Fact]
    public void IsEven_EvenNumber_ReturnsTrue()
    {
        var calc = new Calculator();
        bool result = calc.IsEven(4);
        Assert.True(result);
    }

    [Fact]
    public void IsEven_OddNumber_ReturnsFalse()
    {
        var calc = new Calculator();
        bool result = calc.IsEven(5);
        Assert.False(result);
    }
}