namespace BinanceTrackerDesktop.Core.Trigger
{
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
        /// Invoking <see cref="OnTriggerEventHandler"/>
        /// </summary>
        /// <param name="e">Event args</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        void TriggerEvent(TEventArgs e);
    }
}
