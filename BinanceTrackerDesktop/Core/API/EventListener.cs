using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.API
{
    public class EventListener : ITriggerableEventHandler<EventArgs>
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
