using BinanceTrackerDesktop.Window.Extension;
using System.Diagnostics;

namespace BinanceTrackerDesktop.Window.Helper;

public sealed class ProcessWindowHelper : IProcessWindowHelper
{
    public void SetWindowToForeground()
    {
        Process.GetCurrentProcess()?.SetProcessWindowToForeground();
    }
}
