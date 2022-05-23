namespace BinanceTrackerDesktop.Core.Validators
{
    public interface IStringValidator : IValidator
    {
        IStringValidator MinCharacters(int count);

        IStringValidator MaxCharacters(int count);

        IStringValidator ContentNotNullOrWhiteSpace();

        IStringValidator ThrowIfFailed(Type type);

        IStringValidator ThrowIfFailed(string message);

        IStringValidator ThrowIfFailed(Exception exception);
    }
}