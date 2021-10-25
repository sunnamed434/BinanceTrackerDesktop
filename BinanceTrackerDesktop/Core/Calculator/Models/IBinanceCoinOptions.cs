namespace BinanceTrackerDesktop.Core.Calculator.Models
{
    public interface IBinanceCoinOptions
    {
        decimal Price { get; }

        decimal Amount { get; }
    }

    public sealed class BinanceCoinOptions : IBinanceCoinOptions
    {
        public decimal Price { get; }

        public decimal Amount { get; }



        public BinanceCoinOptions(decimal price, decimal amount)
        {
            Price = price;
            Amount = amount;
        }
    }
}
