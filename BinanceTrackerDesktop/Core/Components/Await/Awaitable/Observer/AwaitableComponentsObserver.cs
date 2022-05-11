using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider.Utilities.Reflection;

namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer
{
    public sealed class AwaitableComponentsObserver : IAwaitableComponentsObserver
    {
        private IList<Func<Task>> closeCallbacks = new List<Func<Task>>();

        private IList<Type> registeredTypes = new List<Type>();



        public IEnumerable<Func<Task>> Callbacks => closeCallbacks;



        public void RegisterListener(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            registeredTypes.Add(type);
        }

        public void RegisterListeners(Type[] types)
        {
            if (types.Any() == false)
            {
                throw new InvalidOperationException();
            }

            for (int i = 0; i < types.Length; i++)
            {
                RegisterListener(types[i]);
            }
        }

        public void RegisterListeners(IEnumerable<Type> types)
        {
            if (types.Any() == false)
            {
                throw new InvalidOperationException();
            }

            foreach (Type type in types)
            {
                RegisterListener(type);
            }
        }

        public async Task CallListenersAsync()
        {
            Type[] types = registeredTypes.ToArray();
            foreach (IAwaitableComponentObserverInstance awaitableComponentObserverInstance in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComponentObserverInstance>(types))
            {
                awaitableComponentObserverInstance.Observer = this;
            }

            foreach (IAwaitableComponentStart awaitableComponentStart in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComponentStart>(types))
            {
                awaitableComponentStart.OnStart();
            }

            foreach (IAwaitableComponentExecute awaitableComponent in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComponentExecute>(types))
            {
                awaitableComponent.OnExecute();
            }

            foreach (IAwaitableComponentAsyncExecute awaitableComponentAsync in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComponentAsyncExecute>(types))
            {
                closeCallbacks.Add(awaitableComponentAsync.OnExecuteAsync);
            }

            Func<Task> func = () => Task.CompletedTask;
            foreach (Func<Task> callback in closeCallbacks)
            {
                if (callback == null)
                    throw new ArgumentNullException(nameof(callback));

                func += callback;
            }

            await func?.Invoke();

            foreach (IAwaitableComponentComplete awaitableComponentComplete in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComponentComplete>(types))
            {
                awaitableComponentComplete.OnComplete();
            }
        }
    }
}
