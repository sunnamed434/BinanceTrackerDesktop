using BinanceTrackerDesktop.Core.Currencies;
using BinanceTrackerDesktop.Core.UserData.API;
using ConsoleBinanceTracker.Core.Wallet.API;
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
        public string Format(decimal value)
        {
            return CurrencySymbol.EUR + value.ToString(StringFormats.Default);
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

    public class BinanceUserBalanceLosesColorFormatter : IBinanceValueFormatter<Color, BinanceUserBalanceLosesOptions>
    {
        public Color Format(BinanceUserBalanceLosesOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.WalletResult.Value > options.Data.Balance)
                return Color.Green;
            else if (options.WalletResult.Value == options.Data.Balance)
                return Color.Gray;
            else return Color.Red;
        }



        public class BinanceUserBalanceLosesOptions
        {
            public readonly BinanceUserWalletResult WalletResult;

            public readonly BinanceUserData Data;



            public BinanceUserBalanceLosesOptions(BinanceUserWalletResult walletResult, BinanceUserData data)
            {
                WalletResult = walletResult;
                Data = data;
            }
        }
    }
}
