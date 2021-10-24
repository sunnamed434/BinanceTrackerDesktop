namespace BinanceTrackerDesktop.Core.Validator.String.Exception
{
    public class FailedStringValidationException : System.Exception
    {
        public FailedStringValidationException(string argument) : base(string.Format("String validation failed: {0}", argument))
        {

        }

        public FailedStringValidationException() : this(default)
        {

        }
    }
}
