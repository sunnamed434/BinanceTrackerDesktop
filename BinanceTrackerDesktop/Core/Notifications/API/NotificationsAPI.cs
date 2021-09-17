using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Notifications.API
{
    public class NotificationsControl
    {
        private readonly NotifyIcon notifyIcon;



        private const int DefaultTimeout = 90;



        public NotificationsControl(NotifyIcon notifyIcon)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            this.notifyIcon = notifyIcon;
        }



        public void ShowPopup(string title, string content, ToolTipIcon icon = ToolTipIcon.Info)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            this.notifyIcon.ShowBalloonTip(DefaultTimeout, title, content, icon);
        }
    }
}
