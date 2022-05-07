using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;

namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider
{
    public interface IAwaitableComponentsProvider
    {
        IAwaitableComponentsObserver Observer { get; }

        bool IsRegistered { get; }



        void RegisterAllOnce();
    }
}