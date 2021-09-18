namespace BinanceTrackerDesktop.Core.Calculator.Extension
{
    public static class BinanceCalculationExtension
    {
        public static bool ValueFitsToCalculation(this decimal source) => source != decimal.Zero;
    }
}
