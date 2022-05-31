namespace BinanceTrackerDesktop.Validators.String.Exceptions;

public sealed class FailedStringValidationException : Exception
{
    public FailedStringValidationException(string argument) : base(string.Format("String validation failed: {0}", argument))
    {
    }

    public FailedStringValidationException() : this(string.Empty)
    {
    }
}
