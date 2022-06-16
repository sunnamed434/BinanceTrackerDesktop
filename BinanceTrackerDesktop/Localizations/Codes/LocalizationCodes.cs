using BinanceTrackerDesktop.Localizations.Exceptions;
using BinanceTrackerDesktop.Localizations.Language;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Localizations.LocalizationsDirectoryFilesControl;

namespace BinanceTrackerDesktop.Localizations.Codes;

public sealed class LocalizationCodes
{
    public static string GetLocalizationCodeFromLanguage(Languages name)
    {
        return name switch
        {
            Languages.English  => RegisteredLocalizations.English,
            Languages.Russian  => RegisteredLocalizations.Russian,
            Languages.Ukranian => RegisteredLocalizations.Ukrainian,
            _ => throw new LanguageNotSupportedException(),
        };
    }
}
