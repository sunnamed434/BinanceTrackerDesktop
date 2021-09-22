using System.Collections.Generic;

namespace BinanceTrackerDesktop.Core.Components.API
{
    public interface IExpandableComponent<TAddGetRemove, TSearchArgument>
    {
        IEnumerable<TAddGetRemove> AllComponents { get; }



        void AddComponent(TAddGetRemove value);

        void AddComponents(IEnumerable<TAddGetRemove> values);

        void RemoveComponent(TAddGetRemove value);

        TAddGetRemove GetComponentAt(TSearchArgument value);
    }
}
