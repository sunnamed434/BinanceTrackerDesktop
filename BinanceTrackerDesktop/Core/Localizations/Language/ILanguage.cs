using BinanceTrackerDesktop.Core.Localizations.Language.Name;

namespace BinanceTrackerDesktop.Core.Localizations.Language
{
    public interface ILanguage
    {
        LanguagesName Name { get; }

        string Code { get; }
    }
}