using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl;
using BinanceTrackerDesktop.Core.Components.TrayControl;
using BinanceTrackerDesktop.Core.Components.TrayControl.Extension;
using BinanceTrackerDesktop.Core.Notification.Builder;
using BinanceTrackerDesktop.Core.User.Client;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using BinanceTrackerDesktop.Core.Window;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Tray
{
    public sealed class BinanceTrackerTray : TrayComponentControlBase
    {
        private readonly ISafelyComponentControl formSafelyCloseControl;

        private readonly ProcessWindowHelper processWindowHelper;

        private readonly MenuStripComponentItemControl applicationOpenItemControl;

        private readonly MenuStripComponentItemControl notificationsItemControl;

        private readonly MenuStripComponentItemControl applicationQuitItemControl;



        public BinanceTrackerTray(NotifyIcon notifyIcon, ISafelyComponentControl formSafelyCloseControl) : base(notifyIcon)
        {
            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            this.formSafelyCloseControl = formSafelyCloseControl;
            processWindowHelper = new ProcessWindowHelper();

            applicationOpenItemControl = base.GetComponentAt(TrayItemsIdContainer.OpenApplicationUniqueIndex);
            notificationsItemControl = base.GetComponentAt(TrayItemsIdContainer.NotificationsUniqueIndex);
            applicationQuitItemControl = base.GetComponentAt(TrayItemsIdContainer.QuitApplicationUniqueIndex);

            initializeAsync();

            this.formSafelyCloseControl.RegisterListener(onCloseCallbackAsync);
            applicationOpenItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onApplicationOpenItemClicked;
            notificationsItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onNotificationsItemControlClicked;
            applicationQuitItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onApplicationQuitItemClicked;
            EventsContainerControl.DoubleClickListener.OnTriggerEventHandler += onTrayDoubleClick;
        }



        private async void initializeAsync()
        {
            UserData binanceUserData = new BinaryUserDataSaveSystem().Read();
            notificationsItemControl.SetText(getNotificationsText(binanceUserData.NotificationsEnabled ?? default(bool)));

            UserWalletCoinResult coinResult = await new UserClient().Wallet.GetBestCoinAsync();

            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Tracker Running")
                .WillCloseIn(90)
                .WithOnClickAction(() => new ProcessWindowHelper().SetWindowToForeground())
                .WithOnCloseAction(() => new PopupBuilder()
                                              .WithTitle(ApplicationEnviroment.GlobalName)
                                              .WithMessage("Your best for today: " + coinResult.Asset)
                                              .WillCloseIn(90)
                                              .Build(false))
                .Build(false);

            
        }

        private string getNotificationsText(bool isNotificationsEnabled)
        {
            return isNotificationsEnabled == true ? TrayItemsTextContainer.DisableNotifications : TrayItemsTextContainer.EnableNotifications;
        }



        private void onTrayDoubleClick(EventArgs e)
        {
            processWindowHelper.SetWindowToForeground();
        }

        private void onApplicationOpenItemClicked(EventArgs e)
        {
            onTrayDoubleClick(e);
        }

        private void onNotificationsItemControlClicked(EventArgs e)
        {
            UserData userData = new BinaryUserDataSaveSystem().Read();
            userData.NotificationsEnabled = userData.NotificationsEnabled == true ? false : true;
            userData.SaveUserData();

            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage(userData.NotificationsEnabled == true ? TrayItemsTextContainer.NotificationsEnabled : TrayItemsTextContainer.NotificationsDisabled)
                .WillCloseIn(90)
                .Build(true);

            notificationsItemControl.SetText(getNotificationsText(userData.NotificationsEnabled ?? default));
        }

        private async void onApplicationQuitItemClicked(EventArgs e)
        {
            this.HideTray();

            await this.formSafelyCloseControl.CallListenersAsync();
        }

        private async Task onCloseCallbackAsync()
        {
            applicationOpenItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onApplicationOpenItemClicked;
            notificationsItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onNotificationsItemControlClicked;
            applicationQuitItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onApplicationQuitItemClicked;
            EventsContainerControl.DoubleClickListener.OnTriggerEventHandler -= onTrayDoubleClick;

            this.HideTray();

            await Task.CompletedTask;
        }



        protected override IEnumerable<MenuStripComponentItemControl> InitializeItems()
        {
            yield return new MenuStripComponentItemControl(TrayItemsTextContainer.OpenApplication, TrayItemsIdContainer.OpenApplicationUniqueIndex);
            yield return new MenuStripComponentItemControl(TrayItemsTextContainer.DisableNotifications, TrayItemsIdContainer.NotificationsUniqueIndex);
            yield return new MenuStripComponentItemControl(TrayItemsTextContainer.QuitApplication, TrayItemsIdContainer.QuitApplicationUniqueIndex);
        }
    }

    public sealed class TrayItemsTextContainer
    {
        public const string OpenApplication = "Open Binance Tracker";

        public const string QuitApplication = "Quit Binance Tracker";

        public const string NotificationsEnabled = "Notifications Enabled";

        public const string NotificationsDisabled = "Notifications Disabled";

        public const string EnableNotifications = "Enable Notifications";

        public const string DisableNotifications = "Disable Notifications";
    }

    public sealed class TrayItemsIdContainer
    {
        public const byte OpenApplicationUniqueIndex = 1;

        public const byte NotificationsUniqueIndex = 2;

        public const byte QuitApplicationUniqueIndex = 3;
    }
}
