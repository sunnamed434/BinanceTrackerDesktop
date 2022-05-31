using System.Runtime.InteropServices;

namespace BinanceTrackerDesktop.Window.External;

internal sealed class ExternalWindowControl
{
    [DllImport("user32.dll")] internal static extern bool ShowWindowAsync(IntPtr handle, int command);

    [DllImport("user32.dll")] internal static extern bool SetForegroundWindow(IntPtr handle);

    [DllImport("user32.dll")] internal static extern bool IsIconic(IntPtr handle);
}
