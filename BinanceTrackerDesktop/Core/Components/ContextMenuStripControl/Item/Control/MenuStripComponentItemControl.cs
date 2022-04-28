using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item.EventsContainer;
using BinanceTrackerDesktop.Core.Components.TextControl;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item.Control
{
    public sealed class MenuStripComponentItemControl : TextComponentControl, IMenuStripItem
    {
        public readonly MenuStripComponentItemEventsContainer EventsContainer;

        public readonly ToolStripMenuItem ToolStrip;



        public string Header { get; }

        public Image Image { get; }

        public byte Id { get; }



        public MenuStripComponentItemControl(string header, Image image, byte id)
        {
            if (string.IsNullOrEmpty(header))
                throw new ArgumentNullException(nameof(header));

            Header = header;
            Image = image ?? default;
            Id = id;

            EventsContainer = new MenuStripComponentItemEventsContainer();
            ToolStrip = new ToolStripMenuItem(header, image, (s, e) => EventsContainer.OnClick.TriggerEvent(e));
        }

        public MenuStripComponentItemControl(string header, byte id) : this(header, null, id)
        {

        }



        public override void SetText(string content, Color? color = null)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            ToolStrip.Text = content;
            SetTextColor(color);
        }

        public override void SetTextColor(Color? color)
        {
            if (color != null)
                ToolStrip.ForeColor = (Color)color;
        }
    }

}
