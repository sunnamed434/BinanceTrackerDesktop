using System;
using System.Diagnostics;

namespace BinanceTrackerDesktop.Core.Window.Extension
{
    public static class ProcessWindowExtension
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
