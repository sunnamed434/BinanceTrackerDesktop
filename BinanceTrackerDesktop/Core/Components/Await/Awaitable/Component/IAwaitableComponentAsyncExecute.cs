namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component
{
    /// <summary>
    /// For awaitable component, an a interface that giving a special method when application closing started, and it wait`s while method doing things.
    /// <para>It executes automatically at runtime via <see langword="Reflection"/></para>
    /// </summary>
    public interface IAwaitableComponentAsyncExecute
    {
        /// <summary>
        /// Execute`s <see langword="async"/> when application is closing, its wait`s while you were not done with method execution.
        /// </summary>
        /// <returns><see cref="Task.CompletedTask"/></returns>
        Task OnExecuteAsync();
    }
}
