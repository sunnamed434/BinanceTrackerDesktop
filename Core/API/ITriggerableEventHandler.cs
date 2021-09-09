using System;

namespace BinanceTrackerDesktop.Core.API
{
    public interface ITriggerableEventHandler<EventArgs>
    {
        event Action<EventArgs> OnTriggerEventHandler;



        void TriggerEvent(EventArgs e);
    }
}
