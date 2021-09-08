using BinanceTrackerDesktop.Core.Control.API;
using BinanceTrackerDesktop.Forms.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.SystemTray.API
{
    public interface IFormTrayControl : IFormText
    {
        IFormSystemTrayControl SystemTrayFormControl { get; }

        IFormTrayItemControl[] Items { get; }

        IFormClickEventListenerHandle ClickListener { get; }

        IFormClickEventListenerHandle DoubleClickListener { get; }

        IFormClickEventListenerHandle CloseListener { get; }



        void AddComponent(IFormTrayItemControl item);

        void RemoveComponent(IFormTrayItemControl item);
    }

    public interface IFormTrayItemControl : IFormClickEventListenerHandle, IFormTextColor
    {
        ToolStripMenuItem ToolStrip { get; }



        void SetHeader(string text);

        void SetImage(Image image);
    }

    public class FormTrayControl : IFormTrayControl
    {
        public IFormSystemTrayControl SystemTrayFormControl { get; }

        public IFormTrayItemControl[] Items => components.ToArray();

        public IFormClickEventListenerHandle ClickListener { get; }

        public IFormClickEventListenerHandle DoubleClickListener { get; }

        public IFormClickEventListenerHandle CloseListener { get; }



        private List<IFormTrayItemControl> components = new List<IFormTrayItemControl>();



        public FormTrayControl(IFormSystemTrayControl trayFormControl, List<IFormTrayItemControl> items)
        {
            if (trayFormControl == null)
                throw new ArgumentNullException(nameof(trayFormControl));

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (items.Count < 0)
                throw new InvalidOperationException();

            foreach (IFormTrayItemControl item in items)
                components.Add(item);
        }

        public FormTrayControl(IFormSystemTrayControl trayFormControl)
        {
            if (trayFormControl == null)
                throw new ArgumentNullException(nameof(trayFormControl));
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

            components.Remove(item);
            SystemTrayFormControl.RemoveItem(item);
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



        private Color defaulColor;



        public FormTrayItemControl(string header, Image image = default)
        {
            if (string.IsNullOrEmpty(header))
                throw new ArgumentNullException(nameof(header));

            ClickEvent = new FormEventListener();
            ToolStrip = new ToolStripMenuItem(header, image, (s, e) => ClickEvent.TriggerEvent(s, e));
            defaulColor = Color.Empty;
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

            defaulColor = color;
        }

        public Color GetDefaultTextColor()
        {
            return defaulColor;
        }
    }
}
