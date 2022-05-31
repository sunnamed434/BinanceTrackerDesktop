using BinanceTrackerDesktop.Components.ContextMenuStripControl.Item.Control;

namespace BinanceTrackerDesktop.Components.ContextMenuStripControl.Extension;

public static class MenuStripItemComponentControlExtension
{
    public static void SetImage(this MenuStripComponentItemControl source, Image to)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (to == null)
        {
            throw new ArgumentNullException(nameof(to));
        }

        source.ToolStripItem.Image = to;
    }

    public static void Enable(this MenuStripComponentItemControl source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        source.ToolStripItem.Enabled = true;
    }

    public static void Disable(this MenuStripComponentItemControl source)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        source.ToolStripItem.Enabled = false;
    }
}
