using BinanceTrackerDesktop.Core.Themes.Recognizers;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Exceptions;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Light;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.System;
using BinanceTrackerDesktop.Core.User.Data.Value;

namespace BinanceTrackerDesktop.Core.Themes.Detectors
{
    public sealed class ThemeDetector : IThemeDetector
    {
        public ThemeDetector(ISystemThemeRecognizer themeRecognizer)
        {
            ThemeRecognizer = themeRecognizer ?? throw new ArgumentNullException(nameof(themeRecognizer));
        }



        public ISystemThemeRecognizer ThemeRecognizer { get; }



        public IThemeDataRepository GetThemeReaderRepository()
        {
            Theme theme = UserDataValues.Theme.GetValue();
            return theme switch
            {
                Theme.System => new SystemThemeDataRepository(ThemeRecognizer),
                Theme.Light  => new LightThemeReaderRepository(),
                Theme.Dark   => new DarkThemeReaderRepository(),
                _ => throw new ThemeCannotBeRecognizedException()
            };
        }
    }
}
