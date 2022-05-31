using BinanceTrackerDesktop.Themes.Detectors;
using BinanceTrackerDesktop.Themes.Models;

namespace BinanceTrackerDesktop.Themes.Provider;

public interface IThemesProvider
{
    IThemeDetector ThemeDetector { get; }



    ThemeColors LoadThemeData();
}
