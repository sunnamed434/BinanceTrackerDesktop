using BinanceTrackerDesktop.Core.Themes.Recognizers;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Exceptions;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Light;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.System;
using BinanceTrackerDesktop.Core.User.Data.Value.Repositories.Language;

namespace BinanceTrackerDesktop.Core.Themes.Detectors
{
    public sealed class ThemeDetector : IThemeDetector
    {
        public ThemeDetector(ThemeUserDataValueRepository themeRepository, ISystemThemeRecognizer themeRecognizer)
        {
            ThemeRepository = themeRepository ?? throw new ArgumentNullException(nameof(themeRepository));
            ThemeRecognizer = themeRecognizer ?? throw new ArgumentNullException(nameof(themeRecognizer));
        }



        public ThemeUserDataValueRepository ThemeRepository { get; }

        public ISystemThemeRecognizer ThemeRecognizer { get; }



        public IThemeDataRepository GetThemeReaderRepository()
        {
            Theme theme = ThemeRepository.GetValue();
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
