using BinanceTrackerDesktop.Core.Triggers.Events.Listener;

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
