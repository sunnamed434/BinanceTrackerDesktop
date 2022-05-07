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
                throw new ArgumentNullException(nameof(type));   

            registeredTypes.Add(type);
        }

        public async Task CallListenersAsync()
        {
            foreach (IAwaitableComponentObserverInstance awaitableComponentObserverInstance in ReflectionInterfacesFromTypesReceiverAssemblyUtility.GetInterfacesFromTypes<IAwaitableComponentObserverInstance>(registeredTypes.ToArray()))
            {
                awaitableComponentObserverInstance.Observer = this;
            }

            foreach (IAwaitableComponentStart awaitableComponentStart in ReflectionInterfacesFromTypesReceiverAssemblyUtility.GetInterfacesFromTypes<IAwaitableComponentStart>(registeredTypes.ToArray()))
            {
                awaitableComponentStart.OnStart();
            }

            foreach (IAwaitableComponent awaitableComponent in ReflectionInterfacesFromTypesReceiverAssemblyUtility.GetInterfacesFromTypes<IAwaitableComponent>(registeredTypes.ToArray()))
            {
                closeCallbacks.Add(awaitableComponent.OnExecute);
            }

            Func<Task> func = () => Task.CompletedTask;
            foreach (Func<Task> callback in closeCallbacks)
            {
                if (callback == null)
                    throw new ArgumentNullException(nameof(callback));

                func += callback;
            }

            await func?.Invoke();

            foreach (IAwaitableComponentComplete awaitableComponentComplete in ReflectionInterfacesFromTypesReceiverAssemblyUtility.GetInterfacesFromTypes<IAwaitableComponentComplete>(registeredTypes.ToArray()))
            {
                awaitableComponentComplete.OnComplete();
            }

            await Task.CompletedTask;
        }
    }
}
