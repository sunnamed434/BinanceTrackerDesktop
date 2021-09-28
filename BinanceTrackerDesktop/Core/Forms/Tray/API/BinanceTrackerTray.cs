using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API;
using BinanceTrackerDesktop.Core.Components.TrayControl.API;
using BinanceTrackerDesktop.Core.Popup.API;
using BinanceTrackerDesktop.Core.Popup.Extension;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.Window.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Tray.API
{
    public class BinanceTrackerTray : TrayControlBase
    {
        private readonly ISafelyComponentControl formSafelyCloseControl;

        private readonly ProcessWindowHelper processWindow;

        private readonly MenuStripItemControl applicationOpenItemControl;

        private readonly MenuStripItemControl notificationsItemControl;

        private readonly MenuStripItemControl applicationQuitItemControl;



        public BinanceTrackerTray(NotifyIcon notifyIcon, ISafelyComponentControl formSafelyCloseControl) : base(notifyIcon)
        {
            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            this.formSafelyCloseControl = formSafelyCloseControl;
            processWindow = new ProcessWindowHelper();

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



        private void initializeAsync()
        {
            UserData binanceUserData = new BinaryUserDataSaveReadSystem().Read();
            notificationsItemControl.SetText(binanceUserData.NotificationsEnabled == true ? TrayItemsTextContainer.DisableNotifications : TrayItemsTextContainer.EnableNotifications);
        }

        private string getNotificationsText(bool isNotificationsEnabled)
        {
            return isNotificationsEnabled == true ? TrayItemsTextContainer.DisableNotifications : TrayItemsTextContainer.EnableNotifications;
        }



        private void onTrayDoubleClick(EventArgs e)
        {
            processWindow.SetWindowToForeground();
        }

        private void onApplicationOpenItemClicked(EventArgs e)
        {
            onTrayDoubleClick(e);
        }

        private void onNotificationsItemControlClicked(EventArgs e)
        {
            UserData userData = new BinaryUserDataSaveReadSystem().Read();
            userData.NotificationsEnabled = !userData.NotificationsEnabled;

            userData.SaveUserData();

            notificationsItemControl.SetText(getNotificationsText(userData.NotificationsEnabled));

            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage(userData.NotificationsEnabled ? TrayItemsTextContainer.NotificationsEnabled : TrayItemsTextContainer.NotificationsDisabled)
                .WillCloseIn(90)
                .Build()
                .Show();
        }

        private async void onApplicationQuitItemClicked(EventArgs e)
        {
            base.Close();

            await this.formSafelyCloseControl.CallListenersAsync();
        }

        private async Task onCloseCallbackAsync()
        {
            applicationOpenItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onApplicationOpenItemClicked;
            notificationsItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onNotificationsItemControlClicked;
            applicationQuitItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onApplicationQuitItemClicked;
            EventsContainerControl.DoubleClickListener.OnTriggerEventHandler -= onTrayDoubleClick;

            base.Close();

            await Task.CompletedTask;
        }



        protected override IEnumerable<MenuStripItemControl> InitializeItems()
        {
            return new List<MenuStripItemControl>()
            {
                new MenuStripItemControl(TrayItemsTextContainer.OpenApplication, TrayItemsIdContainer.OpenApplicationUniqueIndex),
                new MenuStripItemControl(TrayItemsTextContainer.DisableNotifications, TrayItemsIdContainer.NotificationsUniqueIndex),
                new MenuStripItemControl(TrayItemsTextContainer.QuitApplication, TrayItemsIdContainer.QuitApplicationUniqueIndex),
            };
        }
    }

    public class TrayItemsTextContainer
    {
        public const string OpenApplication = "Open Binance Tracker";

        public const string QuitApplication = "Quit Binance Tracker";

        public const string NotificationsEnabled = "Notifications Enabled";

        public const string NotificationsDisabled = "Notifications Disabled";

        public const string EnableNotifications = "Enable Notifications";

        public const string DisableNotifications = "Disable Notifications";
    }

    public class TrayItemsIdContainer
    {
        public const byte OpenApplicationUniqueIndex = 1;

        public const byte NotificationsUniqueIndex = 2;

        public const byte QuitApplicationUniqueIndex = 3;
    }
}
