using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API;
using System;
using System.Drawing;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Extension
{
    public static class MenuStripExtension
    {
        public static void SetImage(this StripItemControl source, Image to)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (to == null)
                throw new ArgumentNullException(nameof(to));

            source.ToolStrip.Image = to;
        }
    }
}
