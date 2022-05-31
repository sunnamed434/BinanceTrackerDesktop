using BinanceTrackerDesktop.Localizations.Exceptions;
using BinanceTrackerDesktop.Localizations.Localization;
using BinanceTrackerDesktop.Localizations.Models;

namespace BinanceTrackerDesktop.Localizations.Translation;

public sealed class Translation : ITranslation
{
    public Translation(ILocalization localization)
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

        TranslationsData translationsData = Localization.Load();
        if (translationsData.Values.TryGetValue(key, out string value))
        {
            return value;
        }

        throw new TranslationKeyWasNotFoundException(nameof(key));
    }
}
