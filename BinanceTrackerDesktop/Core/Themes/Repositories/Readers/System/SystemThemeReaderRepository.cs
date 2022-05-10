using BinanceTrackerDesktop.Core.Themes.Models.Resource;
using BinanceTrackerDesktop.Core.Themes.Recognizers;
using BinanceTrackerDesktop.Core.Themes.Recognizers.Windows;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Exceptions;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Light;

namespace BinanceTrackerDesktop.Core.Themes.Repositories.Readers.System
{
    public sealed class SystemThemeReaderRepository : IThemeDataReaderRepository
    {
        public IEnumerable<ThemeData> GetThemeData()
        {
            ISystemThemeRecognizer themeRecognizer = new WindowsSystemThemeRecognizer();
            Theme theme = themeRecognizer.RecognizeTheme();
            if (theme.Equals(Theme.Light))
            {
                return new LightThemeReaderRepository().GetThemeData();
            }
            else if (theme.Equals(Theme.Dark))
            {
                return new DarkThemeReaderRepository().GetThemeData();
            }

            throw new ThemeCannotBeRecognizedException();
        }
    }
}
