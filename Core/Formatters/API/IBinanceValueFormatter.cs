using BinanceTrackerDesktop.Core.Currencies;

namespace BinanceTrackerDesktop.Core.Formatters.API
{
    public interface IBinanceValueFormatter<Result, Argument>
    {
        Result Format(Argument value);
    }

    public class BinanceCurrencyValueFormatter : IBinanceValueFormatter<string, decimal>
    {
        public string Format(decimal value) => CurrencySymbol.EUR + value.ToString(StringFormats.Default);
    }

    public class BinanceCryptocurrencyStringFormatter : IBinanceValueFormatter<string, string>
    {
        public string Format(string value) => value + CurrencyName.EUR;
    }
}
