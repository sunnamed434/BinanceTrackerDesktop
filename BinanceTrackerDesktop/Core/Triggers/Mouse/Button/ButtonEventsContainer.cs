using BinanceTrackerDesktop.Core.Triggers.Events.Listener;

namespace BinanceTrackerDesktop.Core.Triggers.Mouse.Button
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
