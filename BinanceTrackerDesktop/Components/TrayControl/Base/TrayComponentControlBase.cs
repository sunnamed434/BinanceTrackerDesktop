using BinanceTrackerDesktop.Components.ContextMenuStripControl.Base;
using BinanceTrackerDesktop.Components.TrayControl.EventsContainer;

namespace BinanceTrackerDesktop.Components.TrayControl.Base;

public class TrayComponentControlBase : MenuStripComponentControlBase
{
    public readonly TrayComponentEventsContainer EventsContainerControl;

    public readonly NotifyIcon NotifyIcon;



    public TrayComponentControlBase(NotifyIcon notifyIcon) : base(notifyIcon.ContextMenuStrip)
    {
        if (notifyIcon == null)
        {
            throw new ArgumentNullException(nameof(notifyIcon));
        }

        EventsContainerControl = new TrayComponentEventsContainer();
        NotifyIcon = notifyIcon;

        notifyIcon.MouseClick += (s, e) => EventsContainerControl.MouseClickListener.TriggerEvent(e);
        notifyIcon.DoubleClick += (s, e) => EventsContainerControl.DoubleClickListener.TriggerEvent(e);

        AddComponents(InitializeItems());
    }



    public override void SetText(string content, Color? color = null)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException(content);
        }

        NotifyIcon.Text = content;
    }
}
