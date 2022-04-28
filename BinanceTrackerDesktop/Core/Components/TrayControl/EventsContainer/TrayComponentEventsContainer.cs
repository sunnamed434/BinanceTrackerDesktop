using BinanceTrackerDesktop.Core.Components.Events;
using BinanceTrackerDesktop.Core.Event;

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
