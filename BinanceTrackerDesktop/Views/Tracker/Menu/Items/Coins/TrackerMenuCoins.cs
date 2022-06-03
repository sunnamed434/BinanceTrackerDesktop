using BinanceTrackerDesktop.Formatters.Currency;
using BinanceTrackerDesktop.Formatters.Utilities;
using BinanceTrackerDesktop.User.Client;
using BinanceTrackerDesktop.User.Wallet.Results.Coin;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

namespace BinanceTrackerDesktop.Views.Tracker.Menu.Items.Coins;

public sealed class TrackerMenuCoins : TrackerMenuBase
{
    public override string Label => "Coins";

    public override Image Image => null;

    public override ToolStripItem[] DropDownItems => null;
    


    public async override void OnClick()
    {
        IEnumerable<IUserWalletCoinResult> result = await UserClient.Wallet.GetAllBuyedCoinsAsync();

        MessageBox.Show(string.Join("\n", result.Select(r => $"{r.Asset} = {FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(r.Price)}")));
    }
}
