using BinanceTrackerDesktop.Core.Trigger;

namespace BinanceTrackerDesktop.Core.Event
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
