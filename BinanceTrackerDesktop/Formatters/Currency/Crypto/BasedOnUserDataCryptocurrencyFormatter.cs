using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using System.Text;

namespace BinanceTrackerDesktop.Formatters.Currency.Crypto;

public sealed class BasedOnUserDataCryptocurrencyFormatter : IFormatter<string>
{
    public object Format(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException(nameof(content));
        }

        UserData userData = new BinaryUserDataSaveSystem().Read();
        return new StringBuilder()
            .Append(content)
            .Append(userData.Currency)
            .ToString();
    }
}
