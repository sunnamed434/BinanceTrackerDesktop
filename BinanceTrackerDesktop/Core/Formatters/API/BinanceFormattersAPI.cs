using BinanceTrackerDesktop.Core.Currencies;
using System;
using System.Drawing;
using static BinanceTrackerDesktop.Core.Formatters.API.BinanceUserBalanceLosesColorFormatter;

namespace BinanceTrackerDesktop.Core.Formatters.API
{
    public interface IBinanceValueFormatter<Result, Argument>
    {
        Result Format(Argument value);
    }

    public class BinanceCurrencyValueFormatter : IBinanceValueFormatter<string, decimal>
    {
        private const string Default = "0.00";



        public string Format(decimal value)
        {
            return CurrencySymbol.EUR + value.ToString(Default);
        }
    }

    public class BinanceCryptocurrencyStringFormatter : IBinanceValueFormatter<string, string>
    {
        public string Format(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            return content + CurrencyName.EUR;
        }
    }

    public class BinanceUserBalanceLosesColorFormatter : IBinanceValueFormatter<Color, BinanceUserBalanceLossesOptions>
    {
        public Color Format(BinanceUserBalanceLossesOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.BestBalance == decimal.Zero)
                return Color.Gray;
            else if (options.BestBalance > options.TotalBalance)
                return Color.Red;
            else if (options.BestBalance < options.TotalBalance)
                return Color.Green;
            else 
                return Color.Gray;
        }



        public class BinanceUserBalanceLossesOptions
        {
            public readonly decimal TotalBalance;

            public readonly decimal BestBalance;



            public BinanceUserBalanceLossesOptions(decimal totalBalance, decimal bestBalance)
            {
                TotalBalance = totalBalance;
                BestBalance = bestBalance;
            }
        }
    }
}
