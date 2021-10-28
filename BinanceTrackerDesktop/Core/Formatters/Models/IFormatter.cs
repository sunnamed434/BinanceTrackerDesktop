using BinanceTrackerDesktop.Core.Currencies;
using System;
using System.Text;

namespace BinanceTrackerDesktop.Core.Formatters.Models
{
    public interface IFormatter<TArgument>
    {
        object Format(TArgument value);
    }

    public sealed class CurrencyFormatter : IFormatter<decimal>
    {
        private const string DefaultFormat = "0.00";



        public object Format(decimal value)
        {
            return new StringBuilder()
                .Append(CurrencySymbol.EUR)
                .Append(value.ToString(DefaultFormat))
                .ToString();
        }
    }

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
