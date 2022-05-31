namespace BinanceTrackerDesktop.Calculators.Coins;

public interface ICoin
{
    decimal Price { get; }

    decimal Amount { get; }
}
