using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Themes.Models;
using Newtonsoft.Json;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Themes.ThemesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark
{
    public sealed class DarkThemeReaderRepository : IThemeDataRepository
    {
        public IEnumerable<ThemeData> GetThemeData()
        {
            string fileText = new ApplicationDirectoriesControl().Folders.Resources.Themes.GetDirectoryFile(RegisteredThemes.DarkTheme).GetStringResult();
            return JsonConvert.DeserializeObject<IEnumerable<ThemeData>>(fileText);
        }
    }
}
