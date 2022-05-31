using BinanceTrackerDesktop.Formatters.Utilities;
using BinanceTrackerDesktop.Formatters.ValueString;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using System.Text;

namespace BinanceTrackerDesktop.Formatters.Currency;

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
