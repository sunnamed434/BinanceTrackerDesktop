namespace BinanceTrackerDesktop.Core.Formatters
{
    public interface IFormatter<TArgument>
    {
        object Format(TArgument argument);
    }
}
