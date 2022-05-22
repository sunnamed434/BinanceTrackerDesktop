using BinanceTrackerDesktop.Core.Awaitable.Awaitables;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;

namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider
{
    /// <summary>
    /// Provider for registering Awaitables.
    /// </summary>
    public interface IAwaitablesProvider
    {
        /// <summary>
        /// Instance to the <see cref="IAwaitablesObserver"/>
        /// </summary>
        IAwaitablesObserver Observer { get; }

        /// <summary>
        /// Returning <see langword="true"/> if method <see cref="RegisterAwaitablesOnce"/> was already ever called once, <see langword="false"/> if yet not.
        /// </summary>
        bool IsRegistered { get; }



        /// <summary>
        /// Registering awaitables at the end set to <see cref="IsRegistered"/> to <see langword="true"/>.
        /// <br>Registering only once in next order:</br>
        /// <para><see cref="IAwaitableObserverInstance"/>;</para> 
        /// <para><see cref="IAwaitableStart"/>;</para>
        /// <para><see cref="IAwaitableAsyncExecute"/>;</para>
        /// <para><see cref="IAwaitableComplete"/></para>
        /// </summary>
        /// <exception cref="System.InvalidOperationException"></exception>
        void RegisterAwaitablesOnce();
    }
}