using BinanceTrackerDesktop.Core.Themes.Models.Resource;

namespace BinanceTrackerDesktop.Core.Themes.Repositories.Readers
{
    /// <summary>
    /// Repository for reading custom theme data.
    /// </summary>
    public interface IThemeDataReaderRepository
    {
        /// <summary>
        /// Reading theme data.
        /// </summary>
        /// <returns>Readed theme data.</returns>
        IEnumerable<ThemeData> GetThemeData();
    }
}
