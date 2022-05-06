using BinanceTrackerDesktop.Core.Themes.Repositories.Readers;
using BinanceTrackerDesktop.Core.User.Theme.Repositories;

namespace BinanceTrackerDesktop.Core.Themes.Detector
{
    public interface IThemeDetector
    {
        IUserThemeRepository UserThemeRepository { get; }



        IThemeReaderRepository GetThemeReaderRepository();
    }
}
