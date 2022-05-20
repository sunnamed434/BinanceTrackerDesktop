using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Localizations.Models;
using BinanceTrackerDesktop.Core.User.Data.Value.Repositories.Language;
using Newtonsoft.Json;

namespace BinanceTrackerDesktop.Core.Localizations.Localization
{
    public sealed class Localization : ILocalization
    {
        public Localization(LanguageUserDataValueRepository languageRepository)
        {
            LanguageRepository = languageRepository ?? throw new ArgumentNullException(nameof(languageRepository));
        }



        public LanguageUserDataValueRepository LanguageRepository { get; }



        public TranslationsData Load()
        {
            string jsonValue = new ApplicationDirectoriesControl().Folders.Resources.Localizations.GetDirectoryFile(LanguageRepository.GetValue().Code).GetStringResult();
            return JsonConvert.DeserializeObject<TranslationsData>(jsonValue);
        }
    }
}
