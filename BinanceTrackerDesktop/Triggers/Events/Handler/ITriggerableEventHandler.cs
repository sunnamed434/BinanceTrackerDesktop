namespace BinanceTrackerDesktop.Triggers.Events.Handler;

/// <summary>
/// A triggerable event handler for special targets.
/// </summary>
/// <typeparam name="TEventArgs">Event args.</typeparam>
public interface ITriggerableEventHandler<TEventArgs> where TEventArgs : EventArgs
{
    /// <summary>
    /// An event target for triggering.
    /// </summary>
    event Action<TEventArgs> OnTriggerEventHandler;



    /// <summary>
    /// Invoking event <see cref="OnTriggerEventHandler"/>
    /// </summary>
    /// <param name="e">Event args</param>
    /// <exception cref="ArgumentNullException"></exception>
    void TriggerEvent(TEventArgs e);
}
