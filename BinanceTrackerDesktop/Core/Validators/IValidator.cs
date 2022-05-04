namespace BinanceTrackerDesktop.Core.Validators
{
    public interface IValidator
    {
        bool IsFailed { get; }

        bool IsSuccess { get; }
    }
}
