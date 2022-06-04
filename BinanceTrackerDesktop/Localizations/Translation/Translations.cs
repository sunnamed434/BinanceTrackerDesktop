using BinanceTrackerDesktop.Localizations.Exceptions;
using BinanceTrackerDesktop.Localizations.Localization;

namespace BinanceTrackerDesktop.Localizations.Translation;

public sealed class Translations : ITranslations
{
    public Translations(ILocalization localization)
    {
        Localization = localization ?? throw new ArgumentNullException(nameof(localization));
    }



    public ILocalization Localization { get; }



    public string Translate(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException(nameof(key));
        }

        IDictionary<string, string> localization = Localization.Load();
        if (localization.TryGetValue(key, out string value))
        {
            return value;
        }

        throw new TranslationKeyWasNotFoundException(key);
    }
}
