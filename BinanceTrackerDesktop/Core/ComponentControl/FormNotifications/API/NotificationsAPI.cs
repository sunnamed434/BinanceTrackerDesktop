using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.FormNotifications.API
{
    public interface IFormNotificationsControl
    {
        NotifyIcon NotifyIcon { get; }



        void Show(string title, string content, ToolTipIcon icon);
    }

    public abstract class FormNotificationsControlBase : IFormNotificationsControl
    {
        public NotifyIcon NotifyIcon { get; }



        public const int DefaultTimeout = 90;



        public FormNotificationsControlBase(NotifyIcon notifyIcon)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            NotifyIcon = notifyIcon;
        }



        public abstract void Show(string title, string content, ToolTipIcon icon);
    }

    public class FormStableNotificationsControl : FormNotificationsControlBase
    {
        public FormStableNotificationsControl(NotifyIcon notifyIcon) : base(notifyIcon)
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

    public class FormNotificationsControl
    {
        private readonly IFormNotificationsControl notificationsControl;



        public FormNotificationsControl(IFormNotificationsControl notificationsControl)
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
