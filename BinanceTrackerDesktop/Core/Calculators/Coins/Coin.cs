namespace BinanceTrackerDesktop.Core.Calculator.Options
{
    public sealed class Coin : ICoin
    {
        public decimal Price { get; }

        public decimal Amount { get; }



        public Coin(decimal price, decimal amount)
        {
            Price = price;
            Amount = amount;
        }
    }
}
