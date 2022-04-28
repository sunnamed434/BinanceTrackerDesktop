using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item.Control;
using System;
using System.Drawing;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Extension
{
    public static class MenuStripItemComponentControlExtension
    {
        public static void SetImage(this MenuStripComponentItemControl source, Image to)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (to == null)
                throw new ArgumentNullException(nameof(to));

            source.ToolStrip.Image = to;
        }

        public static void Enable(this MenuStripComponentItemControl source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.ToolStrip.Enabled = true;
        }

        public static void Disable(this MenuStripComponentItemControl source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.ToolStrip.Enabled = false;
        }
    }
}
