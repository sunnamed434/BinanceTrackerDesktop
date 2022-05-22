using BinanceTrackerDesktop.Core.Calculators.Coins.Calculators;

namespace BinanceTrackerDesktop.Core.Calculator
{
    public sealed class CoinsCalculators
    {
        public static readonly ICoinsCalculator CoinsCalculator = new CoinsCalculator();
    }
}
