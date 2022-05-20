using BinanceTrackerDesktop.Core.Awaitable.Awaitables;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;
using System.Reflection;

namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider
{
    public sealed class AwaitablesProvider : IAwaitablesProvider
    {
        public AwaitablesProvider(IAwaitablesObserver observer)
        {
            Observer = observer ?? throw new ArgumentNullException(nameof(observer));
        }



        public IAwaitablesObserver Observer { get; }

        public bool IsRegistered { get; private set; }



        public void RegisterAwaitablesOnce()
        {
            if (IsRegistered)
            {
                throw new InvalidOperationException();
            }

            Observer.RegisterListeners(Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => 
                    t.GetInterface(nameof(IAwaitableObserverInstance)) != null
                    || t.GetInterface(nameof(IAwaitableStart)) != null
                    || t.GetInterface(nameof(IAwaitableExecute)) != null
                    || t.GetInterface(nameof(IAwaitableAsyncExecute)) != null
                    || t.GetInterface(nameof(IAwaitableComplete)) != null));

            IsRegistered = !IsRegistered;
        }
    }
}
