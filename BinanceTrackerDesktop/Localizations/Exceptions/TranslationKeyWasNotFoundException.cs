namespace BinanceTrackerDesktop.Localizations.Exceptions;

public sealed class TranslationKeyWasNotFoundException : Exception
{
    public TranslationKeyWasNotFoundException(string message) : base(string.Format("Given translation key cannot be found: {0}", message))
    {
    }

    public TranslationKeyWasNotFoundException()
    {
    }
}
