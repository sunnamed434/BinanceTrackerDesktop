namespace BinanceTrackerDesktop.Core.Calculator.Extensions
{
    public static class BinanceCalculationExtension
    {
        public static bool ValueFitsForCalculation(this decimal source) => source != decimal.Zero;

        public static bool ValueFitsToCalculation(this decimal? source) => source != null && source != decimal.Zero;
    }
}
