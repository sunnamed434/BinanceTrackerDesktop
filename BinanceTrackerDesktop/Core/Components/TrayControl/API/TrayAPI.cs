using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.TextControl.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.TrayControl.API
{
    public class TrayClickEventListener : ITriggerableEventHandler<MouseEventArgs>
    {
        public event Action<MouseEventArgs> OnTriggerEventHandler;



        public void TriggerEvent(MouseEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            OnTriggerEventHandler?.Invoke(e);
        }
    }

    public class TrayItemControl : TextComponentControl
    {
        public ToolStripMenuItem ToolStrip { get; }

        public TrayItemEventsContainer EventsContainer { get; }

        public byte UniqueIndex { get; }



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
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            ToolStrip.Text = content;
        }
    }

    public class TrayEventsContainer
    {
        public TrayClickEventListener MouseClickListener { get; }

        public IEventListener DoubleClickListener { get; }



        public TrayEventsContainer()
        {
            MouseClickListener = new TrayClickEventListener();
            DoubleClickListener = new EventListener();
        }
    }

    public class TrayItemEventsContainer
    {
        public IEventListener ClickEvent { get; }



        public TrayItemEventsContainer(IEventListener clickEvent)
        {
            ClickEvent = clickEvent;
        }
    }

    public abstract class TrayBase : TextComponentControl
    {
        private NotifyIcon notifyIcon { get; }



        public TrayEventsContainer EventsContainerControl { get; }



        private List<TrayItemControl> components = new List<TrayItemControl>();



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



        protected abstract IEnumerable<TrayItemControl> InitializeItems();
    }
}
