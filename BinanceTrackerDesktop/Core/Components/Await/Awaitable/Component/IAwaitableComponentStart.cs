namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component
{
    /// <summary>
    /// For awaitable component, an a interface that giving a special method when application close is starting.
    /// <para>It executes automatically at runtime via <see langword="Reflection"/></para>
    /// </summary>
    public interface IAwaitableComponentStart
    {
        /// <summary>
        /// Execute`s when application closing started.
        /// </summary>
        void OnStart();
    }
}
