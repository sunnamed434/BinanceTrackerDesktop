namespace BinanceTrackerDesktop.Formatters;

public interface IFormatter<TArgument>
{
    object Format(TArgument argument);
}
