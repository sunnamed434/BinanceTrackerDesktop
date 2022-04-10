using BinanceTrackerDesktop.Core.Currencies;
using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Save;
using System;
using System.Text;

namespace BinanceTrackerDesktop.Core.Formatters.Models
{
    public interface IFormatter<TArgument>
    {
        object Format(TArgument value);
    }

    public sealed class ValueStringFormatter : IFormatter<decimal>
    {
        private const string DefaultFormat = "0.00";



        public object Format(decimal value)
        {
            return new StringBuilder()
                .Append(value.ToString(DefaultFormat))
                .ToString();
        }
    }

    public sealed class BasedOnUserDataCurrencyFormatter : IFormatter<decimal>
    {
        public object Format(decimal value)
        {
            UserData userData = new BinaryUserDataSaveSystem().Read();

            return new StringBuilder()
                .Append(userData.Currency)
                .Append(FormatterUtility<ValueStringFormatter>.Format(value).ToString())
                .ToString();
        }
    }

    public sealed class BasedOnUserDataCryptocurrencyFormatter : IFormatter<string>
    {
        public object Format(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            UserData userData = new BinaryUserDataSaveSystem().Read();

            return new StringBuilder()
                .Append(content)
                .Append(userData.Currency)
                .ToString();
        }
    }

    [Obsolete("use " + nameof(BasedOnUserDataCurrencyFormatter))]
    public sealed class CurrencyFormatter : IFormatter<decimal>
    {
        public object Format(decimal value)
        {
            return new StringBuilder()
                .Append(CurrencySymbol.EUR)
                .Append(FormatterUtility<ValueStringFormatter>.Format(value).ToString())
                .ToString();
        }
    }

    [Obsolete("use " + nameof(BasedOnUserDataCryptocurrencyFormatter))]
    public sealed class CryptocurrencyFormatter : IFormatter<string>
    {
        public object Format(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            return new StringBuilder()
                .Append(content)
                .Append(CurrencyName.EUR)
                .ToString();
        }
    }
}
