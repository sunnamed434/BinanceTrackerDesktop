using BinanceTrackerDesktop.Core.Calculator.Options;

namespace BinanceTrackerDesktop.Core.Calculators.Coins.Calculators
{
    public interface ICoinsCalculator
    {
        decimal CalculatePrice(ICoin coin);

        decimal CalculatePrice(IEnumerable<ICoin> coins);
    }
}
