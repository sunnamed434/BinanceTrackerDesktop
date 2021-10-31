using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using BinanceTrackerDesktop.Core.Components.TextControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl
{
    public interface IMenuStripItem
    {
        string Header { get; }

        Image Image { get; }

        byte Id { get; }
    }

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

    public sealed class MenuStripComponentItemEventsContainer
    {
        public readonly EventListener OnClick;



        public MenuStripComponentItemEventsContainer()
        {
            OnClick = new EventListener();
        }
    }

    public class MenuStripComponentControlBase : TextComponentControl, IExpandableComponent<MenuStripComponentItemControl, byte>
    {
        public readonly ContextMenuStrip Strip;

        public IReadOnlyCollection<MenuStripComponentItemControl> AllComponents => Components;



        protected List<MenuStripComponentItemControl> Components = new List<MenuStripComponentItemControl>();



        public MenuStripComponentControlBase(ContextMenuStrip strip) : base(strip)
        {
            if (strip == null)
                throw new ArgumentNullException(nameof(strip));

            Strip = strip;
        }

        public MenuStripComponentControlBase()
        {

        }



        public virtual void AddComponent(MenuStripComponentItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Strip.Items.Add(item.ToolStrip);
            Components.Add(item);
        }

        public void AddComponents(IEnumerable<MenuStripComponentItemControl> items)
        {
            if (!items.Any())
                throw new InvalidOperationException();

            foreach (MenuStripComponentItemControl item in items)
                AddComponent(item);
        }

        public virtual void RemoveComponent(MenuStripComponentItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Strip.Items.Remove(item.ToolStrip);
            Components.Remove(item);
        }

        public MenuStripComponentItemControl GetComponentAt(byte id)
        {
            return Components.FirstOrDefault(c => c.Id == id);
        }



        protected virtual IEnumerable<MenuStripComponentItemControl> InitializeItems()
        {
            return new List<MenuStripComponentItemControl>();
        }
    }
}
