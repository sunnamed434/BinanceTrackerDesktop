using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;
using BinanceTrackerDesktop.Themes.Recognizers;
using BinanceTrackerDesktop.Themes.Repositories.Readers;
using BinanceTrackerDesktop.Themes.Repositories.Readers.Exceptions;
using BinanceTrackerDesktop.Themes.Repositories.Readers.Light;
using BinanceTrackerDesktop.Themes.Repositories.Readers.System;
using BinanceTrackerDesktop.User.Data.Value;

namespace BinanceTrackerDesktop.Themes.Detectors.Data;

public sealed class ThemeDataDetector : IThemeDataDetector
{
    public ThemeDataDetector(ISystemThemeRecognizer themeRecognizer)
    {
        ThemeRecognizer = themeRecognizer ?? throw new ArgumentNullException(nameof(themeRecognizer));
    }



    public ISystemThemeRecognizer ThemeRecognizer { get; }



    public IThemeDataRepository DetectThemeDataRepository()
    {
        Theme theme = UserDataValues.Theme.GetValue();
        return theme switch
        {
            Theme.System => new SystemThemeDataRepository(ThemeRecognizer),
            Theme.Light => new LightThemeReaderRepository(),
            Theme.Dark => new DarkThemeReaderRepository(),
            _ => throw new ThemeCannotBeRecognizedException()
        };
    }
}
