namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component
{
    /// <summary>
    /// For awaitable component, an a interface that giving a special method when application closing started.
    /// <para>It executes automatically at runtime via <see langword="Reflection"/></para>
    /// </summary>
    public interface IAwaitableComponentExecute
    {
        /// <summary>
        /// Execute`s when application is closing.
        /// </summary>
        void OnExecute();
    }
}
