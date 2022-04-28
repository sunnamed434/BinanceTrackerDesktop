using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Base;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item.Control;
using BinanceTrackerDesktop.Core.Formatters.Currency;
using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.Forms.Settings;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.UI.Menu
{
    public sealed class BinanceTrackerMenuStripControlUI : MenuStripComponentControlBase
    {
        private readonly MenuStrip menuStrip;

        private readonly BinanceClient client;

        private readonly UserWallet wallet;

        private readonly MenuStripComponentItemControl apiItemControl;

        private readonly MenuStripComponentItemControl coinsItemControl;

        private readonly MenuStripComponentItemControl settingsItemControl;



        public BinanceTrackerMenuStripControlUI(MenuStrip self, BinanceClient client, UserWallet wallet)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));

            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            this.menuStrip = self;
            this.menuStrip.RenderMode = ToolStripRenderMode.Professional;
            this.client = client;
            this.wallet = wallet;

            foreach (MenuStripComponentItemControl item in InitializeItems())
                AddComponent(item);

            apiItemControl = base.GetComponentAt(MenuItemsIdContainer.API);
            coinsItemControl = base.GetComponentAt(MenuItemsIdContainer.Coins);
            settingsItemControl = base.GetComponentAt(MenuItemsIdContainer.Settings);

            apiItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onAPIItemClicked;
            coinsItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onCoinsItemClicked;
            settingsItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onSettingsItemClicked;
        }

        

        ~BinanceTrackerMenuStripControlUI()
        {
            apiItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onAPIItemClicked;
            coinsItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onCoinsItemClicked;
            settingsItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onSettingsItemClicked;
        }



        public override void AddComponent(MenuStripComponentItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.menuStrip.Items.Add(item.ToolStrip);
            base.Components.Add(item);
        }

        public override void RemoveComponent(MenuStripComponentItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.menuStrip.Items.Remove(item.ToolStrip);
            base.Components.Remove(item);
        }



        private async void onAPIItemClicked(EventArgs e)
        {
            WebCallResult<BinanceAPIKeyPermissions> result = await this.client.SpotApi.Account.GetAPIKeyPermissionsAsync();
            BinanceAPIKeyPermissions permissions = result.Data;

            MessageBox.Show
            (
                new StringBuilder()
                    .Append($"{nameof(permissions.IpRestrict)} = {permissions.IpRestrict}, ")
                    .Append($"{nameof(permissions.EnableFutures)} = {permissions.EnableFutures}, ")
                    .Append($"{nameof(permissions.EnableWithdrawals)} = {permissions.EnableWithdrawals}, ")
                    .Append($"{nameof(permissions.EnableMargin)} = {permissions.EnableMargin}, ")
                    .Append($"{nameof(permissions.EnableVanillaOptions)} = {permissions.EnableVanillaOptions}, ")
                    .Append($"{nameof(permissions.EnableSpotAndMarginTrading)} = {permissions.EnableSpotAndMarginTrading}, ")
                    .Append($"{nameof(permissions.EnableInternalTransfer)} = {permissions.EnableInternalTransfer}, ")
                    .Append($"{nameof(permissions.EnableReading)} = {permissions.EnableReading}, ")
                    .Append($"{nameof(permissions.PermitsUniversalTransfer)} = {permissions.PermitsUniversalTransfer}, ")
                    .Append($"{nameof(permissions.TradingAuthorityExpirationTime)} = {permissions.TradingAuthorityExpirationTime}")
                    .ToString(),

                ApplicationEnviroment.GlobalName, MessageBoxButtons.OK, MessageBoxIcon.Information
            );
        }

        private async void onCoinsItemClicked(EventArgs e)
        {
            IEnumerable<IBinanceUserWalletCoinResult> result = await wallet.GetAllBuyedCoinsAsync();

            MessageBox.Show(string.Join("\n", result.Select(r => $"{r.Asset } = {FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(r.Price)}")));
        }

        private void onSettingsItemClicked(EventArgs e)
        {
            new BinanceTrackerSettingsForm(wallet).ShowDialog();
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
        public static readonly string API = nameof(API);

        public static readonly string Coins = nameof(Coins);

        public static readonly string Settings = nameof(Settings);
    }

    public sealed class MenuItemsIdContainer
    {
        public const byte API = 1;

        public const byte Coins = 2;

        public const byte Settings = 3;
    }
}
