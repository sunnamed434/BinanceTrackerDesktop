using BinanceTrackerDesktop.Expandable;

namespace BinanceTrackerDesktop.Expandables;

public abstract class Expandable<TAddGetRemove, TSearchArgument> : IExpandable<TAddGetRemove, TSearchArgument>
{
    protected IDictionary<TSearchArgument, TAddGetRemove> Components;



    public Expandable()
    {
        Components = new Dictionary<TSearchArgument, TAddGetRemove>();
    }



    public IDictionary<TSearchArgument, TAddGetRemove> AllComponents => Components;



    public virtual void AddComponent(TAddGetRemove item, TSearchArgument search)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        Components.Add(search, item);
    }

    public virtual void AddComponents(IInitializableExpandable<TAddGetRemove, TSearchArgument> initializableExpandable)
    {
        if (initializableExpandable == null)
        {
            throw new ArgumentNullException(nameof(initializableExpandable));
        }

        AddComponents(initializableExpandable.InitializeItems());
    }

    public virtual void AddComponents(IDictionary<TSearchArgument, TAddGetRemove> items)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        if (items.Any() == false)
        {
            throw new InvalidOperationException();
        }

        foreach (KeyValuePair<TSearchArgument, TAddGetRemove> keyValuePairItem in items)
        {
            AddComponent(keyValuePairItem.Value, keyValuePairItem.Key);
        }
    }

    public virtual void AddComponents(IEnumerable<KeyValuePair<TSearchArgument, TAddGetRemove>> items)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        if (items.Any() == false)
        {
            throw new InvalidOperationException();
        }

        foreach (KeyValuePair<TSearchArgument, TAddGetRemove> keyValuePairItem in items)
        {
            AddComponent(keyValuePairItem.Value, keyValuePairItem.Key);
        }
    }

    public virtual TAddGetRemove GetComponentOrDefault(TSearchArgument search)
    {
        if (AllComponents.TryGetValue(search, out TAddGetRemove item))
        {
            return item;
        }

        return default(TAddGetRemove);
    }

    public virtual void RemoveComponent(TSearchArgument search)
    {
        Components.Remove(search);
    }
}
