using BinanceTrackerDesktop.Core.Components.TrayControl.API;
using System;

namespace BinanceTrackerDesktop.Core.Components.TrayControl.Extension
{
    public static class TrayControlExtension
    {
        public static void ShowTray(this TrayControlBase source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.NotifyIcon.ContextMenuStrip.Show();
        }

        public static void HideTray(this TrayControlBase source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            using (source.NotifyIcon)
                source.NotifyIcon.Visible = false;
        }
    }
}
