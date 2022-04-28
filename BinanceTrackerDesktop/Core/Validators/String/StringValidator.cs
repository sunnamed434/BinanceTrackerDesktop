using BinanceTrackerDesktop.Core.Validators.String.Exception;
using System;

namespace BinanceTrackerDesktop.Core.Validators
{
    public sealed class StringValidator : IStringValidator
    {
        private readonly string content;



        private bool success = true;



        public StringValidator(string content) 
        {
            this.content = content;
        }



        public bool IsSuccess => success == true;

        public bool IsFailed => IsSuccess == false;



        public IStringValidator MinCharacters(int count)
        {
            if (this.content.Length < count)
                success = false;

            return this;
        }

        public IStringValidator ContentNotNullOrEmpty()
        {
            if (string.IsNullOrEmpty(this.content))
                success = false;

            return this;
        }

        public IStringValidator ThrowIfFailed(Type type)
        {
            ThrowIfFailed(type.Name);

            return this;
        }

        public IStringValidator ThrowIfFailed(string message)
        {
            if (IsFailed)
                throw new FailedStringValidationException(message);

            return this;
        }

        public IStringValidator ThrowIfFailed(Exception exception)
        {
            if (IsFailed)
                throw exception;

            return this;
        }
    }
}
