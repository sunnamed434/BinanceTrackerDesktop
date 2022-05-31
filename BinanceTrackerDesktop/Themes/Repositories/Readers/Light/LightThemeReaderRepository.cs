using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Themes.Models;
using Newtonsoft.Json;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Themes.ThemesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Themes.Repositories.Readers.Light;

public sealed class LightThemeReaderRepository : IThemeDataRepository
{
    public IEnumerable<ThemeData> GetThemeData()
    {
        string fileText = ApplicationDirectories.Resources.Themes.GetDirectoryFile(RegisteredThemes.LightTheme).GetStringResult();
        return JsonConvert.DeserializeObject<IEnumerable<ThemeData>>(fileText);
    }
}
