using BinanceTrackerDesktop.Core.Validators.String.Exception;

namespace BinanceTrackerDesktop.Core.Validators
{
    public sealed class StringValidator : IStringValidator
    {
        private readonly string content;



        private bool success;



        public StringValidator(string content) 
        {
            this.content = content;
            success = true;
        }



        public bool IsSuccess => success == true;

        public bool IsFailed => IsSuccess == false;



        public IStringValidator MinCharacters(int count)
        {
            if (this.content.Length < count)
                success = false;

            return this;
        }

        public IStringValidator MaxCharacters(int count)
        {
            if (this.content.Length > count)
                success = false;

            return this;
        }

        public IStringValidator ContentNotNullOrEmpty()
        {
            if (string.IsNullOrWhiteSpace(this.content))
                success = false;

            return this;
        }

        public IStringValidator ThrowIfFailed(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            ThrowIfFailed(type.Name);

            return this;
        }

        public IStringValidator ThrowIfFailed(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (IsFailed)
                return ThrowIfFailed(new FailedStringValidationException(message));

            return this;
        }

        public IStringValidator ThrowIfFailed(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            if (IsFailed)
                throw exception;

            return this;
        }
    }
}
