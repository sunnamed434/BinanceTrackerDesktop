using BinanceTrackerDesktop.Themes.Detectors.Data;
using BinanceTrackerDesktop.Themes.Models;

namespace BinanceTrackerDesktop.Themes.Provider;

public interface IThemesProvider
{
    IThemeDataDetector ThemeDetector { get; }



    ThemeColors LoadThemeData();
}
