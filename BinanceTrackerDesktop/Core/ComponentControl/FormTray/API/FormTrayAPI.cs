using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.ComponentControl.API;
using BinanceTrackerDesktop.Core.Forms.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.FormTray.API
{
    public interface IFormTray : IFormText
    {
        FormTrayEventsContainer EventsContainerControl { get; }



        void AddComponent(IFormTrayItemControl item);

        void RemoveComponent(IFormTrayItemControl item);

        IFormTrayItemControl GetComponentAt(byte uniqueIndex);
    }

    public interface IFormTrayItemControl : IFormTextColor
    {
        ToolStripMenuItem ToolStrip { get; }

        FormTrayItemEventsContainer EventsContainer { get; }

        byte UniqueIndex { get; }



        void SetImage(Image image);
    }

    public interface IFormTrayClickEventListener : ITriggerableEventHandler<MouseEventArgs>
    {

    }

    public class FormTrayClickEventListener : IFormTrayClickEventListener
    {
        public event Action<MouseEventArgs> OnTriggerEventHandler;



        public void TriggerEvent(MouseEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            OnTriggerEventHandler?.Invoke(e);
        }
    }

    public class FormTrayItemControl : IFormTrayItemControl
    {
        public ToolStripMenuItem ToolStrip { get; }

        public byte UniqueIndex { get; }

        public FormTrayItemEventsContainer EventsContainer { get; }



        private Color defaultColor;



        public FormTrayItemControl(string header, byte uniqueIndex, Image image = default)
        {
            if (string.IsNullOrEmpty(header))
                throw new ArgumentNullException(nameof(header));

            EventsContainer = new FormTrayItemEventsContainer(new FormEventListener());
            ToolStrip = new ToolStripMenuItem(header, image, (s, e) => EventsContainer.ClickEvent.TriggerEvent(e));
            UniqueIndex = uniqueIndex;
            defaultColor = Color.Empty;
        }



        public void SetImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            ToolStrip.Image = image;
        }

        public void SetText(string content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            ToolStrip.Text = content;
        }

        public void SetTextSync(string content, Color color = default)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            SetText(content);

            if (color != default)
                SetTextColor(color);
        }

        public void SetTextColor(Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            ToolStrip.ForeColor = color;
        }

        public void SetDefaultTextColor(Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            defaultColor = color;
        }

        public Color GetDefaultTextColor()
        {
            return defaultColor;
        }
    }

    public class FormTrayEventsContainer
    {
        public IFormTrayClickEventListener MouseClickListener { get; }

        public IFormEventListener DoubleClickListener { get; }



        public FormTrayEventsContainer()
        {
            MouseClickListener = new FormTrayClickEventListener();
            DoubleClickListener = new FormEventListener();
        }
    }

    public class FormTrayItemEventsContainer
    {
        public IFormEventListener ClickEvent { get; }



        public FormTrayItemEventsContainer(IFormEventListener clickEvent)
        {
            ClickEvent = clickEvent;
        }
    }

    public abstract class FormTrayBase : IFormTray
    {
        private NotifyIcon notifyIcon { get; }



        public FormTrayEventsContainer EventsContainerControl { get; }



        private List<IFormTrayItemControl> components = new List<IFormTrayItemControl>();



        public FormTrayBase(NotifyIcon notifyIcon)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            this.notifyIcon = notifyIcon;

            EventsContainerControl = new FormTrayEventsContainer();
            notifyIcon.MouseClick += (s, e) => EventsContainerControl.MouseClickListener.TriggerEvent(e);
            notifyIcon.DoubleClick += (s, e) => EventsContainerControl.DoubleClickListener.TriggerEvent(e);

            foreach (IFormTrayItemControl item in InitializeItems())
                AddComponent(item);
        }



        public void AddComponent(IFormTrayItemControl control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            notifyIcon.ContextMenuStrip.Items.Add(control.ToolStrip);
            components.Add(control);
        }

        public void RemoveComponent(IFormTrayItemControl control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            notifyIcon.ContextMenuStrip.Items.Remove(control.ToolStrip);
            components.Remove(control);
        }

        public IFormTrayItemControl GetComponentAt(byte uniqueIndex)
        {
            return components.FirstOrDefault(c => c.UniqueIndex == uniqueIndex);
        }

        public void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(content);
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



        protected abstract IEnumerable<IFormTrayItemControl> InitializeItems();
    }
}
