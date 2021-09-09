using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Window.API;
using BinanceTrackerDesktop.Forms.API;
using BinanceTrackerDesktop.Forms.SystemTray.API;
using BinanceTrackerDesktop.Forms.SystemTray.Tray.Data;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.SystemTray.Tray
{
    public class BinanceTrackerTray
    {
        private readonly IFormSafelyComponentControl formSafelyCloseControl;

        private readonly IFormSystemTrayControl systemTrayFormControl;

        private readonly BinanceTrackerSystemTrayForm systemTrayForm;

        private readonly BinanceTrackerNotificationsControl notificationsControl;

        private readonly IFormTrayControl formTrayControl;



        public BinanceTrackerTray(IFormSafelyComponentControl formSafelyCloseControl, IFormSystemTrayControl systemTrayFormControl, BinanceTrackerSystemTrayForm systemTrayForm, BinanceTrackerNotificationsControl notificationsControl, IFormTrayControl formTrayControl)
        {
            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            if (systemTrayFormControl == null)
                throw new ArgumentNullException(nameof(systemTrayFormControl));

            if (systemTrayForm == null)
                throw new ArgumentNullException(nameof(systemTrayForm));

            if (notificationsControl == null)
                throw new ArgumentNullException(nameof(notificationsControl));

            if (formTrayControl == null)
                throw new ArgumentNullException(nameof(formTrayControl));

            this.formSafelyCloseControl = formSafelyCloseControl;
            this.systemTrayFormControl = systemTrayFormControl;
            this.systemTrayForm = systemTrayForm;
            this.notificationsControl = notificationsControl;
            this.formTrayControl = formTrayControl;

            this.formSafelyCloseControl.RegisterListener(onCloseCallbackAsync);
            this.formTrayControl.EventsContainerControl.ClickListener.OnTriggerEventHandler += onTrayClicked;
            this.formTrayControl.EventsContainerControl.DoubleClickListener.OnTriggerEventHandler += onTrayDoubleClicked;
            this.formTrayControl.Items[0].ClickEvent.OnTriggerEventHandler += onApplicationOpenClicked;
            this.formTrayControl.Items[1].ClickEvent.OnTriggerEventHandler += onDisableNotificationsClicked;
            this.formTrayControl.Items[2].ClickEvent.OnTriggerEventHandler += onApplicationQuitClicked;
        }

        

        private void setWindowToForegound()
        {
            new BinanceProcessWindow().SetWindowToForeground();
        }



        private void onTrayClicked(object sender, EventArgs e)
        {
            MessageBox.Show(nameof(onTrayClicked));

            this.formTrayControl.SystemTrayFormControl.Show();
        }

        private void onTrayDoubleClicked(object sender, EventArgs e)
        {
            setWindowToForegound();
        }

        private void onApplicationOpenClicked(object sender, EventArgs e)
        {
            setWindowToForegound();
        }

        private async void onDisableNotificationsClicked(object sender, EventArgs e)
        {
            BinanceUserData binanceUserData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            binanceUserData.NotificationsEnabled = !binanceUserData.NotificationsEnabled;

            await new BinanceUserDataWriter().WriteDataAsync(binanceUserData);

            this.formTrayControl.Items[2].SetHeader(binanceUserData.NotificationsEnabled == true ? TrayDataContainer.DisableNotifications : TrayDataContainer.EnableNotifications);
            this.notificationsControl.Show(TrayDataContainer.ApplicationName, binanceUserData.NotificationsEnabled == true ? TrayDataContainer.NotificationsEnabled : TrayDataContainer.NotificationsDisabled);
        }

        private async void onApplicationQuitClicked(object sender, EventArgs e)
        {
            await this.systemTrayFormControl.CloseAsync();
            await this.formSafelyCloseControl.CallListenersAsync();
        }

        private async Task onCloseCallbackAsync()
        {
            this.formTrayControl.EventsContainerControl.DoubleClickListener.OnTriggerEventHandler -= onTrayDoubleClicked;
            this.formTrayControl.Items[0].ClickEvent.OnTriggerEventHandler -= onApplicationOpenClicked;
            this.formTrayControl.Items[1].ClickEvent.OnTriggerEventHandler -= onDisableNotificationsClicked;
            this.formTrayControl.Items[2].ClickEvent.OnTriggerEventHandler -= onApplicationQuitClicked;

            await Task.CompletedTask;
        }
    }
}
