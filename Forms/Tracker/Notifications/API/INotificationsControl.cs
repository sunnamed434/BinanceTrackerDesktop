using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.Notifications.API
{
    public interface INotificationsControl
    {
        NotifyIcon NotifyIcon { get; }



        void Show(string title, string content, ToolTipIcon icon);
    }

    public abstract class NotificationsControlBase : INotificationsControl
    {
        public NotifyIcon NotifyIcon { get; }



        public const int DefaultTimeout = 90;



        public NotificationsControlBase(NotifyIcon notifyIcon)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            NotifyIcon = notifyIcon;
        }



        public abstract void Show(string title, string content, ToolTipIcon icon);
    }

    public class NotificationsControl : NotificationsControlBase
    {
        public NotificationsControl(NotifyIcon notifyIcon) : base(notifyIcon)
        {

        }



        public override void Show(string title, string content, ToolTipIcon icon = ToolTipIcon.Info)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));

            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            NotifyIcon.ShowBalloonTip(DefaultTimeout, title, content, icon);
        }
    }
}
