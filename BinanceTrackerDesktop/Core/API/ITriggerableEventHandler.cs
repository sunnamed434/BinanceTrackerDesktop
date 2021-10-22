using System;

namespace BinanceTrackerDesktop.Core.API
{
    public interface ITriggerableEventHandler<TEventArgs> where TEventArgs : EventArgs
    {
        event Action<TEventArgs> OnTriggerEventHandler;



        void TriggerEvent(TEventArgs e);
    }
}
