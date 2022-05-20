using BinanceTrackerDesktop.Core.Localizations.Language.Name;

namespace BinanceTrackerDesktop.Core.Localizations.Language
{
    public class Language : ILanguage
    {
        public Language(LanguagesName name, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException(nameof(code));

            Name = name;
            Code = code;
        }



        public LanguagesName Name { get; }

        public string Code { get; }
    }
}
