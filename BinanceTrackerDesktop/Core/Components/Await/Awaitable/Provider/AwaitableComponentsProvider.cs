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



        public void RegisterAllOnce()
        {
            if (IsRegistered)
            {
                throw new InvalidOperationException();
            }

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (type.GetInterface(nameof(IAwaitableComponent)) != null 
                    || type.GetInterface(nameof(IAwaitableComponentStart)) != null 
                    || type.GetInterface(nameof(IAwaitableComponentComplete)) != null
                    || type.GetInterface(nameof(IAwaitableComponentObserverInstance)) != null)
                {
                    Observer.RegisterListener(type);
                }
            }

            IsRegistered = !IsRegistered;
        }
    }
}
