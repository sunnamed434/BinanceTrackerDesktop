using BinanceTrackerDesktop.Forms.Tracker.API;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.MoveListener
{
    public class BinanceTrackerMoveListener
    {
        private readonly IFormControl formControl;

        private readonly IFormEventListener formEventListener;

        private readonly BinanceTrackerNotificationsControl notificationsControl;



        public BinanceTrackerMoveListener(IFormControl formControl, IFormEventListener formEventListener, BinanceTrackerNotificationsControl notificationsControl)
        {
            if (formControl == null)
                throw new ArgumentNullException(nameof(formControl));

            if (formEventListener == null)
                throw new ArgumentNullException(nameof(formEventListener));

            this.formControl = formControl;
            this.formEventListener = formEventListener;
            this.notificationsControl = notificationsControl;

            this.formEventListener.OnTriggerEventHandler += onFormMoved;
        }

        ~BinanceTrackerMoveListener()
        {
            this.formEventListener.OnTriggerEventHandler -= onFormMoved;
        }



        private void onFormMoved(object sender, EventArgs e)
        {
            if (this.formControl.WindowState == FormWindowState.Minimized)
            {
                this.formControl.Hide();
                this.notificationsControl.Show("Binance Tracker Desktop", "Application hiden!");
            }
        }
    }
}
