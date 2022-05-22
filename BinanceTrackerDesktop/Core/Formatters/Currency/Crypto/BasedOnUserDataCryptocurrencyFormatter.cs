using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using System.Text;

namespace BinanceTrackerDesktop.Core.Formatters.Currency.Crypto
{
    public sealed class BasedOnUserDataCryptocurrencyFormatter : IFormatter<string>
    {
        public object Format(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));

            UserData userData = new BinaryUserDataSaveSystem().Read();
            return new StringBuilder()
                .Append(content)
                .Append(userData.Currency)
                .ToString();
        }
    }
}
