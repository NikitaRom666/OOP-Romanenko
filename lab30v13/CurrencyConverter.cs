namespace lab30v13;

public class CurrencyConverter
{
    private readonly Dictionary<string, decimal> _rates = new()
    {
        { "USD", 1.0m },
        { "EUR", 0.92m },
        { "UAH", 41.5m },
        { "GBP", 0.79m },
        { "PLN", 4.02m },
        { "CHF", 0.90m },
    };

    public decimal GetRate(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency code cannot be null or empty.", nameof(currency));

        string code = currency.ToUpper();

        if (!_rates.TryGetValue(code, out decimal rate))
            throw new KeyNotFoundException($"Currency '{code}' is not supported.");

        return rate;
    }

    public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));

        decimal fromRate = GetRate(fromCurrency);
        decimal toRate   = GetRate(toCurrency);

        decimal amountInUsd = amount / fromRate;
        return Math.Round(amountInUsd * toRate, 2);
    }

    public bool IsSupportedCurrency(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency)) return false;
        return _rates.ContainsKey(currency.ToUpper());
    }
}
