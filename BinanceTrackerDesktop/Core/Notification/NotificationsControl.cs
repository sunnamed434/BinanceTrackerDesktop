using BinanceTrackerDesktop.Core.Notification.API;
using BinanceTrackerDesktop.Core.User.Data.Save;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Notification
{
    public sealed class NotificationsControl
    {
        private static NotifyIcon notifyIcon;

        private static Popup lastUsedPopup;



        public static void Initialize(NotifyIcon notifyIcon)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            NotificationsControl.notifyIcon = notifyIcon;

            notifyIcon.BalloonTipShown += onPopupShown;
            notifyIcon.BalloonTipClosed += onPopupClosed;
            notifyIcon.BalloonTipClicked += onPopupClicked;
        }

        ~NotificationsControl()
        {
            notifyIcon.BalloonTipClicked -= onPopupClicked;
            notifyIcon.BalloonTipClosed -= onPopupClosed;
            notifyIcon.BalloonTipShown -= onPopupShown;
        }



        public static void Show(Popup popup, bool sendAnyway = false)
        {
            if (popup == null)
                throw new ArgumentNullException(nameof(popup));

            lastUsedPopup = popup;

            if (sendAnyway)
                notifyIcon.ShowBalloonTip(popup.Timeout, popup.Title, popup.Message, popup.Icon);

            else if (new BinaryUserDataSaveSystem().Read().NotificationsEnabled ?? default(bool) == true)
                notifyIcon.ShowBalloonTip(popup.Timeout, popup.Title, popup.Message, popup.Icon);
        }



        private static void onPopupClicked(object sender, EventArgs e)
        {
            lastUsedPopup?.OnClick?.Invoke();
        }

        private static void onPopupShown(object sender, EventArgs e)
        {
            lastUsedPopup?.OnShow?.Invoke();
        }

        private static void onPopupClosed(object sender, EventArgs e)
        {
            lastUsedPopup?.OnClose?.Invoke();
            lastUsedPopup = null;
        }
    }
}
