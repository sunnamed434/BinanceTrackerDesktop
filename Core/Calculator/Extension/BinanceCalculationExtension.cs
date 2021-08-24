using BinanceTrackerDesktop.Core.Calculator.API;

namespace BinanceTrackerDesktop.Core.Calculator.Extension
{
    public static class BinanceCalculationExtension
    {
        public static bool ValueFitsToCalculation(this IBinanceCoinOptions source) => ValueFitsToCalculation(source.Price);

        public static bool ValueFitsToCalculation(this decimal source) => (double)source != BinanceCalculationPriceStandarts.DefaultValue;
    }
}
