using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API;
using System;
using System.Drawing;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Extension
{
    public static class MenuStripItemControlExtension
    {
        public static void SetImage(this MenuStripItemControl source, Image to)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (to == null)
                throw new ArgumentNullException(nameof(to));

            source.ToolStrip.Image = to;
        }

        public static void Enable(this MenuStripItemControl source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.ToolStrip.Enabled = true;
        }

        public static void Disable(this MenuStripItemControl source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.ToolStrip.Enabled = false;
        }
    }
}
