using BinanceTrackerDesktop.Core.Themes.Detector;
using BinanceTrackerDesktop.Core.Themes.Models.Resource;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;

namespace BinanceTrackerDesktop.Core.Themes.Provider
{
    public sealed class ThemesProvider : IThemesProvider
    {
        public IThemeDetector ThemeDetector { get; }



        public ThemesProvider(IThemeDetector themeDetector)
        {
            ThemeDetector = themeDetector ?? throw new ArgumentNullException(nameof(themeDetector));
        }



        public IEnumerable<ThemeComponentResourceModel> LoadThemeJSONData()
        {
            return new DarkThemeReaderRepository().GetThemesDataFromReadedFile();
            //return ThemeDetector.GetThemeReaderRepository().GetThemesDataFromReadedFile();
        }

        public ThemeComponentResourceModel GetResourceModelByName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            return LoadThemeJSONData().FirstOrDefault(r => r.Name.Equals(value));
        }
    }
}
