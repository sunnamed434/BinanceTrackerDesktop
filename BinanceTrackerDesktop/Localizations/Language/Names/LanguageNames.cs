using BinanceTrackerDesktop.Localizations.Exceptions;

namespace BinanceTrackerDesktop.Localizations.Language.Names;

public sealed class LanguageNames
{
    public const string English = "English";

    public const string Russian = "Русский";

    public const string Ukranian = "Українська";



    public static Languages GetFromName(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException(nameof(text));
        }

        return text switch
        {
            English  => Languages.English,
            Russian  => Languages.Russian,
            Ukranian => Languages.Ukranian,
            _ => throw new LanguageNotSupportedException(text),
        };
    }
}
