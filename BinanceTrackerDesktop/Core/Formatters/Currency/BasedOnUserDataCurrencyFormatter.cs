using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.Formatters.ValueString;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Save;
using System.Text;

namespace BinanceTrackerDesktop.Core.Formatters.Currency
{
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
}
