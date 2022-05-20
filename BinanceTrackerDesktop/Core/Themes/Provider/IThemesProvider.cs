using BinanceTrackerDesktop.Core.Themes.Detectors;
using BinanceTrackerDesktop.Core.Themes.Models;

namespace BinanceTrackerDesktop.Core.Themes.Provider
{
    public interface IThemesProvider
    {
        IThemeDetector ThemeDetector { get; }



        ThemeColors LoadThemeData();
    }
}
