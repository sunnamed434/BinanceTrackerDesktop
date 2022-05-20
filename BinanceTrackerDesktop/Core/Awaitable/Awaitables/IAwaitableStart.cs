namespace BinanceTrackerDesktop.Core.Awaitable.Awaitables
{
    /// <summary>
    /// For awaitable component, an a interface that giving a special method when application close is starting.
    /// <para>It executes automatically at runtime via <see langword="Reflection"/></para>
    /// </summary>
    public interface IAwaitableStart
    {
        /// <summary>
        /// Execute`s when application closing started.
        /// </summary>
        void OnStart();
    }
}
