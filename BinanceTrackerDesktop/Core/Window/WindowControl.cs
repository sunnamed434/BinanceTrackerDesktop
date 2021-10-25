using System;
using System.Runtime.InteropServices;

namespace BinanceTrackerDesktop.Core.Window
{
    internal sealed class WindowControl
    {
        [DllImport("user32.dll")] internal static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")] internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")] internal static extern bool IsIconic(IntPtr hWnd);



        internal static bool Show(IntPtr handle, WindowCommand command) => ShowWindowAsync(handle, (int)command);

        internal static bool GetIsMinimized(IntPtr handle) => IsIconic(handle);

        internal static bool SetToForeground(IntPtr handle) => SetForegroundWindow(handle);



        internal enum WindowCommand : int
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
