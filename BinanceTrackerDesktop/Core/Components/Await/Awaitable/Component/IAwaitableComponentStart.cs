namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component
{
    /// <summary>
    /// For awaitable component, an a interface that giving a special method when application close is starting.
    /// <para>It set`s automatically at runtime via "Reflection".</para>
    /// </summary>
    public interface IAwaitableComponentStart
    {
        /// <summary>
        /// Execute`s when application closing started.
        /// </summary>
        void OnStart();
    }
}
