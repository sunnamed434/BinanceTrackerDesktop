using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.TrayControl.API
{
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

    public class TrayControlBase : MenuStripControlBase
    {
        public readonly NotifyIcon NotifyIcon;

        public readonly TrayEventsContainer EventsContainerControl;



        public TrayControlBase(NotifyIcon notifyIcon) : base(notifyIcon.ContextMenuStrip)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            NotifyIcon = notifyIcon;
            EventsContainerControl = new TrayEventsContainer();

            notifyIcon.MouseClick += (s, e) => EventsContainerControl.MouseClickListener.TriggerEvent(e);
            notifyIcon.DoubleClick += (s, e) => EventsContainerControl.DoubleClickListener.TriggerEvent(e);

            base.AddComponents(InitializeItems());
        }



        public override void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(content);

            NotifyIcon.Text = content; 
        }

        public void Show()
        {
            NotifyIcon.ContextMenuStrip.Show();
        }

        public void Close()
        {
            using (NotifyIcon)
                NotifyIcon.Visible = false;
        }
    }
}
