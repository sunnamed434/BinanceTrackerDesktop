using BinanceTrackerDesktop.Calculators.Coins.Calculators;

namespace BinanceTrackerDesktop.Calculators;

public sealed class CoinsCalculators
{
    public static readonly ICoinsCalculator CoinsCalculator = new CoinsCalculator();
}
