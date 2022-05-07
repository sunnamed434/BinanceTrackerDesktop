using BinanceTrackerDesktop.Core.Themes.Detector;
using BinanceTrackerDesktop.Core.Themes.Models.Resource;
using BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Dark;

namespace BinanceTrackerDesktop.Core.Themes.Provider
{
    public sealed class ThemesProvider : IThemesProvider
    {
        public IThemeDetector ThemeDetector { get; }



        public ThemesProvider(IThemeDetector themeDetector)
        {
            ThemeDetector = themeDetector ?? throw new ArgumentNullException(nameof(themeDetector));
        }



        public IEnumerable<ThemeComponentResourceModel> LoadThemeJSONData()
        {
            return new DarkThemeReaderRepository().GetThemesDataFromReadedFile();
            //return ThemeDetector.GetThemeReaderRepository().GetThemesDataFromReadedFile();
        }

        /*public void ApplyTheme()
        {
            foreach ((ThemeComponentAttribute, Control) dataPair in CustomAttributesUtility.GetCustomAttributesFromFields<ThemeComponentAttribute, Control>
                (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                //Control.FromHandle()
                foreach (ThemeComponentResourceModel resource in LoadThemeJSONData())
                {
                    if (dataPair.Item1.ComponentNameStringModel.Name.Equals(resource.NameString.Name))
                    {
                        MessageBox.Show("found!, " + resource.NameString);
                    }
                }
            }
        }*/
    }
}
