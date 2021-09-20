using System.Collections.Generic;

namespace BinanceTrackerDesktop.Core.Components.API
{
    public interface IExpandableComponent<TAddRemove, TSearchArgument>
    {
        IEnumerable<TAddRemove> AllComponents { get; }



        void AddComponent(TAddRemove value);

        void RemoveComponent(TAddRemove value);

        TAddRemove GetComponentAt(TSearchArgument value);
    }
}
