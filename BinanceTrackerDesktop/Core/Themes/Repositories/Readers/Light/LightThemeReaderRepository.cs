using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Themes.Models;
using Newtonsoft.Json;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Themes.ThemesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Light
{
    public sealed class LightThemeReaderRepository : IThemeDataRepository
    {
        public IEnumerable<ThemeData> GetThemeData()
        {
            string fileText = new ApplicationDirectoriesControl().Folders.Resources.Themes.GetDirectoryFile(RegisteredThemes.LightTheme).GetStringResult();
            return JsonConvert.DeserializeObject<IEnumerable<ThemeData>>(fileText);
        }
    }
}
