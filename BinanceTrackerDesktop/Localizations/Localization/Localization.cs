using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Localizations.Models;
using BinanceTrackerDesktop.User.Data.Value;
using Newtonsoft.Json;

namespace BinanceTrackerDesktop.Localizations.Localization;

public sealed class Localization : ILocalization
{
    public TranslationsData Load()
    {
        string jsonValue = ApplicationDirectories.Resources.Localizations.GetDirectoryFile(UserDataValues.Language.GetValue().Code).GetStringResult();
        return JsonConvert.DeserializeObject<TranslationsData>(jsonValue);
    }
}
