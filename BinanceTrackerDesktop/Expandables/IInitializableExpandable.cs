namespace BinanceTrackerDesktop.Expandables;

public interface IInitializableExpandable<TAddGetRemove, TSearchArgument>
{
    IEnumerable<KeyValuePair<TSearchArgument, TAddGetRemove>> InitializeItems();
}
