﻿using BinanceTrackerDesktop.Awaitable.Observer;

namespace BinanceTrackerDesktop.Awaitable.Awaitables;

/// <summary>
/// For awaitable component, an a interface that giving a special property instance.
/// <para>It executes automatically at runtime via <see langword="Reflection"/></para>
/// </summary>
public interface IAwaitableObserverInstance
{
    /// <summary>
    /// Instance to the observer.
    /// </summary>
    IAwaitablesObserver Observer { get; set; }
}
