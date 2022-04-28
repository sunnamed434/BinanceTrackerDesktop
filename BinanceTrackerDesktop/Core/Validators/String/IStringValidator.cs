using System;

namespace BinanceTrackerDesktop.Core.Validators
{
    public interface IStringValidator
    {
        bool IsSuccess { get; }

        bool IsFailed { get; }



        IStringValidator MinCharacters(int count);

        IStringValidator ContentNotNullOrEmpty();

        IStringValidator ThrowIfFailed(Type type);

        IStringValidator ThrowIfFailed(string message);

        IStringValidator ThrowIfFailed(Exception exception);
    }
}