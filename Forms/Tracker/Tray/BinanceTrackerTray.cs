using BinanceTrackerDesktop.Forms.Tracker.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.Tray
{
    public class BinanceTrackerTray
    {
        private readonly IFormControl formControl;

        private readonly IFormEventListener[] formEventListeners;



        public BinanceTrackerTray(IFormControl formControl, params IFormEventListener[] formEventListeners)
        {
            if (formControl == null)
                throw new ArgumentNullException(nameof(formControl));

            if (formEventListeners == null)
                throw new ArgumentNullException(nameof(formEventListeners));

            if (formEventListeners.Length < 0)
                throw new InvalidOperationException();

            this.formControl = formControl;
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
