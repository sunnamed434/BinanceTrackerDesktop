namespace BinanceTrackerDesktop.Awaitable.Awaitables;

/// <summary>
/// For awaitable component, an a interface that giving a special method when application closing started.
/// <para>It executes automatically at runtime via <see langword="Reflection"/></para>
/// </summary>
public interface IAwaitableExecute
{
    /// <summary>
    /// Execute`s when application is closing.
    /// </summary>
    void OnExecute();
}
