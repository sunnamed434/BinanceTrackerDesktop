using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.Tracker.API;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using System;

namespace BinanceTrackerDesktop.Forms.SystemTray.Tray
{
    public class BinanceTrackerTray
    {
        private readonly IFormControl formControl;

        private readonly BinanceTrackerSystemTrayForm systemTrayForm;

        private readonly BinanceTrackerNotificationsControl notificationsControl;

        private readonly IFormEventListener[] formEventListeners;



        public BinanceTrackerTray(IFormControl formControl, BinanceTrackerSystemTrayForm systemTrayForm, BinanceTrackerNotificationsControl notificationsControl, params IFormEventListener[] formEventListeners)
        {
            if (formControl == null)
                throw new ArgumentNullException(nameof(formControl));

            if (systemTrayForm == null)
                throw new ArgumentNullException(nameof(systemTrayForm));

            if (notificationsControl == null)
                throw new ArgumentNullException(nameof(notificationsControl));

            if (formEventListeners == null)
                throw new ArgumentNullException(nameof(formEventListeners));

            if (formEventListeners.Length < 0)
                throw new InvalidOperationException();

            this.formControl = formControl;
            this.systemTrayForm = systemTrayForm;
            this.notificationsControl = notificationsControl;
            this.formEventListeners = formEventListeners;

            this.formEventListeners[0].OnTriggerEventHandler += onTrayDoubleClicked;
            this.formEventListeners[1].OnTriggerEventHandler += onApplicationOpenClicked;
            this.formEventListeners[2].OnTriggerEventHandler += onDisableNotificationsClicked;
            this.formEventListeners[3].OnTriggerEventHandler += onApplicationQuitClicked;
        }

        ~BinanceTrackerTray()
        {
            this.formEventListeners[0].OnTriggerEventHandler -= onTrayDoubleClicked;
            this.formEventListeners[1].OnTriggerEventHandler -= onApplicationOpenClicked;
            this.formEventListeners[2].OnTriggerEventHandler -= onDisableNotificationsClicked;
            this.formEventListeners[3].OnTriggerEventHandler -= onApplicationQuitClicked;
        }



        private void onTrayDoubleClicked(object sender, EventArgs e)
        {
            this.formControl.Show();
        }

        private void onApplicationOpenClicked(object sender, EventArgs e)
        {
            this.formControl.Show();
        }

        private async void onDisableNotificationsClicked(object sender, EventArgs e)
        {
            BinanceUserData binanceUserData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            binanceUserData.NotificationsEnabled = !binanceUserData.NotificationsEnabled;

            await new BinanceUserDataWriter().WriteDataAsync(binanceUserData);

            this.systemTrayForm.ChangeMenuItemTitle(1, binanceUserData.NotificationsEnabled == true ? TrayDataContainer.DisableNotifications : TrayDataContainer.EnableNotifications);
            this.notificationsControl.Show(TrayDataContainer.ApplicationName, binanceUserData.NotificationsEnabled == true ? TrayDataContainer.NotificationsEnabled : TrayDataContainer.NotificationsDisabled);
        }

        private void onApplicationQuitClicked(object sender, EventArgs e)
        {
            this.formControl.Close();
        }
    }
}
