namespace BinanceTrackerDesktop.Core.Validation.Exception
{
    public class FailedStringValidationException : System.Exception
    {
        public FailedStringValidationException(string message) : base(string.Format("String failed validation of validator: {0}", message))
        {

        }

        public FailedStringValidationException()
        {

        }
    }
}
