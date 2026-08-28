namespace Sales.Domain.Common
{
    public record struct Money
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
