using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Themes.Models;
using BinanceTrackerDesktop.Themes.Repositories.Readers;
using Newtonsoft.Json;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Themes.ThemesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;

public sealed class DarkThemeReaderRepository : IThemeDataRepository
{
    public IEnumerable<ThemeData> GetThemeData()
    {
        string fileText = ApplicationDirectories.Resources.Themes.GetDirectoryFile(RegisteredThemes.DarkTheme).GetStringResult();
        return JsonConvert.DeserializeObject<IEnumerable<ThemeData>>(fileText);
    }
}
