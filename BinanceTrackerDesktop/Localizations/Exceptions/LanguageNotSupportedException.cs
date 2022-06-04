using BinanceTrackerDesktop.Localizations.Language;

namespace BinanceTrackerDesktop.Localizations.Exceptions;

public sealed class LanguageNotSupportedException : Exception
{
    public LanguageNotSupportedException(Languages name) : base(string.Format("Given language is not supported: {0}", name))
    {
    }

    public LanguageNotSupportedException()
    {
    }
}
