using BinanceTrackerDesktop.Core.Triggers.Events.Handler;

namespace BinanceTrackerDesktop.Core.Triggers.Events.Listener
{
    public sealed class EventListener : ITriggerableEventHandler<EventArgs>
    {
        public event Action<EventArgs> OnTriggerEventHandler;



        public void TriggerEvent(EventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            OnTriggerEventHandler?.Invoke(e);
        }
    }
}
