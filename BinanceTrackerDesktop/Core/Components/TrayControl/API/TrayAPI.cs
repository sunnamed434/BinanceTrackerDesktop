using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.TrayControl.API
{
    public class TrayItemControl : TextComponentControl
    {
        public readonly ToolStripMenuItem ToolStrip;

        public readonly TrayItemEventsContainer EventsContainer;

        public readonly byte UniqueIndex;



        public TrayItemControl(string header, byte uniqueIndex, Image image = default) : base()
        {
            if (string.IsNullOrEmpty(header))
                throw new ArgumentNullException(nameof(header));

            EventsContainer = new TrayItemEventsContainer(new EventListener());
            ToolStrip = new ToolStripMenuItem(header, image, (s, e) => EventsContainer.ClickEvent.TriggerEvent(e));
            UniqueIndex = uniqueIndex;
        }



        public void SetImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            ToolStrip.Image = image;
        }

        public override void SetTextColor(Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            ToolStrip.ForeColor = color;
        }

        public override void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            ToolStrip.Text = content;
        }
    }

    public abstract class TrayBase : TextComponentControl
    {
        public readonly TrayEventsContainer EventsContainerControl;



        private readonly NotifyIcon notifyIcon;

        private readonly List<TrayItemControl> components = new List<TrayItemControl>();



        public TrayBase(NotifyIcon notifyIcon) : base()
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            this.notifyIcon = notifyIcon;

            EventsContainerControl = new TrayEventsContainer();
            notifyIcon.MouseClick += (s, e) => EventsContainerControl.MouseClickListener.TriggerEvent(e);
            notifyIcon.DoubleClick += (s, e) => EventsContainerControl.DoubleClickListener.TriggerEvent(e);

            foreach (TrayItemControl item in InitializeItems())
                AddComponent(item);
        }



        public void AddComponent(TrayItemControl control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            notifyIcon.ContextMenuStrip.Items.Add(control.ToolStrip);
            components.Add(control);
        }

        public void RemoveComponent(TrayItemControl control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            notifyIcon.ContextMenuStrip.Items.Remove(control.ToolStrip);
            components.Remove(control);
        }

        public TrayItemControl GetComponentAt(byte uniqueIndex)
        {
            return components.FirstOrDefault(c => c.UniqueIndex == uniqueIndex);
        }

        public override void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(content);

            notifyIcon.Text = content; 
        }

        public void Show()
        {
            notifyIcon.ContextMenuStrip.Show();
        }

        public void Close()
        {
            using (this.notifyIcon)
                this.notifyIcon.Visible = false;
        }



        protected virtual IEnumerable<TrayItemControl> InitializeItems()
        {
            return new List<TrayItemControl>();
        }
    }

    public class TrayEventsContainer
    {
        public readonly MouseClickEventListener MouseClickListener;

        public readonly EventListener DoubleClickListener;



        public TrayEventsContainer()
        {
            MouseClickListener = new MouseClickEventListener();
            DoubleClickListener = new EventListener();
        }
    }

    public class TrayItemEventsContainer
    {
        public readonly EventListener ClickEvent;



        public TrayItemEventsContainer(EventListener clickEvent)
        {
            ClickEvent = clickEvent;
        }
    }
}
