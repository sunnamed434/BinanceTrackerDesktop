using BinanceTrackerDesktop.Core.Components.TextControl;
using System;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item
{
    public interface IMenuStripItem
    {
        string Header { get; }

        Image Image { get; }

        byte Id { get; }
    }
}
