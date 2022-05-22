namespace BinanceTrackerDesktop.Core.Calculator.Options
{
    public interface ICoin
    {
        decimal Price { get; }

        decimal Amount { get; }
    }
}
