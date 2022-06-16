using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Localizations.Codes;
using BinanceTrackerDesktop.Localizations.Language;
using Newtonsoft.Json;

namespace BinanceTrackerDesktop.Localizations.Localization
{
    public sealed class CustomLocalization : ILocalization
    {
        private readonly Languages language;



        public CustomLocalization(Languages language)
        {
            this.language = language;
        }



        public IDictionary<string, string> Load()
        {
                string jsonValue = ApplicationDirectories.Resources.Localizations.GetDirectoryFile(
                    LocalizationCodes.GetLocalizationCodeFromLanguage(language)).GetStringResult();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonValue);
        }
    }
}
