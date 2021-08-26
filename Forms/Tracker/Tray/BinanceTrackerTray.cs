using BinanceTrackerDesktop.Forms.Tracker.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.Tray
{
    public class BinanceTrackerTray
    {
        private readonly IFormControl formControl;

        private readonly IFormEventListener trayDoubleClickEventListener;

        private readonly TrayApplicationOpenClickEventListener applicationOpenEventClickEventListener;

        private readonly TrayDisableNotificationsClickEventListener disableNotificationsClickEventListener;

        private readonly TrayApplicationQuitClickEventListener applicationQuitClickEventListener;



        public BinanceTrackerTray(IFormControl formControl, IFormEventListener trayDoubleClickEventListener, TrayApplicationOpenClickEventListener applicationOpenEventClickEventListener, TrayDisableNotificationsClickEventListener disableNotificationsClickEventListener, TrayApplicationQuitClickEventListener applicationQuitClickEventListener)
        {
            if (formControl == null)
                throw new ArgumentNullException(nameof(formControl));

            if (applicationOpenEventClickEventListener == null)
                throw new ArgumentNullException(nameof(applicationOpenEventClickEventListener));

            if (disableNotificationsClickEventListener == null)
                throw new ArgumentNullException(nameof(disableNotificationsClickEventListener));

            if (applicationQuitClickEventListener == null)
                throw new ArgumentNullException(nameof(applicationQuitClickEventListener));

            this.formControl = formControl;
            this.trayDoubleClickEventListener = trayDoubleClickEventListener;
            this.applicationOpenEventClickEventListener = applicationOpenEventClickEventListener;
            this.disableNotificationsClickEventListener = disableNotificationsClickEventListener;
            this.applicationQuitClickEventListener = applicationQuitClickEventListener;

            this.trayDoubleClickEventListener.OnTriggerEventHandler += onTrayDoubleClicked;
            this.applicationOpenEventClickEventListener.OnTriggerEventHandler += onApplicationOpenClicked;
            this.disableNotificationsClickEventListener.OnTriggerEventHandler += onDisableNotificationsClicked;
            this.applicationQuitClickEventListener.OnTriggerEventHandler += onApplicationQuitClicked;
        }

        ~BinanceTrackerTray()
        {
            this.trayDoubleClickEventListener.OnTriggerEventHandler -= onTrayDoubleClicked;
            this.applicationOpenEventClickEventListener.OnTriggerEventHandler -= onApplicationOpenClicked;
            this.disableNotificationsClickEventListener.OnTriggerEventHandler -= onDisableNotificationsClicked;
            this.applicationQuitClickEventListener.OnTriggerEventHandler -= onApplicationQuitClicked;
        }



        private void onTrayDoubleClicked(object sender, EventArgs e)
        {
            formControl.Show();
        }

        private void onApplicationOpenClicked(object sender, EventArgs e)
        {
            formControl.Show();
        }

        private void onDisableNotificationsClicked(object sender, EventArgs e)
        {
            MessageBox.Show(nameof(onDisableNotificationsClicked));
        }

        private void onApplicationQuitClicked(object sender, EventArgs e)
        {
            this.formControl.Close();
        }
    }
}
