using System;

namespace BinanceTrackerDesktop.Core.Validation.API
{
    public class FailedStringValidationException : Exception
    {
        public FailedStringValidationException(string message) : base(string.Format("String failed validation of validator: {0}", message))
        {

        }

        public FailedStringValidationException()
        {

        }
    }
}
