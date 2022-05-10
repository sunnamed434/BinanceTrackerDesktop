using BinanceTrackerDesktop.Core.Themes.Detectors;
using BinanceTrackerDesktop.Core.Themes.Models.Data;

namespace BinanceTrackerDesktop.Core.Themes.Provider
{
    public interface IThemesProvider
    {
        IThemeDetector ThemeDetector { get; }



        LoadedThemeData LoadThemeData();
    }
}
