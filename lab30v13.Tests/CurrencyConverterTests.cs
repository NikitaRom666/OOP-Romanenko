using lab30v13;

namespace lab30v13.Tests;

public class CurrencyConverterTests
{
    private readonly CurrencyConverter _converter = new();

    [Fact]
    public void GetRate_USD_ReturnsOne()
    {
        decimal rate = _converter.GetRate("USD");
        Assert.Equal(1.0m, rate);
    }

    [Fact]
    public void GetRate_CaseInsensitive_ReturnsRate()
    {
        decimal lower = _converter.GetRate("eur");
        decimal upper = _converter.GetRate("EUR");
        Assert.Equal(upper, lower);
    }

    [Fact]
    public void GetRate_UnsupportedCurrency_ThrowsKeyNotFoundException()
    {
        Assert.Throws<KeyNotFoundException>(() => _converter.GetRate("XYZ"));
    }

    [Fact]
    public void GetRate_NullCurrency_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _converter.GetRate(null!));
    }

    [Fact]
    public void GetRate_EmptyString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _converter.GetRate(""));
    }

    [Fact]
    public void Convert_SameCurrency_ReturnsSameAmount()
    {
        decimal result = _converter.Convert(100m, "USD", "USD");
        Assert.Equal(100.00m, result);
    }

    [Fact]
    public void Convert_NegativeAmount_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _converter.Convert(-50m, "USD", "EUR"));
    }

    [Fact]
    public void Convert_ZeroAmount_ReturnsZero()
    {
        decimal result = _converter.Convert(0m, "USD", "UAH");
        Assert.Equal(0.00m, result);
    }

    [Fact]
    public void IsSupportedCurrency_KnownCode_ReturnsTrue()
    {
        Assert.True(_converter.IsSupportedCurrency("UAH"));
    }

    [Fact]
    public void IsSupportedCurrency_UnknownCode_ReturnsFalse()
    {
        Assert.False(_converter.IsSupportedCurrency("ABC"));
    }

    [Theory]
    [InlineData("USD", 1.0)]
    [InlineData("EUR", 0.92)]
    [InlineData("UAH", 41.5)]
    [InlineData("GBP", 0.79)]
    [InlineData("PLN", 4.02)]
    public void GetRate_SupportedCurrencies_ReturnsExpectedRate(string code, double expected)
    {
        decimal rate = _converter.GetRate(code);
        Assert.Equal((decimal)expected, rate);
    }

    [Theory]
    [InlineData(100,  "USD", "EUR",  92.00)]
    [InlineData(100,  "EUR", "USD", 108.70)]
    [InlineData(1000, "UAH", "USD",  24.10)]
    [InlineData(50,   "GBP", "USD",  63.29)]
    public void Convert_ValidAmounts_ReturnsExpectedResult(
        double amount, string from, string to, double expected)
    {
        decimal result = _converter.Convert((decimal)amount, from, to);
        Assert.Equal((decimal)expected, result);
    }
}
