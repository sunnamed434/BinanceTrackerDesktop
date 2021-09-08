using System;

namespace BinanceTrackerDesktop.Core.API
{
    public interface ITriggerableEventHandler
    {
        event EventHandler OnTriggerEventHandler;

        void TriggerEvent(object sender, EventArgs e);
    }
}
