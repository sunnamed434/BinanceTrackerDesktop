using BinanceTrackerDesktop.Core.Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BinanceTrackerDesktop.Core.Calculator
{
    public sealed class BinanceCoinCalculator
    {
        public static decimal GetPriceOf(BinanceCoinOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return options.Amount * options.Price;
        }

        public static decimal GetPriceOf(IEnumerable<BinanceCoinOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (!options.Any())
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
