using BinanceTrackerDesktop.Core.Popup.API;
using System;

namespace BinanceTrackerDesktop.Core.Popup.Extension
{
    public static class PopupExtension
    {
        public static void Show(this Popup.API.Popup source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            NotificationsControl.Show(source);
        }
    }
}
