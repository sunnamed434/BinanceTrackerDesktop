using System;
using static BinanceTrackerDesktop.Core.Window.External.ExternalWindowControl;

namespace BinanceTrackerDesktop.Core.Window
{
    public sealed class WindowControl
    {
        public static bool Show(IntPtr handle, WindowCommand command) => ShowWindowAsync(handle, (int)command);

        public static bool GetIsMinimized(IntPtr handle) => IsIconic(handle);

        public static bool SetToForeground(IntPtr handle) => SetForegroundWindow(handle);



        public enum WindowCommand : int
        {
            Hide = 0,
            ShowNormal = 1,
            ShowMinimized = 2,
            ShowMaximazed = 3,
            ShowNonActive = 4,
            Show = 5,
            Restore = 9,
            ShowDefault = 10,
        }
    }
}
