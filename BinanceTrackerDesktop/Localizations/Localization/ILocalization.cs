using BinanceTrackerDesktop.Localizations.Models;

namespace BinanceTrackerDesktop.Localizations.Localization;

public interface ILocalization
{
    TranslationsData Load();
}