using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;
using System.Reflection;

namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider
{
    public sealed class AwaitableComponentsProvider : IAwaitableComponentsProvider
    {
        public IAwaitableComponentsObserver Observer { get; }

        public bool IsRegistered { get; private set; }



        public AwaitableComponentsProvider(IAwaitableComponentsObserver observer)
        {
            Observer = observer ?? throw new ArgumentNullException(nameof(observer));
        }



        public void RegisterAwaitablesOnce()
        {
            if (IsRegistered)
            {
                throw new InvalidOperationException();
            }

            Observer.RegisterListeners(Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => 
                    t.GetInterface(nameof(IAwaitableComponentObserverInstance)) != null
                    || t.GetInterface(nameof(IAwaitableComponentStart)) != null
                    || t.GetInterface(nameof(IAwaitableComponentExecute)) != null
                    || t.GetInterface(nameof(IAwaitableComponentAsyncExecute)) != null
                    || t.GetInterface(nameof(IAwaitableComponentComplete)) != null));

            IsRegistered = !IsRegistered;
        }
    }
}
