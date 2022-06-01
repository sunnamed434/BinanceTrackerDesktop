using BinanceTrackerDesktop.Themes.Detectors.Data;
using BinanceTrackerDesktop.Themes.Models;
using System.Reflection;

namespace BinanceTrackerDesktop.Themes.Provider;

public sealed class ThemesProvider : IThemesProvider
{
    public IThemeDataDetector ThemeDetector { get; }



    public ThemesProvider(ThemeDataDetector themeDetector)
    {
        ThemeDetector = themeDetector ?? throw new ArgumentNullException(nameof(themeDetector));
    }



    public ThemeColors LoadThemeData()
    {
        ThemeColors themeColors = new ThemeColors();
        foreach (ThemeData themeData in ThemeDetector.DetectThemeDataRepository().GetThemeData())
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
