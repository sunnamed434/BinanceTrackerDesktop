namespace BinanceTrackerDesktop.Calculators.Coins;

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
