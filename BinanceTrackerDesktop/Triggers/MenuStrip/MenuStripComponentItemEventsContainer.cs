using BinanceTrackerDesktop.Triggers.Events.Listener;

namespace BinanceTrackerDesktop.Triggers.MenuStrip;

public sealed class MenuStripComponentItemEventsContainer
{
    public readonly EventListener OnClick;



    public MenuStripComponentItemEventsContainer()
    {
        OnClick = new EventListener();
    }
}
