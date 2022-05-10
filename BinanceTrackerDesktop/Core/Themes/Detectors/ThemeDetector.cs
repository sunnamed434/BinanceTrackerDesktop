using BinanceTrackerDesktop.Core.Themes.Repositories.Readers;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Exceptions;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Light;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.System;
using BinanceTrackerDesktop.Core.User.Theme.Repositories;

namespace BinanceTrackerDesktop.Core.Themes.Detectors
{
    public sealed class ThemeDetector : IThemeDetector
    {
        public IUserThemeRepository UserThemeRepository { get; }



        public ThemeDetector(IUserThemeRepository userThemeRepository)
        {
            UserThemeRepository = userThemeRepository ?? throw new ArgumentNullException(nameof(userThemeRepository));
        }



        public IThemeDataReaderRepository GetThemeReaderRepository()
        {
            Theme theme = UserThemeRepository.GetTheme();
            if (theme.Equals(Theme.System))
                return new SystemThemeReaderRepository();

            if (theme.Equals(Theme.Light))
                return new LightThemeReaderRepository();

            if (theme.Equals(Theme.Dark))
                return new DarkThemeReaderRepository();

            throw new ThemeCannotBeRecognizedException();
        }
    }
}
