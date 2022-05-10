using BinanceTrackerDesktop.Core.Themes.Repositories.Readers;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Exceptions;
using BinanceTrackerDesktop.Core.User.Theme.Repositories;

namespace BinanceTrackerDesktop.Core.Themes.Detectors
{
    /// <summary>
    /// User theme detector.
    /// </summary>
    public interface IThemeDetector
    {
        /// <summary>
        /// User theme repository.
        /// </summary>
        IUserThemeRepository UserThemeRepository { get; }



        /// <summary>
        /// Getting theme and reading it from <see cref="UserThemeRepository"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ThemeCannotBeRecognizedException"></exception>
        IThemeDataReaderRepository GetThemeReaderRepository();
    }
}
