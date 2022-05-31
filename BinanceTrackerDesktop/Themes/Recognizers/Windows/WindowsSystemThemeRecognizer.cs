using BinanceTrackerDesktop.Themes.Repositories.Readers.Exceptions;
using Microsoft.Win32;

namespace BinanceTrackerDesktop.Themes.Recognizers.Windows;

public sealed class WindowsSystemThemeRecognizer : ISystemThemeRecognizer
{
    private const string ThemesPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    private const string SystemUsesLightTheme = nameof(SystemUsesLightTheme);

    private const int LightThemeValue = 1;

    private const int DarkThemeValue = 0;



    public Theme RecognizeTheme()
    {
        RegistryKey registry = Registry.CurrentUser.OpenSubKey(ThemesPath);
        int themeValue = (int)registry.GetValue(SystemUsesLightTheme);

        return themeValue switch
        {
            LightThemeValue => Theme.Light,
            DarkThemeValue  => Theme.Dark,
            _ => throw new ThemeCannotBeRecognizedException(),
        };
    }
}
