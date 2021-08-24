using BinanceTrackerDesktop.Core.Calculator.API;
using System;
using System.Collections.Generic;

namespace BinanceTrackerDesktop.Core.Calculator
{
    public class BinanceCoinCalculator
    {
        public static decimal GetPrice(BinanceCoinOptions property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            return property.Amount * property.Price;
        }

        public static decimal GetPrice(IEnumerable<BinanceCoinOptions> properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            decimal result = decimal.Zero;
            foreach (BinanceCoinOptions property in properties)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));

                result += GetPrice(property);
            }

            return result;
        }
    }
}
