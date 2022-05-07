using BinanceTrackerDesktop.Core.Themes.Models.Resource;

namespace BinanceTrackerDesktop.Core.Themes.Repositories.Readers
{
    public interface IThemeReaderRepository
    {
        IEnumerable<ThemeComponentResourceModel> GetThemesDataFromReadedFile();
    }
}
