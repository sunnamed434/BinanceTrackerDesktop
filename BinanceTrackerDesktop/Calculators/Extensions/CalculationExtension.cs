namespace BinanceTrackerDesktop.Calculators.Extensions;

public static class CalculationExtension
{
    public static bool ValueFitsForCalculation(this decimal source) => source != decimal.Zero;
}
