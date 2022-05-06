using BinanceTrackerDesktop.Core.Themes.Detector;
using BinanceTrackerDesktop.Core.Themes.Models.Resource;

namespace BinanceTrackerDesktop.Core.Themes.Provider
{
    internal interface IThemesProvider
    {
        IThemeDetector ThemeDetector { get; }



        IEnumerable<ThemeComponentResourceModel> LoadThemeJSONData();
    }
}
