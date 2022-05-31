using BinanceTrackerDesktop.Triggers.Events.Listener;

namespace BinanceTrackerDesktop.Components.ContextMenuStripControl.Item.EventsContainer;

public sealed class MenuStripComponentItemEventsContainer
{
    public readonly EventListener OnClick;



    public MenuStripComponentItemEventsContainer()
    {
        OnClick = new EventListener();
    }
}
