using BinanceTrackerDesktop.Triggers.Events.Handler;

namespace BinanceTrackerDesktop.Triggers.Mouse;

public class MouseClickEventListener : ITriggerableEventHandler<MouseEventArgs>
{
    public event Action<MouseEventArgs> OnTriggerEventHandler;



    public void TriggerEvent(MouseEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        OnTriggerEventHandler?.Invoke(e);
    }
}
