using BinanceTrackerDesktop.Forms.Tracker.Notifications.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.Notifications
{
    public class BinanceTrackerNotificationsControl
    {
        private readonly INotificationsControl notificationsControl;



        public BinanceTrackerNotificationsControl(INotificationsControl notificationsControl)
        {
            if (notificationsControl == null)
                throw new ArgumentNullException(nameof(notificationsControl));

            this.notificationsControl = notificationsControl;
        }



        public void Show(string title, string content, ToolTipIcon icon = ToolTipIcon.Info)
        {
            this.notificationsControl.Show(title, content, icon);
        }
    }
}
