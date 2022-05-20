using BinanceTrackerDesktop.Core.Themes.Detectors;
using BinanceTrackerDesktop.Core.Themes.Models;
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



        public ThemeColors LoadThemeData()
        {
            ThemeColors themeColors = new ThemeColors();
            foreach (ThemeData themeData in ThemeDetector.GetThemeReaderRepository().GetThemeData())
            {
                foreach (FieldInfo fieldInfo in themeColors.GetType().GetFields())
                {
                    if (themeData.Name == fieldInfo.Name)
                    {
                        fieldInfo.SetValue(themeColors, themeData.HEX.GetColor());
                    }
                }
            }

            return themeColors;
        }
    }
}
