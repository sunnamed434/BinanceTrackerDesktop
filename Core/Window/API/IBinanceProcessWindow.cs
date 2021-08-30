using BinanceTrackerDesktop.Core.Window.Extension;
using System.Diagnostics;

namespace BinanceTrackerDesktop.Core.Window.API
{
    public interface IBinanceProcessWindow
    {
        void SetWindowToForeground();
    }

    public class BinanceProcessWindow : IBinanceProcessWindow
    {
        public void SetWindowToForeground()
        {
            Process.GetCurrentProcess()?.SetProcessWindowToForeground();
        }
    }
}
