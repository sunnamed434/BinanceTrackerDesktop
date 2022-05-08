using BinanceTrackerDesktop.Core.Themes.Detector;
using BinanceTrackerDesktop.Core.Themes.Models.Resource;

namespace BinanceTrackerDesktop.Core.Themes.Provider
{
    public interface IThemesProvider
    {
        IThemeDetector ThemeDetector { get; }



        IEnumerable<ThemeComponentResourceModel> LoadThemeJSONData();

        ThemeComponentResourceModel GetResourceModelByName(string value);
    }
}
