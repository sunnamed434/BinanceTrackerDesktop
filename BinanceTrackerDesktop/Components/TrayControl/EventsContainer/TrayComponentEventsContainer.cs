using BinanceTrackerDesktop.Triggers.Events.Listener;
using BinanceTrackerDesktop.Triggers.Mouse;

namespace BinanceTrackerDesktop.Components.TrayControl.EventsContainer;

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
