using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Exceptions;

namespace BinanceTrackerDesktop.Core.Themes.Recognizers
{
    /// <summary>
    /// System theme recognizer.
    /// </summary>
    public interface ISystemThemeRecognizer
    {
        /// <summary>
        /// Recognizing system theme.
        /// </summary>
        /// <returns>Recognized theme.</returns>
        /// <exception cref="ThemeCannotBeRecognizedException"></exception>
        Theme RecognizeTheme();
    }
}
