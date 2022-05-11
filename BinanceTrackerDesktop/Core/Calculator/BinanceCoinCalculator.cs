using BinanceTrackerDesktop.Core.Calculator.Options;

namespace BinanceTrackerDesktop.Core.Calculator
{
    public sealed class BinanceCoinCalculator
    {
        public static decimal GetPriceOf(IBinanceCoinOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return options.Amount * options.Price;
        }

        public static decimal GetPriceOf(IEnumerable<IBinanceCoinOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.Any() == false)
                throw new InvalidOperationException();

            decimal result = decimal.Zero;
            foreach (BinanceCoinOptions property in options)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));

                result += GetPriceOf(property);
            }

            return result;
        }
    }
}
