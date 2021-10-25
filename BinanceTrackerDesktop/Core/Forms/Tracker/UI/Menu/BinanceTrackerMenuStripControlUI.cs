using Binance.Net;
using Binance.Net.Objects.Other;
using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API;
using BinanceTrackerDesktop.Core.Wallet;
using BinanceTrackerDesktop.Core.Wallet.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.UI.Menu
{
    public sealed class BinanceTrackerMenuStripControlUI : MenuStripControlBase
    {
        private readonly MenuStrip menuStrip;

        private readonly BinanceClient client;

        private readonly BinanceUserWallet wallet;

        private readonly MenuStripItemControl apiItemControl;

        private readonly MenuStripItemControl coinsItemControl;



        public BinanceTrackerMenuStripControlUI(MenuStrip self, BinanceClient client, BinanceUserWallet wallet)
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

            foreach (MenuStripItemControl item in InitializeItems())
                AddComponent(item);

            apiItemControl = base.GetComponentAt(MenuItemsIdContainer.API);
            coinsItemControl = base.GetComponentAt(MenuItemsIdContainer.Coins);

            apiItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onAPIItemClicked;
            coinsItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onCoinsItemClicked;
        }

        ~BinanceTrackerMenuStripControlUI()
        {
            apiItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onAPIItemClicked;
            coinsItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onCoinsItemClicked;
        }



        public override void AddComponent(MenuStripItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.menuStrip.Items.Add(item.ToolStrip);
            base.Components.Add(item);
        }

        public override void RemoveComponent(MenuStripItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.menuStrip.Items.Remove(item.ToolStrip);
            base.Components.Remove(item);
        }



        private async void onAPIItemClicked(EventArgs e)
        {
            WebCallResult<BinanceAPIKeyPermissions> result = await this.client.General.GetAPIKeyPermissionsAsync();
            BinanceAPIKeyPermissions api = result.Data;

            MessageBox.Show
            (
                new StringBuilder()
                    .Append($"{nameof(api.IpRestrict)} = {api.IpRestrict}, ")
                    .Append($"{nameof(api.EnableFutures)} = {api.EnableFutures}, ")
                    .Append($"{nameof(api.EnableWithdrawals)} = {api.EnableWithdrawals}, ")
                    .Append($"{nameof(api.EnableMargin)} = {api.EnableMargin}, ")
                    .Append($"{nameof(api.EnableVanillaOptions)} = {api.EnableVanillaOptions}, ")
                    .Append($"{nameof(api.EnableSpotAndMarginTrading)} = {api.EnableSpotAndMarginTrading}, ")
                    .Append($"{nameof(api.EnableInternalTransfer)} = {api.EnableInternalTransfer}, ")
                    .Append($"{nameof(api.EnableReading)} = {api.EnableReading}, ")
                    .Append($"{nameof(api.PermitUniversalTransfer)} = {api.PermitUniversalTransfer}, ")
                    .Append($"{nameof(api.TradingAuthorityExpirationTime)} = {api.TradingAuthorityExpirationTime}")
                    .ToString(),

                ApplicationEnviroment.GlobalName, MessageBoxButtons.OK, MessageBoxIcon.Information
            );
        }

        private async void onCoinsItemClicked(EventArgs e)
        {
            IEnumerable<BinanceUserWalletCoinResult> result = await wallet.GetAllBuyedCoinsAsync();

            MessageBox.Show(string.Join(", ", result.Select(r => $"{r.Asset } = {r.Price}")));
        }



        protected override IEnumerable<MenuStripItemControl> InitializeItems()
        {
            yield return new MenuStripItemControl(MenuItemsTextContainer.API, MenuItemsIdContainer.API);
            yield return new MenuStripItemControl(MenuItemsTextContainer.Coins, MenuItemsIdContainer.Coins);
        }
    }

    public sealed class MenuItemsTextContainer
    {
        public static readonly string API = nameof(API);

        public static readonly string Coins = nameof(Coins);
    }

    public sealed class MenuItemsIdContainer
    {
        public const byte API = 1;

        public const byte Coins = 2;
    }
}
