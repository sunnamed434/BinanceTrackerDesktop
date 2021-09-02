using BinanceTrackerDesktop.Core.API;
using System;

namespace BinanceTrackerDesktop.Forms.API
{
    public interface IFormEventListener : ITriggerableEventHandler
    {
        
    }

    public class FormEventListener : IFormEventListener
    {
        public event EventHandler OnTriggerEventHandler;



        public void TriggerEvent(object sender, EventArgs e)
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));

            if (e == null)
                throw new ArgumentNullException(nameof(e));

            OnTriggerEventHandler?.Invoke(sender, e);
        }
    }
}
