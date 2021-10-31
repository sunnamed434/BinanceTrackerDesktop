using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.TrayControl
{
    public sealed class TrayComponentEventsContainer
    {
        public readonly MouseClickEventListener MouseClickListener;

        public readonly EventListener DoubleClickListener;



        public TrayComponentEventsContainer()
        {
            MouseClickListener = new MouseClickEventListener();
            DoubleClickListener = new EventListener();
        }
    }

    public class TrayComponentControlBase : MenuStripComponentControlBase
    {
        public readonly TrayComponentEventsContainer EventsContainerControl;

        public readonly NotifyIcon NotifyIcon;



        public TrayComponentControlBase(NotifyIcon notifyIcon) : base(notifyIcon.ContextMenuStrip)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            EventsContainerControl = new TrayComponentEventsContainer();
            NotifyIcon = notifyIcon;

            notifyIcon.MouseClick += (s, e) => EventsContainerControl.MouseClickListener.TriggerEvent(e);
            notifyIcon.DoubleClick += (s, e) => EventsContainerControl.DoubleClickListener.TriggerEvent(e);

            base.AddComponents(InitializeItems());
        }



        public override void SetText(string content, Color? color = null)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(content);

            NotifyIcon.Text = content;
        }
    }
}
