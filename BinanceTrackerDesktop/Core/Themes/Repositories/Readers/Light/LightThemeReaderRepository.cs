using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Themes.Models.Resource;
using Newtonsoft.Json;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.Themes.DirectoryThemesControl;

namespace BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Light
{
    public sealed class LightThemeReaderRepository : IThemeReaderRepository
    {
        public IEnumerable<ThemeComponentResourceModel> GetThemesDataFromReadedFile()
        {
            string fileText = new ApplicationDirectoriesControl().Folders.Resources.Themes.GetDirectoryFile(RegisteredThemes.LightTheme).GetStringResult();
            return JsonConvert.DeserializeObject<IEnumerable<ThemeComponentResourceModel>>(fileText);
        }
    }
}
