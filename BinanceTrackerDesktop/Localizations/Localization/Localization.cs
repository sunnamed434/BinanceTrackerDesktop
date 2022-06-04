using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Localizations.Codes;
using BinanceTrackerDesktop.User.Data.Value;
using Newtonsoft.Json;

namespace BinanceTrackerDesktop.Localizations.Localization;

public sealed class Localization : ILocalization
{
    public IDictionary<string, string> Load()
    {
        string jsonValue = ApplicationDirectories.Resources.Localizations.GetDirectoryFile(
            LocalizationCodes.GetLocalizationCodeFromLanguage(UserDataValues.Language.GetValue())).GetStringResult();
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonValue);
    }
}
