namespace Sales.Domain.Common;

public readonly record struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal Amount, string Currency)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(Amount);
        this.Amount = Amount;
        this.Currency = Currency;
    }

    public static Money operator +(Money left, Money right)
        => new(left.Amount + CurrencyConvertor(right, left.Currency).Amount, left.Currency);
    public static Money operator *(Money money, decimal multiplier)
        => new(money.Amount * multiplier, money.Currency);
    public static Money operator *(decimal multiplier,Money money)
        => new(money.Amount * multiplier, money.Currency);

    public static Money CurrencyConvertor(Money money, string destinationCurrency)
        => throw new ArithmeticException("Currency conversion is not possible yet");
}

public static class MoneyExtentions
{
    public static Money SumInSameCurrencies(this IEnumerable<Money> source)
    {
        string? currency = null;
        decimal sumAmount = 0;

        if(!source.Any())
            throw new InvalidOperationException("Can not operate sum on empty collection");

        foreach (var item in source)
        {
            if (currency is null)
                currency = item.Currency;
            else if (item.Currency != currency)
                throw new Exception("Currencies are different");
            sumAmount += item.Amount;
        }

        return new(sumAmount, currency);
    }
    public static Money SumInDifferentCurrencies(this IEnumerable<Money> source, string destinationCurrency)
    {
        var sumAmount = source.Sum(x => Money.CurrencyConvertor(x, destinationCurrency).Amount);
        var currency = source.First().Currency;
        return new(sumAmount, currency);
    }
}
