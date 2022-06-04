using BinanceTrackerDesktop.Formatters.Currency;
using BinanceTrackerDesktop.Formatters.Utilities;
using BinanceTrackerDesktop.Localizations.Data;
using BinanceTrackerDesktop.User.Client;
using BinanceTrackerDesktop.User.Wallet.Results.Coin;
using BinanceTrackerDesktop.Views.Tracker.Menu.Base;

namespace BinanceTrackerDesktop.Views.Tracker.Menu.Items;

public sealed class TrackerMenuCoins : TrackerMenuBase
{
    public async override void OnClick()
    {
        IEnumerable<IUserWalletCoinResult> result = await UserClient.Wallet.GetAllBuyedCoinsAsync();
        MessageBox.Show(string.Join("\n", result.Select(r => $"{r.Asset} = {FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(r.Price)}")));
    }



    protected override ToolStripMenuItem InitializeToolStripMenuItem()
    {
        return new ToolStripMenuItem(LocalizationData.Read().Coins);
    }
}
