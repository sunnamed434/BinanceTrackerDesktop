using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item.EventsContainer;
using BinanceTrackerDesktop.Core.Components.TextControl;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item.Control
{
    public sealed class MenuStripComponentItemControl : TextComponentControl, IMenuStripItem
    {
        public readonly MenuStripComponentItemEventsContainer EventsContainer;

        public readonly ToolStripMenuItem ToolStripItem;



        public MenuStripComponentItemControl(string header, Image image, byte id)
        {
            if (string.IsNullOrWhiteSpace(header))
                throw new ArgumentException(nameof(header));

            Header = header;
            Image = image ?? default;
            Id = id;

            EventsContainer = new MenuStripComponentItemEventsContainer();
            ToolStripItem = new ToolStripMenuItem(header, image, (s, e) => EventsContainer.OnClick.TriggerEvent(e));
        }

        public MenuStripComponentItemControl(string header, byte id) : this(header, null, id)
        {

        }



        public string Header { get; }

        public Image Image { get; }

        public byte Id { get; }



        public override void SetText(string content, Color? color = null)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException(nameof(content));

            ToolStripItem.Text = content;
            if (color != null) 
                SetForegroundColor(color);
        }

        public override void SetBackgroundColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            ToolStripItem.BackColor = color.Value;
        }

        public override void SetForegroundColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            ToolStripItem.ForeColor = color.Value;
        }

        public override void SetDefaultTextColorAndAsCurrentBackgroundColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            SetDefaultTextColor(color);
            SetBackgroundColor(color);
        }

        public override void SetDefaultTextColorAndAsCurrentForegroundColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            SetDefaultTextColor(color);
            SetForegroundColor(color);
        }
    }
}
