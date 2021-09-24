using Binance.Net;
using Binance.Net.Objects.Other;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API;
using BinanceTrackerDesktop.Core.Forms.Tray.API;
using BinanceTrackerDesktop.Core.Wallet;
using BinanceTrackerDesktop.Core.Wallet.API;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.UI.Menu
{
    public class BinanceTrackerMenuStripControlUI : MenuStripControlBase
    {
        private readonly MenuStrip menuStrip;

        private readonly BinanceClient client;

        private readonly BinanceUserWallet wallet;

        private readonly MenuStripItemControl apiItemControl;

        private readonly MenuStripItemControl coinsItemControl;



        public BinanceTrackerMenuStripControlUI(MenuStrip menuStrip, BinanceClient client, BinanceUserWallet wallet)
        {
            if (menuStrip == null)
                throw new ArgumentNullException(nameof(menuStrip));

            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            this.menuStrip = menuStrip;
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
                $"{nameof(api.CreateTime)} = {api.CreateTime}, " +
                $"{nameof(api.IpRestrict)} = {api.IpRestrict}, " +
                $"{nameof(api.EnableFutures)} = {api.EnableFutures}, " +
                $"{nameof(api.EnableWithdrawals)} = {api.EnableWithdrawals}, " +
                $"{nameof(api.EnableMargin)} = {api.EnableMargin}, " +
                $"{nameof(api.EnableVanillaOptions)} = {api.EnableVanillaOptions}, " +
                $"{nameof(api.EnableSpotAndMarginTrading)} = {api.EnableSpotAndMarginTrading}, " +
                $"{nameof(api.EnableInternalTransfer)} = {api.EnableInternalTransfer}, " +
                $"{nameof(api.EnableReading)} = {api.EnableReading}, " +
                $"{nameof(api.PermitUniversalTransfer)} = {api.PermitUniversalTransfer}, " +
                $"{nameof(api.TradingAuthorityExpirationTime)} = {api.TradingAuthorityExpirationTime}",

                TrayItemsTextContainer.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information
            );
        }

        private async void onCoinsItemClicked(EventArgs e)
        {
            IEnumerable<BinanceUserWalletCoinResult> result = await wallet.GetAllBuyedCoinsAsync();

            MessageBox.Show(string.Join(", ", result.Select(r => $"{r.Asset } = {r.Price}")));
        }



        protected override IEnumerable<MenuStripItemControl> InitializeItems()
        {
            return new List<MenuStripItemControl>
            {
                new MenuStripItemControl(MenuItemsTextContainer.API, MenuItemsIdContainer.API),
                new MenuStripItemControl(MenuItemsTextContainer.Coins, MenuItemsIdContainer.Coins),
            };
        }
    }

    public class MenuItemsTextContainer
    {
        public static readonly string API = nameof(API);

        public static readonly string Coins = nameof(Coins);
    }

    public class MenuItemsIdContainer
    {
        public const byte API = 1;

        public const byte Coins = 2;
    }
}
