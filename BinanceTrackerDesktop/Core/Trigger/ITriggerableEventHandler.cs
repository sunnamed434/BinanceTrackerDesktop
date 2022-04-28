using System;

namespace BinanceTrackerDesktop.Core.Trigger
{
    public interface ITriggerableEventHandler<TEventArgs> where TEventArgs : EventArgs
    {
        event Action<TEventArgs> OnTriggerEventHandler;



        void TriggerEvent(TEventArgs e);
    }
}
