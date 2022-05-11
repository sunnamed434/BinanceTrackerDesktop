namespace BinanceTrackerDesktop.Core.Calculator.Options
{
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
