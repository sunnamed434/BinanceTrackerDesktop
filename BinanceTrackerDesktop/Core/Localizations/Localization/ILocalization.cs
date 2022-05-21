using BinanceTrackerDesktop.Core.Localizations.Models;
using BinanceTrackerDesktop.Core.User.Data.Value.Repositories.Language;

namespace BinanceTrackerDesktop.Core.Localizations.Localization
{
    public interface ILocalization
    {
        TranslationsData Load();
    }
}