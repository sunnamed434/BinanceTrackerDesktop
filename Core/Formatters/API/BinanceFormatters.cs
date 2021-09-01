using BinanceTrackerDesktop.Core.Currencies;
using BinanceTrackerDesktop.Core.UserData.API;
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
            if (content == null)
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

            if (options.Value > options.Data.Balance)
                return Color.Green;
            else if (options.Value < options.Data.Balance)
                return Color.Red;
            else
                return Color.Gray;
        }



        public class BinanceUserBalanceLossesOptions
        {
            public readonly decimal Value;

            public readonly BinanceUserData Data;



            public BinanceUserBalanceLossesOptions(decimal value, BinanceUserData data)
            {
                Value = value;
                Data = data;
            }
        }
    }
}
