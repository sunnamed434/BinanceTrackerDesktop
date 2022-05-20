using BinanceTrackerDesktop.Core.Localizations.Language.Name;

namespace BinanceTrackerDesktop.Core.Localizations.Language
{
    public sealed class Languages
    {
        public static readonly ILanguage English = GetLanguage(LanguagesName.English);
                                                
        public static readonly ILanguage Russian = GetLanguage(LanguagesName.Russian);



        private static IList<ILanguage> all = new List<ILanguage>
        {
            new Language(LanguagesName.English, "en"),
            new Language(LanguagesName.Russian, "ru")
        };



        public static ILanguage GetLanguage(LanguagesName name)
        {
            return all.FirstOrDefault(l => l.Name.Equals(name));
        }
    }
}
