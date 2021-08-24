using BinanceTrackerDesktop.Core.Currencies;
using ConsoleBinanceTracker.Core.Wallet.API;

namespace BinanceTrackerDesktop.Core.Formatter
{
    public class BinanceUserWalletStringFormatter
    {
        private const string FormatType = "0.00";



        public static string Format(BinanceUserWalletResult userWalletResult) => CurrencySymbol.EUR + userWalletResult.Value.ToString(FormatType);
    }
}
