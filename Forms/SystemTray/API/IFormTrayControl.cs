using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Control.API;
using BinanceTrackerDesktop.Forms.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.SystemTray.API
{
    public interface IFormTrayControl : IFormText
    {
        IFormSystemTrayControl SystemTrayFormControl { get; }

        IFormTrayItemControl[] Items { get; }

        FormTrayEventsContainerControl EventsContainerControl { get; }



        void AddComponent(IFormTrayItemControl item);

        void RemoveComponent(IFormTrayItemControl item);

        IFormTrayItemControl GetComponentAt(byte uniqueIndex);
    }

    public interface IFormTrayItemControl : IFormClickEventListenerHandle, IFormTextColor
    {
        ToolStripMenuItem ToolStrip { get; }

        byte UniqueIndex { get; }



        void SetHeader(string text);

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

    public class FormTrayEventsContainerControl
    {
        public IFormTrayClickEventListener MouseClick { get; }

        public IFormEventListener DoubleClickListener { get; }

        public IFormEventListener CloseListener { get; }



        public FormTrayEventsContainerControl()
        {
            MouseClick = new FormTrayClickEventListener();
            DoubleClickListener = new FormEventListener();
            CloseListener = new FormEventListener();
        }
    }

    public class FormTrayControl : IFormTrayControl
    {
        public IFormSystemTrayControl SystemTrayFormControl { get; }

        public IFormTrayItemControl[] Items => components.ToArray();

        public FormTrayEventsContainerControl EventsContainerControl { get; }



        private List<IFormTrayItemControl> components = new List<IFormTrayItemControl>();



        public FormTrayControl(IFormSystemTrayControl systemTrayFormControl, List<IFormTrayItemControl> items)
        {
            if (systemTrayFormControl == null)
                throw new ArgumentNullException(nameof(systemTrayFormControl));

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (items.Count < 0)
                throw new InvalidOperationException();

            SystemTrayFormControl = systemTrayFormControl;
            EventsContainerControl = new FormTrayEventsContainerControl();

            foreach (IFormTrayItemControl item in items)
                AddComponent(item);
        }



        public void AddComponent(IFormTrayItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            components.Add(item);
            SystemTrayFormControl.AddItem(item);
        }

        public void RemoveComponent(IFormTrayItemControl item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            components.Add(item);
            SystemTrayFormControl.AddItem(item);
        }

        public IFormTrayItemControl GetComponentAt(byte uniqueIndex)
        {
            return components.FirstOrDefault(c => c.UniqueIndex == uniqueIndex);
        }

        public void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            SystemTrayFormControl.NotifyIcon.Text = content;
        }
    }

    public class FormTrayItemControl : IFormTrayItemControl
    {
        public ToolStripMenuItem ToolStrip { get; }

        public IFormEventListener ClickEvent { get; }

        public byte UniqueIndex { get; }



        private Color defaultColor;



        public FormTrayItemControl(string header, byte uniqueIndex, Image image = default)
        {
            if (string.IsNullOrEmpty(header))
                throw new ArgumentNullException(nameof(header));

            ClickEvent = new FormEventListener();
            ToolStrip = new ToolStripMenuItem(header, image, (s, e) => ClickEvent.TriggerEvent(e));
            UniqueIndex = uniqueIndex;
            defaultColor = Color.Empty;
        }



        public void SetHeader(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            ToolStrip.Text = text;
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
}
