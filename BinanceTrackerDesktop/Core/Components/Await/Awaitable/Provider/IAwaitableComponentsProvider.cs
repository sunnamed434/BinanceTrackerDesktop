using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;

namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider
{
    /// <summary>
    /// Provider for registering Awaitables.
    /// </summary>
    public interface IAwaitableComponentsProvider
    {
        /// <summary>
        /// Instance to the <see cref="IAwaitableComponentsObserver"/>
        /// </summary>
        IAwaitableComponentsObserver Observer { get; }

        /// <summary>
        /// Returning <see langword="true"/> if method <see cref="RegisterAwaitablesOnce"/> was already ever called once, <see langword="false"/> if yet not.
        /// </summary>
        bool IsRegistered { get; }



        /// <summary>
        /// Registering awaitables at the end set to <see cref="IsRegistered"/> to <see langword="true"/>.
        /// Registering only once with next interfaces:
        /// <para><see cref="IAwaitableComponentObserverInstance"/>;</para> 
        /// <para><see cref="IAwaitableComponentStart"/>;</para>
        /// <para><see cref="IAwaitableComponentAsyncExecute"/>;</para>
        /// <para><see cref="IAwaitableComponentComplete"/></para>
        /// </summary>
        /// <exception cref="System.InvalidOperationException"></exception>
        void RegisterAwaitablesOnce();
    }
}