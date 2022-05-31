using BinanceTrackerDesktop.Localizations.Localization;

namespace BinanceTrackerDesktop.Localizations.Translation;

public interface ITranslation
{
    /// <summary>
    /// Gets the localization.
    /// </summary>
    ILocalization Localization { get; }



    /// <summary>
    /// Translates the given <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The value from translated <paramref name="key"/>.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exceptions.TranslationKeyWasNotFoundException"></exception>
    string Translate(string key);
}