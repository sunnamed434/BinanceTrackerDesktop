using BinanceTrackerDesktop.Themes.Recognizers;
using BinanceTrackerDesktop.Themes.Repositories.Readers;
using BinanceTrackerDesktop.Themes.Repositories.Readers.Exceptions;

namespace BinanceTrackerDesktop.Themes.Detectors.Data;

/// <summary>
/// User theme detector.
/// </summary>
public interface IThemeDataDetector
{
    /// <summary>
    /// The system theme recognizer.
    /// </summary>
    ISystemThemeRecognizer ThemeRecognizer { get; }



    /// <summary>
    /// Getting theme and reading it from <see cref="ThemeRepository"/>
    /// </summary>
    /// <returns>Instance to the theme data reader.</returns>
    /// <exception cref="ThemeCannotBeRecognizedException"></exception>
    IThemeDataRepository DetectThemeDataRepository();
}
