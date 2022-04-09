﻿using BinanceTrackerDesktop.Core.Notification.API;
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
            notifyIcon.BalloonTipClicked += onPopupClicked;
            notifyIcon.BalloonTipClosed += onPopupClosed;
        }

        ~NotificationsControl()
        {
            notifyIcon.BalloonTipShown -= onPopupShown;
            notifyIcon.BalloonTipClicked -= onPopupClicked;
            notifyIcon.BalloonTipClosed -= onPopupClosed;
        }



        public static void Show(Popup popup, bool sendAnyway = false)
        {
            if (popup == null)
                throw new ArgumentNullException(nameof(popup));

            notifyIcon.Icon = popup.Icon;
            lastUsedPopup = popup;

            if (sendAnyway)
                notifyIcon.ShowBalloonTip(popup.Timeout, popup.Title, popup.Message, ToolTipIcon.None);

            else if (new BinaryUserDataSaveSystem().Read().NotificationsEnabled ?? default(bool) == true)
                notifyIcon.ShowBalloonTip(popup.Timeout, popup.Title, popup.Message, ToolTipIcon.None);
        }



        private static void onPopupShown(object sender, EventArgs e)
        {
            lastUsedPopup?.OnShow?.Invoke();
        }

        private static void onPopupClicked(object sender, EventArgs e)
        {
            lastUsedPopup?.OnClick?.Invoke();
            onPopupClosed(sender, e);
        }

        private static void onPopupClosed(object sender, EventArgs e)
        {
            lastUsedPopup?.OnClose?.Invoke();
            lastUsedPopup = null;
        }
    }
}
