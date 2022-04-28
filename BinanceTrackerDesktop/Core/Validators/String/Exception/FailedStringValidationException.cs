namespace BinanceTrackerDesktop.Core.Validators.String.Exception
{
    public sealed class FailedStringValidationException : System.Exception
    {
        public FailedStringValidationException(string argument) : base(string.Format("String validation failed: {0}", argument))
        {

        }

        public FailedStringValidationException() : this(default)
        {

        }
    }
}
