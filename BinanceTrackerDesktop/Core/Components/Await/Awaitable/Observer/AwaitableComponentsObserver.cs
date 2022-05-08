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
            try
            {
                foreach (IAwaitableComponentObserverInstance awaitableComponentObserverInstance in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComponentObserverInstance>(registeredTypes.ToArray()))
                {
                    awaitableComponentObserverInstance.Observer = this;
                }

                foreach (IAwaitableComponentStart awaitableComponentStart in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComponentStart>(registeredTypes.ToArray()))
                {
                    awaitableComponentStart.OnStart();
                }

                foreach (IAwaitableComponentExecute awaitableComponent in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComponentExecute>(registeredTypes.ToArray()))
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

                foreach (IAwaitableComponentComplete awaitableComponentComplete in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComponentComplete>(registeredTypes.ToArray()))
                {
                    awaitableComponentComplete.OnComplete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }
    }
}
