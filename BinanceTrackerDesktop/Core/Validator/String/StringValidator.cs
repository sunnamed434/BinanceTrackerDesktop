using BinanceTrackerDesktop.Core.Validator.String.Exception;
using System;

namespace BinanceTrackerDesktop.Core.Validator
{
    public sealed class StringValidator
    {
        private readonly string content;



        private bool success = true;



        public bool IsSuccess => success == true;

        public bool IsFailed => IsSuccess == false;



        public StringValidator(string content) 
        {
            this.content = content;
        }



        public StringValidator MinCharacters(int count)
        {
            if (this.content.Length < count)
                success = false;

            return this;
        }

        public StringValidator ContentNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(this.content))
                success = false;

            return this;
        }

        public StringValidator ThrowIfFailed(Type type)
        {
            ThrowIfFailed(type.Name);

            return this;
        }

        public StringValidator ThrowIfFailed(string message)
        {
            if (IsFailed)
                throw new FailedStringValidationException(message);

            return this;
        }

        public StringValidator ThrowIfFailed(Exception exception)
        {
            if (IsFailed)
                throw exception;

            return this;
        }
    }
}
