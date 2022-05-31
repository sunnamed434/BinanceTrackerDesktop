using BinanceTrackerDesktop.Themes.Models;

namespace BinanceTrackerDesktop.Themes.Repositories.Readers;

/// <summary>
/// Repository for reading custom theme data.
/// </summary>
public interface IThemeDataRepository
{
    /// <summary>
    /// Reading theme data.
    /// </summary>
    /// <returns>Readed theme data.</returns>
    IEnumerable<ThemeData> GetThemeData();
}
