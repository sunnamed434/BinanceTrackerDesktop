namespace BinanceTrackerDesktop.Core.Calculator.API
{
    public interface IBinanceCoinOptions
    {
        decimal Price { get; }

        decimal Amount { get; }
    }

    public class BinanceCoinOptions : IBinanceCoinOptions
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
