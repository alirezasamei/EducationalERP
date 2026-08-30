namespace Sales.Domain.Common;

public record struct Money(decimal Amount, string Currency)
{
    public static Money operator +(Money left, Money right)
        => new(left.Amount + CurrencyConvertor(right, left.Currency).Amount, left.Currency);
    public static Money operator *(Money money, decimal multiplier)
        => new(money.Amount * multiplier, money.Currency);

    public static Money CurrencyConvertor(Money money, string destinationCurrency)
        => new(money.Amount, destinationCurrency); // must be changed
}

public static class MoneyExtentions
{
    public static Money SumInSameCurrencies(this IEnumerable<Money> source)
    {
        if (source.Select(x => x.Currency).Distinct().Count() > 1)
            throw new Exception("Currencies are different");
        var sumAmount = source.Sum(x => x.Amount);
        var currency = source.First().Currency;
        return new(sumAmount, currency);
    }
    public static Money SumInDifferentCurrencies(this IEnumerable<Money> source, string destinationCurrency)
    {
        var sumAmount = source.Sum(x => Money.CurrencyConvertor(x, destinationCurrency).Amount);
        var currency = source.First().Currency;
        return new(sumAmount, currency);
    }
}
