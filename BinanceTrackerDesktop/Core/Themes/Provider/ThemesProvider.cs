using BinanceTrackerDesktop.Core.Themes.Detectors;
using BinanceTrackerDesktop.Core.Themes.Models.Data;
using BinanceTrackerDesktop.Core.Themes.Models.Resource;
using System.Reflection;

namespace BinanceTrackerDesktop.Core.Themes.Provider
{
    public sealed class ThemesProvider : IThemesProvider
    {
        public IThemeDetector ThemeDetector { get; }



        public ThemesProvider(IThemeDetector themeDetector)
        {
            ThemeDetector = themeDetector ?? throw new ArgumentNullException(nameof(themeDetector));
        }



        public LoadedThemeData LoadThemeData()
        {
            LoadedThemeData loadedThemeData = new LoadedThemeData();
            foreach (ThemeData themeData in ThemeDetector.GetThemeReaderRepository().GetThemeData())
            {
                foreach (FieldInfo fieldInfo in loadedThemeData.GetType().GetFields())
                {
                    if (themeData.Name == fieldInfo.Name)
                    {
                        fieldInfo.SetValue(loadedThemeData, themeData.HEX.GetColor());
                    }
                }
            }

            return loadedThemeData;
        }
    }
}
