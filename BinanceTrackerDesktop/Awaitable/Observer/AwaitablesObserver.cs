using BinanceTrackerDesktop.Awaitable.Awaitables;
using BinanceTrackerDesktop.Awaitable.Provider.Utilities.Reflection;

namespace BinanceTrackerDesktop.Awaitable.Observer;

public sealed class AwaitablesObserver : IAwaitablesObserver
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
        foreach (IAwaitableObserverInstance awaitableComponentObserverInstance in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableObserverInstance>(types))
        {
            awaitableComponentObserverInstance.Observer = this;
        }

        foreach (IAwaitableStart awaitableComponentStart in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableStart>(types))
        {
            awaitableComponentStart.OnStart();
        }

        foreach (IAwaitableExecute awaitableComponent in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableExecute>(types))
        {
            awaitableComponent.OnExecute();
        }

        foreach (IAwaitableAsyncExecute awaitableComponentAsync in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableAsyncExecute>(types))
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

        foreach (IAwaitableComplete awaitableComponentComplete in ReflectionInterfacesFromTypesReceiverUtility.GetInterfacesFromTypes<IAwaitableComplete>(types))
        {
            awaitableComponentComplete.OnComplete();
        }
    }
}
