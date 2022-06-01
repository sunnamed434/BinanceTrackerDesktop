using BinanceTrackerDesktop.Themes.Recognizers.Windows;
using System.Runtime.InteropServices;

namespace BinanceTrackerDesktop.Themes.Recognizers.Provider;

public sealed class SystemThemeRecognizerProvider : ISystemThemeRecognizerProvider
{
    public ISystemThemeRecognizer Recognize()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return new WindowsSystemThemeRecognizer();
        }

        throw new PlatformNotSupportedException();
    }
}