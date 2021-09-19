using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BinanceTrackerDesktop.Core.Window.API
{
    public interface IProcessWindow
    {
        void SetWindowToForeground();
    }

    public class ProcessWindow : IProcessWindow
    {
        public void SetWindowToForeground()
        {
            Process.GetCurrentProcess()?.SetProcessWindowToForeground();
        }
    }

    public class WindowControl
    {
        [DllImport("user32.dll")] private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")] private static extern bool IsIconic(IntPtr hWnd);



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

    public static class ProcessExtension
    {
        private const int ContainsSimiralWindowValue = 1;



        public static bool TryGetArleadyStartedSimilarProcess(this Process source, out Process anotherProcess)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Process[] processes = Process.GetProcessesByName(source.ProcessName);
            if (processes.Length > ContainsSimiralWindowValue)
            {
                int processIndex = 0;
                if (processes[processIndex].Id == source.Id)
                {
                    processIndex++;
                }

                return (anotherProcess = processes[processIndex]) != null;
            }

            anotherProcess = null;
            return false;
        }

        public static void SetProcessWindowToForeground(this Process source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IntPtr mainWindowHandle = source.MainWindowHandle;
            if (WindowControl.GetIsMinimized(mainWindowHandle))
            {
                WindowControl.Show(mainWindowHandle, WindowControl.WindowCommand.Restore);
            }

            WindowControl.SetToForeground(mainWindowHandle);
        }

        public static void UnhideSimilarProcess(this Process source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (TryGetArleadyStartedSimilarProcess(source, out Process anotherProcess))
            {
                SetProcessWindowToForeground(anotherProcess);
            }
        }
    }
}
