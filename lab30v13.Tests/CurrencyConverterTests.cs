using Xunit;
using lab30v13;
using System;

namespace lab30v13.Tests
{
    public class CurrencyConverterTests
    {
        private readonly CurrencyConverter converter = new CurrencyConverter();

        [Fact]
        public void GetRate_USD_Returns1()
        {
            Assert.Equal(1m, converter.GetRate("USD"));
        }

        [Fact]
        public void GetRate_UnknownCurrency_Throws()
        {
            Assert.Throws<ArgumentException>(() => converter.GetRate("XYZ"));
        }

        [Theory]
        [InlineData(100, "USD", "EUR", 93)]
        [InlineData(100, "EUR", "USD", 107.526882)]
        [InlineData(100, "UAH", "USD", 2.710027)]
        [InlineData(100, "GBP", "UAH", 4555.555556)]
        public void Convert_ValidCurrencies_ReturnsExpected(decimal amount, string from, string to, decimal expected)
        {
            decimal result = converter.Convert(amount, from, to);
            Assert.Equal(Math.Round(expected,6), Math.Round(result,6));
        }

        [Fact]
        public void Convert_UnknownCurrency_Throws()
        {
            Assert.Throws<ArgumentException>(() => converter.Convert(100, "USD", "XYZ"));
        }

        [Fact]
        public void Convert_SameCurrency_ReturnsSameAmount()
        {
            decimal amount = 123.45m;
            decimal result = converter.Convert(amount, "USD", "USD");
            Assert.Equal(amount, result);
        }

        [Theory]
        [InlineData(0, "USD", "EUR")]
        [InlineData(0, "EUR", "USD")]
        public void Convert_ZeroAmount_ReturnsZero(decimal amount, string from, string to)
        {
            decimal result = converter.Convert(amount, from, to);
            Assert.Equal(0m, result);
        }

        [Fact]
        public void GetRate_AllCurrencies_ReturnsCorrectRates()
        {
            Assert.Equal(1m, converter.GetRate("USD"));
            Assert.Equal(0.93m, converter.GetRate("EUR"));
            Assert.Equal(36.9m, converter.GetRate("UAH"));
            Assert.Equal(0.81m, converter.GetRate("GBP"));
        }

        [Theory]
        [InlineData(50, "EUR", "GBP")]
        [InlineData(100, "GBP", "EUR")]
        public void Convert_MultipleConversions(decimal amount, string from, string to)
        {
            decimal result = converter.Convert(amount, from, to);
            Assert.True(result > 0);
        }

        [Fact]
        public void Convert_NegativeAmount_Throws()
        {
            Assert.Throws<ArgumentException>(() => converter.Convert(-100, "USD", "EUR"));
        }
    }
}
