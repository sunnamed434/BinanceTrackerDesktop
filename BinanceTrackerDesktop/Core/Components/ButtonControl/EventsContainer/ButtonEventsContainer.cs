using BinanceTrackerDesktop.Core.Event;

namespace BinanceTrackerDesktop.Core.Components.ButtonControl.EventsContainer
{
    public sealed class ButtonEventsContainer
    {
        public readonly EventListener ClickEventListener;



        public ButtonEventsContainer()
        {
            ClickEventListener = new EventListener();
        }
    }
}
