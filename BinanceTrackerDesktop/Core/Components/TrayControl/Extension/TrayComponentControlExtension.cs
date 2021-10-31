using System;

namespace BinanceTrackerDesktop.Core.Components.TrayControl.Extension
{
    public static class TrayComponentControlExtension
    {
        public static void ShowTray(this TrayComponentControlBase source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.NotifyIcon.ContextMenuStrip.Show();
        }

        public static void HideTray(this TrayComponentControlBase source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            using (source.NotifyIcon)
                source.NotifyIcon.Visible = false;
        }
    }
}
