namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component
{
    /// <summary>
    /// For awaitable component, an a interface that giving a special method when application closing started, and it wait`s while method doing things.
    /// <para>It set`s automatically at runtime via "Reflection".</para>
    /// </summary>
    public interface IAwaitableComponentExecute
    {
        /// <summary>
        /// Execute`s <see langword="async"/> when application is closing, its wait`s while you were not done with method execution.
        /// </summary>
        /// <returns><see cref="Task.CompletedTask"/></returns>
        Task OnExecute();
    }
}
