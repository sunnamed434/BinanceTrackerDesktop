using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Localizations.Models;
using BinanceTrackerDesktop.Core.User.Data.Value;
using Newtonsoft.Json;

namespace BinanceTrackerDesktop.Core.Localizations.Localization
{
    public sealed class Localization : ILocalization
    {
        public TranslationsData Load()
        {
            string jsonValue = ApplicationDirectories.Resources.Localizations.GetDirectoryFile(UserDataValues.Language.GetValue().Code).GetStringResult();
            return JsonConvert.DeserializeObject<TranslationsData>(jsonValue);
        }
    }
}
