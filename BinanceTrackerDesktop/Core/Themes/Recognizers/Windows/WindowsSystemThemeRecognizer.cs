using Microsoft.Win32;

namespace BinanceTrackerDesktop.Core.Themes.Recognizers.Windows
{
    public sealed class WindowsSystemThemeRecognizer : ISystemThemeRecognizer
    {
        private const string ThemesPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        private const string SystemUsesLightTheme = "SystemUsesLightTheme";

        private const int LightThemeValue = 1;



        public Theme RecognizeTheme()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(ThemesPath);
            return (int)registry.GetValue(SystemUsesLightTheme) == LightThemeValue ? Theme.Light : Theme.Dark;
        }
    }
}
