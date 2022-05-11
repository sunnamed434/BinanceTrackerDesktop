using BinanceTrackerDesktop.Core.Triggers.Events.Listener;
using BinanceTrackerDesktop.Core.Triggers.Mouse;

namespace BinanceTrackerDesktop.Core.Components.TrayControl.EventsContainer
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
}
