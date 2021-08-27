using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.Tracker.API;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.MoveListener
{
    public class BinanceTrackerMoveListener
    {
        private readonly IFormControl formControl;

        private readonly BinanceTrackerNotificationsControl notificationsControl;



        public BinanceTrackerMoveListener(IFormControl formControl, BinanceTrackerNotificationsControl notificationsControl)
        {
            if (formControl == null)
                throw new ArgumentNullException(nameof(formControl));

            if (notificationsControl == null)
                throw new ArgumentNullException(nameof(notificationsControl));

            this.formControl = formControl;
            this.notificationsControl = notificationsControl;

            this.formControl.Move += onFormMove;
        }

        ~BinanceTrackerMoveListener()
        {
            this.formControl.Move -= onFormMove;
        }



        private async void onFormMove(object sender, EventArgs e)
        {
            if (this.formControl.WindowState == FormWindowState.Minimized)
            {
                BinanceUserData binanceUserData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;

                this.formControl.Hide();

                if (binanceUserData.NotificationsEnabled)
                {
                    this.notificationsControl.Show("Binance Tracker Desktop", "Application hiden!");
                }
            }
        }
    }
}
