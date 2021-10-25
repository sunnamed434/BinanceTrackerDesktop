using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.TrayControl.API
{
    public sealed class TrayEventsContainer
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
        public readonly TrayEventsContainer EventsContainerControl;

        public readonly NotifyIcon NotifyIcon;



        public TrayControlBase(NotifyIcon notifyIcon) : base(notifyIcon.ContextMenuStrip)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            EventsContainerControl = new TrayEventsContainer();
            NotifyIcon = notifyIcon;

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
    }
}
