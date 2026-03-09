using System;
using System.Collections.Generic;

namespace lab30v13
{
    public class CurrencyConverter
    {
        private readonly Dictionary<string, decimal> rates = new()
        {
            {"USD", 1m},
            {"EUR", 0.93m},
            {"UAH", 36.9m},
            {"GBP", 0.81m}
        };

        public decimal GetRate(string currency)
        {
            if (!rates.ContainsKey(currency))
                throw new ArgumentException("Currency not supported");

            return rates[currency];
        }

        public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if(amount < 0) throw new ArgumentException("Amount cannot be negative");
            if (!rates.ContainsKey(fromCurrency) || !rates.ContainsKey(toCurrency))
                throw new ArgumentException("Currency not supported");

            decimal usdAmount = amount / rates[fromCurrency]; // конвертуємо у USD
            return usdAmount * rates[toCurrency];
        }
    }
}
