using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API
{
    public interface IStripItem
    {
        string Header { get; }

        Image Image { get; }

        byte Id { get; }
    }

    public class StripItemControl : TextComponentControl, IStripItem
    {
        public readonly StripItemEventsContainer EventsContainer;

        public readonly ToolStripMenuItem ToolStrip;



        public string Header { get; }

        public Image Image { get; }

        public byte Id { get; }



        public StripItemControl(string header, Image image, byte id)
        {
            if (string.IsNullOrEmpty(header))
                throw new ArgumentNullException(nameof(header));

            Header = header;
            Image = image ?? default;
            Id = id;

            EventsContainer = new StripItemEventsContainer();
            ToolStrip = new ToolStripMenuItem(header, image, (s, e) => EventsContainer.OnClick.TriggerEvent(e));
        }

        public StripItemControl(string header, byte id) : this(header, null, id)
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

    public class StripItemEventsContainer
    {
        public readonly EventListener OnClick;



        public StripItemEventsContainer()
        {
            OnClick = new EventListener();
        }
    }

    public class MenuStripControlBase : TextComponentControl, IExpandableComponent<StripItemControl, byte>
    {
        public readonly ContextMenuStrip Strip;

        public IEnumerable<StripItemControl> AllComponents => Components;



        protected List<StripItemControl> Components = new List<StripItemControl>();



        public MenuStripControlBase(ContextMenuStrip strip) : base(strip)
        {
            if (strip == null)
                throw new ArgumentNullException(nameof(strip));

            Strip = strip;
        }

        public MenuStripControlBase()
        {

        }



        public virtual void AddComponent(StripItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Strip.Items.Add(item.ToolStrip);
            Components.Add(item);
        }

        public virtual void RemoveComponent(StripItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Strip.Items.Remove(item.ToolStrip);
            Components.Remove(item);
        }

        public StripItemControl GetComponentAt(byte id)
        {
            return Components.FirstOrDefault(c => c.Id == id);
        }



        protected virtual IEnumerable<StripItemControl> InitializeItems()
        {
            return new List<StripItemControl>();
        }
    }
}
