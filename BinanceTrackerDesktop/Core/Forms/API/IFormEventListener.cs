using BinanceTrackerDesktop.Core.API;
using System;

namespace BinanceTrackerDesktop.Core.Forms.API
{
    public interface IFormEventListener : ITriggerableEventHandler<EventArgs>
    {
        
    }

    public class FormEventListener : IFormEventListener
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
