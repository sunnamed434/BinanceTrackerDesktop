namespace BinanceTrackerDesktop.Core.Validator.String.Exception
{
    public class FailedStringValidationException : System.Exception
    {
        public FailedStringValidationException(string message) : base(string.Format("String validation failed: {0}", message))
        {

        }

        public FailedStringValidationException()
        {

        }
    }
}
