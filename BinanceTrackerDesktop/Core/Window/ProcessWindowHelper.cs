using BinanceTrackerDesktop.Core.Window.Extension;
using System.Diagnostics;

namespace BinanceTrackerDesktop.Core.Window
{
    public interface IProcessWindowHelper
    {
        void SetWindowToForeground();
    }

    public class ProcessWindowHelper : IProcessWindowHelper
    {
        public void SetWindowToForeground()
        {
            Process.GetCurrentProcess()?.SetProcessWindowToForeground();
        }
    }
}
