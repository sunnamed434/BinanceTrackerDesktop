using BinanceTrackerDesktop.Core.Event;

namespace BinanceTrackerDesktop.Core.Components.LabelControl.EventsContainer
{
    public sealed class LabelComponentEventsContainer
    {
        public readonly EventListener ClickEventListener;



        public LabelComponentEventsContainer()
        {
            ClickEventListener = new EventListener();
        }
    }
}
