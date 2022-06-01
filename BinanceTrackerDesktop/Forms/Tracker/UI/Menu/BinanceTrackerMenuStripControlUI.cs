using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.Expandables;
using BinanceTrackerDesktop.Formatters.Currency;
using BinanceTrackerDesktop.Formatters.Utilities;
using BinanceTrackerDesktop.Forms.Tracker.Settings;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Themes.Forms;
using BinanceTrackerDesktop.Themes.Recognizers.Windows;
using BinanceTrackerDesktop.User.Wallet;
using BinanceTrackerDesktop.User.Wallet.Results.Coin;
using CryptoExchange.Net.Objects;
using System.Text;

namespace BinanceTrackerDesktop.Forms.Tracker.UI.Menu;

public sealed class BinanceTrackerMenuStripControlUI : IInitializableExpandable<ToolStripMenuItem, byte>
{
    private readonly MenuStrip menuStrip;

    private readonly BinanceClient client;

    private readonly UserWallet wallet;

    private readonly MenuStripExpandableDesignable expandable;

    private readonly ToolStripMenuItem apiToolStripMenuItem;

    private readonly ToolStripMenuItem coinsToolStripMenuItem;

    private readonly ToolStripMenuItem settingsToolStripMenuItem;



    public BinanceTrackerMenuStripControlUI(MenuStrip menuStrip, BinanceClient client, UserWallet wallet)
    {
        if (menuStrip == null)
        {
            throw new ArgumentNullException(nameof(menuStrip));
        }

        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        if (wallet == null)
        {
            throw new ArgumentNullException(nameof(wallet));
        }

        this.menuStrip = menuStrip;
        this.menuStrip.RenderMode = ToolStripRenderMode.Professional;
        this.client = client;
        this.wallet = wallet;
        expandable = new MenuStripExpandableDesignable(menuStrip);
        expandable.AddComponents(this);

        apiToolStripMenuItem = expandable.GetComponentOrDefault(MenuItemsIdContainer.API);
        coinsToolStripMenuItem = expandable.GetComponentOrDefault(MenuItemsIdContainer.Coins);
        settingsToolStripMenuItem = expandable.GetComponentOrDefault(MenuItemsIdContainer.Settings);

        FormsTheme.Apply(menuStrip, expandable.AllComponents, new WindowsSystemThemeRecognizer());

        apiToolStripMenuItem.Click += onAPIItemClicked;
        coinsToolStripMenuItem.Click += onCoinsItemClicked;
        settingsToolStripMenuItem.Click += onSettingsItemClicked;
    }



    private async void onAPIItemClicked(object sender, EventArgs e)
    {
        WebCallResult<BinanceAPIKeyPermissions> result = await client.SpotApi.Account.GetAPIKeyPermissionsAsync();
        BinanceAPIKeyPermissions permissions = result.Data;

        new PopupBuilder()
            .WithTitle(ApplicationEnviroment.GlobalName)
            .WithMessage(new StringBuilder()
                             .Append($"{nameof(permissions.IpRestrict)} = {permissions.IpRestrict}, ")
                             .Append($"{nameof(permissions.EnableFutures)} = {permissions.EnableFutures}, ")
                             .Append($"{nameof(permissions.EnableWithdrawals)} = {permissions.EnableWithdrawals}, ")
                             .Append($"{nameof(permissions.EnableMargin)} = {permissions.EnableMargin}, ")
                             .Append($"{nameof(permissions.EnableVanillaOptions)} = {permissions.EnableVanillaOptions}, ")
                             .Append($"{nameof(permissions.EnableSpotAndMarginTrading)} = {permissions.EnableSpotAndMarginTrading}, ")
                             .Append($"{nameof(permissions.EnableInternalTransfer)} = {permissions.EnableInternalTransfer}, ")
                             .Append($"{nameof(permissions.EnableReading)} = {permissions.EnableReading}, ")
                             .Append($"{nameof(permissions.PermitsUniversalTransfer)} = {permissions.PermitsUniversalTransfer}, ")
                             .Append($"{nameof(permissions.TradingAuthorityExpirationTime)} = {permissions.TradingAuthorityExpirationTime}"))
            .BuildAsMessageBox();
    }

    private async void onCoinsItemClicked(object sender, EventArgs e)
    {
        IEnumerable<IUserWalletCoinResult> result = await wallet.GetAllBuyedCoinsAsync();

        MessageBox.Show(string.Join("\n", result.Select(r => $"{r.Asset} = {FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(r.Price)}")));
    }

    private void onSettingsItemClicked(object sender, EventArgs e)
    {
        TrackerSettingsFormView settingsView = new TrackerSettingsFormView(wallet);
        new SettingsController(settingsView, wallet);
        new TrackerSettingsFormView(wallet).ShowDialog();
    }



    IEnumerable<KeyValuePair<byte, ToolStripMenuItem>> IInitializableExpandable<ToolStripMenuItem, byte>.InitializeItems()
    {
        yield return new KeyValuePair<byte, ToolStripMenuItem>(MenuItemsIdContainer.API, new ToolStripMenuItem(MenuItemsTextContainer.API));
        yield return new KeyValuePair<byte, ToolStripMenuItem>(MenuItemsIdContainer.Coins, new ToolStripMenuItem(MenuItemsTextContainer.Coins));
        yield return new KeyValuePair<byte, ToolStripMenuItem>(MenuItemsIdContainer.Settings, new ToolStripMenuItem(MenuItemsTextContainer.Settings));
    }
}

public sealed class MenuItemsTextContainer
{
    public const string API = nameof(API);

    public const string Coins = nameof(Coins);

    public const string Settings = nameof(Settings);
}

public sealed class MenuItemsIdContainer
{
    public const byte API = 1;

    public const byte Coins = 2;

    public const byte Settings = 3;
}
