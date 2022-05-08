using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;

namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component
{
    /// <summary>
    /// For awaitable component, an a interface that giving a special property instance.
    /// <para>It set`s automatically at runtime via "Reflection".</para>
    /// </summary>
    public interface IAwaitableComponentObserverInstance
    {
        /// <summary>
        /// Instance to the observer.
        /// </summary>
        IAwaitableComponentsObserver Observer { get; set; }
    }
}
