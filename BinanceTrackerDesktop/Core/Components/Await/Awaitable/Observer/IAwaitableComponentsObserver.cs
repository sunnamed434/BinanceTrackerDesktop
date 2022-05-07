namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer
{
    public interface IAwaitableComponentsObserver
    {
        IEnumerable<Func<Task>> Callbacks { get; }



        void RegisterListener(Type type);

        Task CallListenersAsync();
    }
}