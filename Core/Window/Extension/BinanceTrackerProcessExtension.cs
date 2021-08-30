using BinanceTrackerDesktop.Core.Window.Handle;
using System;
using System.Diagnostics;

namespace BinanceTrackerDesktop.Core.Window.Extension
{
    public static class BinanceTrackerProcessExtension
    {
        private const int FirstArrayElement = 0;

        private const int ContainsSameWindow = 1;



        public static bool TryGetArleadyStartedSimilarProcess(this Process source, out Process anotherProcess)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Process[] processes = Process.GetProcessesByName(source.ProcessName);
            if (processes.Length > ContainsSameWindow)
            {
                int processIndex = 0;
                if (processes[FirstArrayElement].Id == source.Id)
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
                throw new ArgumentNullException(nameof(source));

            IntPtr mainWindowHandle = source.MainWindowHandle;
            if (WindowHandle.GetIsMinimized(mainWindowHandle))
                WindowHandle.Show(mainWindowHandle, WindowHandle.WindowCommand.Restore);

            WindowHandle.SetForeground(mainWindowHandle);
        }

        public static void UnhideSimilarProcess(this Process source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (TryGetArleadyStartedSimilarProcess(source, out Process anotherProcess))
                SetProcessWindowToForeground(anotherProcess);
        }
    }
}
