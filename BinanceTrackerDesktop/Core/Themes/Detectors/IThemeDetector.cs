using BinanceTrackerDesktop.Core.Themes.Recognizers;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Exceptions;

namespace BinanceTrackerDesktop.Core.Themes.Detectors
{
    /// <summary>
    /// User theme detector.
    /// </summary>
    public interface IThemeDetector
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
        IThemeDataRepository GetThemeReaderRepository();
    }
}
