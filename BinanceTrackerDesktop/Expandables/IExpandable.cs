using BinanceTrackerDesktop.Expandables;

namespace BinanceTrackerDesktop.Expandable;

public interface IExpandable<TAddGetRemove, TSearchArgument>
{
    IDictionary<TSearchArgument, TAddGetRemove> AllComponents { get; }



    void AddComponent(TAddGetRemove value, TSearchArgument search);

    void AddComponents(IEnumerable<KeyValuePair<TSearchArgument, TAddGetRemove>> items);

    void AddComponents(IInitializableExpandable<TAddGetRemove, TSearchArgument> initializableExpandable);

    void AddComponents(IDictionary<TSearchArgument, TAddGetRemove> values);

    void RemoveComponent(TSearchArgument search);

    TAddGetRemove GetComponentOrDefault(TSearchArgument search);
}
