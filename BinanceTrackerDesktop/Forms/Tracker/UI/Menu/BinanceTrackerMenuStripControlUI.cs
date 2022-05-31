using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Components.ContextMenuStripControl.Base;
using BinanceTrackerDesktop.Components.ContextMenuStripControl.Item.Control;
using BinanceTrackerDesktop.Controllers;
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

public sealed class BinanceTrackerMenuStripControlUI : MenuStripComponentControlBase
{
    private readonly MenuStrip menuStrip;

    private readonly BinanceClient client;

    private readonly UserWallet wallet;

    private readonly MenuStripComponentItemControl apiItemControl;

    private readonly MenuStripComponentItemControl coinsItemControl;

    private readonly MenuStripComponentItemControl settingsItemControl;



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

        foreach (MenuStripComponentItemControl item in InitializeItems())
        {
            AddComponent(item);
        }

        apiItemControl = GetComponentAt(MenuItemsIdContainer.API);
        coinsItemControl = GetComponentAt(MenuItemsIdContainer.Coins);
        settingsItemControl = GetComponentAt(MenuItemsIdContainer.Settings);

        FormsTheme.Apply(menuStrip, AllComponents, new WindowsSystemThemeRecognizer());

        apiItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onAPIItemClicked;
        coinsItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onCoinsItemClicked;
        settingsItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onSettingsItemClicked;
    }



    public override void AddComponent(MenuStripComponentItemControl menuStripItem)
    {
        if (menuStripItem == null)
        {
            throw new ArgumentNullException(nameof(menuStripItem));
        }

        menuStrip.Items.Add(menuStripItem.ToolStripItem);
        Components.Add(menuStripItem);
    }

    public override void RemoveComponent(MenuStripComponentItemControl menuStripItem)
    {
        if (menuStripItem == null)
        {
            throw new ArgumentNullException(nameof(menuStripItem));
        }

        menuStrip.Items.Remove(menuStripItem.ToolStripItem);
        Components.Remove(menuStripItem);
    }



    private async void onAPIItemClicked(EventArgs e)
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

    private async void onCoinsItemClicked(EventArgs e)
    {
        IEnumerable<IUserWalletCoinResult> result = await wallet.GetAllBuyedCoinsAsync();

        MessageBox.Show(string.Join("\n", result.Select(r => $"{r.Asset} = {FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(r.Price)}")));
    }

    private void onSettingsItemClicked(EventArgs e)
    {
        TrackerSettingsFormView settingsView = new TrackerSettingsFormView(wallet);
        new SettingsController(settingsView, wallet);
        new TrackerSettingsFormView(wallet).ShowDialog();
    }



    protected override IEnumerable<MenuStripComponentItemControl> InitializeItems()
    {
        yield return new MenuStripComponentItemControl(MenuItemsTextContainer.API, MenuItemsIdContainer.API);
        yield return new MenuStripComponentItemControl(MenuItemsTextContainer.Coins, MenuItemsIdContainer.Coins);
        yield return new MenuStripComponentItemControl(MenuItemsTextContainer.Settings, MenuItemsIdContainer.Settings);
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
