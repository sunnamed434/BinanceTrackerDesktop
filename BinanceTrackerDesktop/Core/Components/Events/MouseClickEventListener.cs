using BinanceTrackerDesktop.Core.Trigger;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.Events
{
    public class MouseClickEventListener : ITriggerableEventHandler<MouseEventArgs>
    {
        public event Action<MouseEventArgs> OnTriggerEventHandler;



        public void TriggerEvent(MouseEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            OnTriggerEventHandler?.Invoke(e);
        }
    }
}
