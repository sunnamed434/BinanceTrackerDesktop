using BinanceTrackerDesktop.Core.Notification.API;
using System;

namespace BinanceTrackerDesktop.Core.Notification.Extension
{
    public static class PopupExtension
    {
        public static void Show(this Popup source, bool sendAnyway = false)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            NotificationsControl.Show(source, sendAnyway);
        }
    }
}
