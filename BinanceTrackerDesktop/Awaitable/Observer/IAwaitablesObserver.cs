using BinanceTrackerDesktop.Awaitable.Awaitables;

namespace BinanceTrackerDesktop.Awaitable.Observer;

/// <summary>
/// Observer for Awaitables.
/// </summary>
public interface IAwaitablesObserver
{
    /// <summary>
    /// All registered callbacks.
    /// </summary>
    IEnumerable<Func<Task>> Callbacks { get; }



    /// <summary>
    /// Registering a given type.
    /// </summary>
    /// <param name="type">Type for register.</param>
    /// <exception cref="ArgumentNullException"></exception>
    void RegisterListener(Type type);

    /// <summary>
    /// Registering all given types, by calling method <see cref="RegisterListener(Type)"/>
    /// </summary>
    /// <param name="types">Types for registering.</param>
    /// <exception cref="InvalidOperationException"></exception>
    void RegisterListeners(Type[] types);

    /// <summary>
    /// Registering all given types, by calling method <see cref="RegisterListener(Type)"/>
    /// </summary>
    /// <param name="types">Types for registering.</param>
    /// <exception cref="InvalidOperationException"></exception>
    void RegisterListeners(IEnumerable<Type> types);

    /// <summary>
    /// Calling registered listeners in next order:
    /// <para><see cref="IAwaitableObserverInstance"/>;</para> 
    /// <para><see cref="IAwaitableStart"/>;</para>
    /// <para><see cref="IAwaitableExecute"/>;</para>
    /// <para><see cref="IAwaitableAsyncExecute"/>;</para>
    /// <para><see cref="IAwaitableComplete"/></para>
    /// </summary>
    /// <returns>Completed task.</returns>
    Task CallListenersAsync();
}
