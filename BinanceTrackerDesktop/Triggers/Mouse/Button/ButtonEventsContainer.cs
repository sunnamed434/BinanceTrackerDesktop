using BinanceTrackerDesktop.Triggers.Events.Listener;

namespace BinanceTrackerDesktop.Triggers.Mouse.Button;

public sealed class ButtonEventsContainer
{
    public readonly EventListener ClickEventListener;



    public ButtonEventsContainer()
    {
        ClickEventListener = new EventListener();
    }
}
