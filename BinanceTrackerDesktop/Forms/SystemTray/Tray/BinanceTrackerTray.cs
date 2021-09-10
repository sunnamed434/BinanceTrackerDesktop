using BinanceTrackerDesktop.Core.Control.FormTray.API;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Window.API;
using BinanceTrackerDesktop.Forms.API;
using BinanceTrackerDesktop.Forms.SystemTray.Tray.Data;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using System;
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

        private readonly IFormTrayItemControl openApplicationItemControl;

        private readonly IFormTrayItemControl notificationsItemControl;

        private readonly IFormTrayItemControl quitApplicationItemControl;



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
            openApplicationItemControl = this.formTrayControl.GetComponentAt(TrayItemsUniquesDataContainer.OpenApplicationUniqueIndex);
            notificationsItemControl = this.formTrayControl.GetComponentAt(TrayItemsUniquesDataContainer.NotificationsUniqueIndex);
            quitApplicationItemControl = this.formTrayControl.GetComponentAt(TrayItemsUniquesDataContainer.QuitApplicationUniqueIndex);

            this.formSafelyCloseControl.RegisterListener(onCloseCallbackAsync);
            this.formTrayControl.EventsContainerControl.MouseClick.OnTriggerEventHandler += onTrayClicked;
            this.formTrayControl.EventsContainerControl.DoubleClickListener.OnTriggerEventHandler += onTrayDoubleClicked;
            openApplicationItemControl.ClickEvent.OnTriggerEventHandler += onTrayOpenApplicationClicked;
            notificationsItemControl.ClickEvent.OnTriggerEventHandler += onTrayNotificationsClicked;
            quitApplicationItemControl.ClickEvent.OnTriggerEventHandler += onTrayQuitApplicationClicked;
        }

        

        private void setWindowToForegound()
        {
            new BinanceProcessWindow().SetWindowToForeground();
        }



        private void onTrayClicked(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this.formTrayControl.SystemTrayFormControl.Show();
        }

        private void onTrayDoubleClicked(EventArgs e)
        {
            setWindowToForegound();
        }

        private void onTrayOpenApplicationClicked(EventArgs e)
        {
            setWindowToForegound();
        }

        private async void onTrayNotificationsClicked(EventArgs e)
        {
            BinanceUserData binanceUserData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            binanceUserData.NotificationsEnabled = !binanceUserData.NotificationsEnabled;

            await new BinanceUserDataWriter().WriteDataAsync(binanceUserData);

            this.formTrayControl.GetComponentAt(TrayItemsUniquesDataContainer.NotificationsUniqueIndex).SetHeader(binanceUserData.NotificationsEnabled == true ? TrayItemsDataContainer.DisableNotifications : TrayItemsDataContainer.EnableNotifications);
            this.notificationsControl.Show(TrayItemsDataContainer.ApplicationName, binanceUserData.NotificationsEnabled == true ? TrayItemsDataContainer.NotificationsEnabled : TrayItemsDataContainer.NotificationsDisabled);
        }

        private async void onTrayQuitApplicationClicked(EventArgs e)
        {
            await this.systemTrayFormControl.CloseAsync();
            await this.formSafelyCloseControl.CallListenersAsync();
        }

        private async Task onCloseCallbackAsync()
        {
            this.formTrayControl.EventsContainerControl.DoubleClickListener.OnTriggerEventHandler -= onTrayDoubleClicked;
            this.formTrayControl.GetComponentAt(TrayItemsUniquesDataContainer.OpenApplicationUniqueIndex).ClickEvent.OnTriggerEventHandler -= onTrayOpenApplicationClicked;
            this.formTrayControl.GetComponentAt(TrayItemsUniquesDataContainer.NotificationsUniqueIndex).ClickEvent.OnTriggerEventHandler -= onTrayNotificationsClicked;
            this.formTrayControl.GetComponentAt(TrayItemsUniquesDataContainer.QuitApplicationUniqueIndex).ClickEvent.OnTriggerEventHandler -= onTrayQuitApplicationClicked;

            await Task.CompletedTask;
        }
    }
}
