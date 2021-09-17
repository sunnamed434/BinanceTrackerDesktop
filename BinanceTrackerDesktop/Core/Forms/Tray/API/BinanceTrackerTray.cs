using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.TrayControl.API;
using BinanceTrackerDesktop.Core.Notifications.API;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.Window.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Tray.API
{
    public class BinanceTrackerTray : TrayBase
    {
        private readonly NotificationsControl notificationsControl;

        private readonly ISafelyComponentControl formSafelyCloseControl;

        private readonly BinanceProcessWindow processWindow;

        private readonly TrayItemControl applicationOpenItemControl;

        private readonly TrayItemControl notificationsItemControl;

        private readonly TrayItemControl applicationQuitItemControl;



        public BinanceTrackerTray(NotifyIcon notifyIcon, ISafelyComponentControl formSafelyCloseControl, NotificationsControl notificationsControl) : base(notifyIcon)
        {
            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            if (notificationsControl == null)
                throw new ArgumentNullException(nameof(notificationsControl));

            this.notificationsControl = notificationsControl;
            this.formSafelyCloseControl = formSafelyCloseControl;
            processWindow = new BinanceProcessWindow();

            applicationOpenItemControl = base.GetComponentAt(TrayItemsUniqueValueContainer.OpenApplicationUniqueIndex);
            notificationsItemControl = base.GetComponentAt(TrayItemsUniqueValueContainer.NotificationsUniqueIndex);
            applicationQuitItemControl = base.GetComponentAt(TrayItemsUniqueValueContainer.QuitApplicationUniqueIndex);

            initializeAsync();

            this.formSafelyCloseControl.RegisterListener(onCloseCallbackAsync);
            applicationOpenItemControl.EventsContainer.ClickEvent.OnTriggerEventHandler += onApplicationOpenItemClicked;
            notificationsItemControl.EventsContainer.ClickEvent.OnTriggerEventHandler += onNotificationsItemControlClicked;
            applicationQuitItemControl.EventsContainer.ClickEvent.OnTriggerEventHandler += onApplicationQuitItemClicked;
            EventsContainerControl.DoubleClickListener.OnTriggerEventHandler += onTrayDoubleClick;
        }



        private async void initializeAsync()
        {
            UserData binanceUserData = await new UserDataReader().ReadDataAsync();
            notificationsItemControl.SetText(binanceUserData.NotificationsEnabled == true ? TrayItemTextContainer.DisableNotifications : TrayItemTextContainer.EnableNotifications);
        }

        private string getNotificationsText(bool isNotificationsEnabled)
        {
            return isNotificationsEnabled == true ? TrayItemTextContainer.DisableNotifications : TrayItemTextContainer.EnableNotifications;
        }



        protected override IEnumerable<TrayItemControl> InitializeItems()
        {
            return new List<TrayItemControl>()
            {
                new TrayItemControl(TrayItemTextContainer.OpenApplication, TrayItemsUniqueValueContainer.OpenApplicationUniqueIndex),
                new TrayItemControl(TrayItemTextContainer.DisableNotifications, TrayItemsUniqueValueContainer.NotificationsUniqueIndex),
                new TrayItemControl(TrayItemTextContainer.QuitApplication, TrayItemsUniqueValueContainer.QuitApplicationUniqueIndex),
            };
        }



        private void onTrayDoubleClick(EventArgs e)
        {
            processWindow.SetWindowToForeground();
        }

        private void onApplicationOpenItemClicked(EventArgs e)
        {
            onTrayDoubleClick(e);
        }

        private async void onNotificationsItemControlClicked(EventArgs e)
        {
            UserData userData = await new UserDataReader().ReadDataAsync();
            userData.NotificationsEnabled = !userData.NotificationsEnabled;

            await new UserDataWriter().WriteDataAsync(userData);

            notificationsItemControl.SetText(getNotificationsText(userData.NotificationsEnabled));
            this.notificationsControl.ShowPopup(TrayItemTextContainer.ApplicationName, userData.NotificationsEnabled ? TrayItemTextContainer.NotificationsEnabled : TrayItemTextContainer.NotificationsDisabled);
        }

        private async void onApplicationQuitItemClicked(EventArgs e)
        {
            base.Close();

            await this.formSafelyCloseControl.CallListenersAsync();
        }

        private async Task onCloseCallbackAsync()
        {
            applicationOpenItemControl.EventsContainer.ClickEvent.OnTriggerEventHandler -= onApplicationOpenItemClicked;
            notificationsItemControl.EventsContainer.ClickEvent.OnTriggerEventHandler -= onNotificationsItemControlClicked;
            applicationQuitItemControl.EventsContainer.ClickEvent.OnTriggerEventHandler -= onApplicationQuitItemClicked;
            EventsContainerControl.DoubleClickListener.OnTriggerEventHandler -= onTrayDoubleClick;

            base.Close();

            await Task.CompletedTask;
        }
    }

    public class TrayItemTextContainer
    {
        public const string ApplicationName = "Binance Tracker Desktop";

        public const string OpenApplication = "Open Binance Tracker";

        public const string QuitApplication = "Quit Binance Tracker";

        public const string NotificationsEnabled = "Notifications Enabled";

        public const string NotificationsDisabled = "Notifications Disabled";

        public const string EnableNotifications = "Enable Notifications";

        public const string DisableNotifications = "Disable Notifications";
    }

    public class TrayItemsUniqueValueContainer
    {
        public const byte OpenApplicationUniqueIndex = 1;

        public const byte NotificationsUniqueIndex = 2;

        public const byte QuitApplicationUniqueIndex = 3;
    }
}
