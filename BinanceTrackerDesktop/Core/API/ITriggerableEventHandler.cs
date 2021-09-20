using System;

namespace BinanceTrackerDesktop.Core.API
{
    public interface ITriggerableEventHandler<TEventArgs>
    {
        event Action<TEventArgs> OnTriggerEventHandler;



        void TriggerEvent(TEventArgs e);
    }
}
