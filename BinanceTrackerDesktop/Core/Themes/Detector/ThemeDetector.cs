using BinanceTrackerDesktop.Core.Themes.Repositories.Readers;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Light;
using BinanceTrackerDesktop.Core.User.Theme.Repositories;

namespace BinanceTrackerDesktop.Core.Themes.Detector
{
    public sealed class ThemeDetector : IThemeDetector
    {
        public IUserThemeRepository UserThemeRepository { get; }



        public IThemeReaderRepository GetThemeReaderRepository()
        {
            Theme theme = UserThemeRepository.GetTheme();
            if (theme.Equals(Theme.Light))
                return new LightThemeReaderRepository();

            if (theme.Equals(Theme.Dark))
                return new DarkThemeReaderRepository();

            return null;
        }
    }
}
