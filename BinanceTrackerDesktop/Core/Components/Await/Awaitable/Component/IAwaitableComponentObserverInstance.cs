using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;

namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component
{
    public interface IAwaitableComponentObserverInstance
    {
        IAwaitableComponentsObserver Observer { get; set; }
    }
}
