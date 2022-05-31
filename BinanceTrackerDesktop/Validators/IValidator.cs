namespace BinanceTrackerDesktop.Validators;

public interface IValidator
{
    bool IsFailed { get; }

    bool IsSuccess { get; }
}
