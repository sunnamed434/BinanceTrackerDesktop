using BinanceTrackerDesktop.Core.Triggers.Events.Listener;

namespace BinanceTrackerDesktop.Core.Triggers.MenuStrip
{
    public sealed class MenuStripComponentItemEventsContainer
    {
        public readonly EventListener OnClick;



        public MenuStripComponentItemEventsContainer()
        {
            OnClick = new EventListener();
        }
    }
}
