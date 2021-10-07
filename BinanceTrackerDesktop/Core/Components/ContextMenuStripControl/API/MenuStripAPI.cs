using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API
{
    public interface IMenuStripItem
    {
        string Header { get; }

        Image Image { get; }

        byte Id { get; }
    }

    public class MenuStripItemControl : TextComponentControl, IMenuStripItem
    {
        public readonly MenuStripItemEventsContainer EventsContainer;

        public readonly ToolStripMenuItem ToolStrip;



        public string Header { get; }

        public Image Image { get; }

        public byte Id { get; }



        public MenuStripItemControl(string header, Image image, byte id)
        {
            if (string.IsNullOrEmpty(header))
                throw new ArgumentNullException(nameof(header));

            Header = header;
            Image = image ?? default;
            Id = id;

            EventsContainer = new MenuStripItemEventsContainer();
            ToolStrip = new ToolStripMenuItem(header, image, (s, e) => EventsContainer.OnClick.TriggerEvent(e));
        }

        public MenuStripItemControl(string header, byte id) : this(header, null, id)
        {

        }



        public override void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            ToolStrip.Text = content;
        }

        public override void SetTextColor(Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            ToolStrip.ForeColor = color;
        }
    }

    public class MenuStripItemEventsContainer
    {
        public readonly EventListener OnClick;



        public MenuStripItemEventsContainer()
        {
            OnClick = new EventListener();
        }
    }

    public class MenuStripControlBase : TextComponentControl, IExpandableComponent<MenuStripItemControl, byte>
    {
        public readonly ContextMenuStrip Strip;

        public IReadOnlyCollection<MenuStripItemControl> AllComponents => Components;



        protected List<MenuStripItemControl> Components = new List<MenuStripItemControl>();



        public MenuStripControlBase(ContextMenuStrip strip) : base(strip)
        {
            if (strip == null)
                throw new ArgumentNullException(nameof(strip));

            Strip = strip;
        }

        public MenuStripControlBase()
        {

        }



        public virtual void AddComponent(MenuStripItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Strip.Items.Add(item.ToolStrip);
            Components.Add(item);
        }

        public void AddComponents(IEnumerable<MenuStripItemControl> values)
        {
            if (!values.Any())
                throw new InvalidOperationException();

            foreach (MenuStripItemControl item in values)
                AddComponent(item);
        }

        public virtual void RemoveComponent(MenuStripItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Strip.Items.Remove(item.ToolStrip);
            Components.Remove(item);
        }

        public MenuStripItemControl GetComponentAt(byte id)
        {
            return Components.FirstOrDefault(c => c.Id == id);
        }



        protected virtual IEnumerable<MenuStripItemControl> InitializeItems()
        {
            return new List<MenuStripItemControl>();
        }
    }
}
