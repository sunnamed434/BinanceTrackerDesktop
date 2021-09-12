using BinanceTrackerDesktop.Core.ComponentControl.FormTray.API;
using BinanceTrackerDesktop.Core.Forms.API;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Window.API;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Tray.API
{
    public class BinanceTrackerTray : FormTrayBase
    {
        private readonly BinanceTrackerNotificationsControl notificationsControl;

        private readonly IFormSafelyComponentControl formSafelyCloseControl;

        private readonly BinanceProcessWindow processWindow;

        private readonly IFormTrayItemControl applicationOpenItemControl;

        private readonly IFormTrayItemControl notificationsItemControl;

        private readonly IFormTrayItemControl applicationQuitItemControl;



        public BinanceTrackerTray(NotifyIcon notifyIcon, IFormSafelyComponentControl formSafelyCloseControl, BinanceTrackerNotificationsControl notificationsControl) : base(notifyIcon)
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
            BinanceUserData binanceUserData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            notificationsItemControl.SetText(binanceUserData.NotificationsEnabled == true ? TrayItemTextContainer.DisableNotifications : TrayItemTextContainer.EnableNotifications);
        }

        private string getNotificationsText(bool isNotificationsEnabled)
        {
            return isNotificationsEnabled == true ? TrayItemTextContainer.DisableNotifications : TrayItemTextContainer.EnableNotifications;
        }



        protected override IEnumerable<IFormTrayItemControl> InitializeItems()
        {
            return new List<IFormTrayItemControl>()
            {
                new FormTrayItemControl(TrayItemTextContainer.OpenApplication, TrayItemsUniqueValueContainer.OpenApplicationUniqueIndex),
                new FormTrayItemControl(TrayItemTextContainer.DisableNotifications, TrayItemsUniqueValueContainer.NotificationsUniqueIndex),
                new FormTrayItemControl(TrayItemTextContainer.QuitApplication, TrayItemsUniqueValueContainer.QuitApplicationUniqueIndex),
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
            BinanceUserData binanceUserData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            binanceUserData.NotificationsEnabled = !binanceUserData.NotificationsEnabled;

            await new BinanceUserDataWriter().WriteDataAsync(binanceUserData);

            notificationsItemControl.SetText(getNotificationsText(binanceUserData.NotificationsEnabled));
            this.notificationsControl.Show(TrayItemTextContainer.ApplicationName, binanceUserData.NotificationsEnabled ? TrayItemTextContainer.NotificationsEnabled : TrayItemTextContainer.NotificationsDisabled);
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
