using Binance.Net;
using Binance.Net.Objects.Other;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API;
using BinanceTrackerDesktop.Core.Forms.Tray.API;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.UI.Menu
{
    public class BinanceTrackerMenuUIControl : MenuStripControlBase
    {
        private readonly MenuStrip menuStrip;

        private readonly BinanceClient client;

        private readonly MenuStripItemControl permissionsItemControl;



        public BinanceTrackerMenuUIControl(MenuStrip menuStrip, BinanceClient client)
        {
            if (menuStrip == null)
                throw new ArgumentNullException(nameof(menuStrip));

            this.menuStrip = menuStrip;
            this.menuStrip.RenderMode = ToolStripRenderMode.Professional;
            this.client = client;

            foreach (MenuStripItemControl item in InitializeItems())
                AddComponent(item);

            permissionsItemControl = base.GetComponentAt(MenuItemsIdContainer.API);
            permissionsItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onPermissionsClicked;
        }

        ~BinanceTrackerMenuUIControl()
        {
            permissionsItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onPermissionsClicked;
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



        private async void onPermissionsClicked(EventArgs e)
        {
            WebCallResult<BinanceAPIKeyPermissions> result = await client.General.GetAPIKeyPermissionsAsync();
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



        protected override IEnumerable<MenuStripItemControl> InitializeItems()
        {
            return new List<MenuStripItemControl>
            {
                new MenuStripItemControl(MenuItemsTextContainer.API, MenuItemsIdContainer.API),
            };
        }
    }

    public class MenuItemsTextContainer
    {
        public static readonly string API = nameof(API);
    }

    public class MenuItemsIdContainer
    {
        public const byte API = 1;
    }
}
