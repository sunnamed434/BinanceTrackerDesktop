using BinanceTrackerDesktop.Core.Forms.Authorization;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.Window.Extension;
using BinanceTrackerDesktop.Tracker.Forms;
using System.Diagnostics;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Client
{
    public class BinanceTrackerEntryPoint
    {
        public BinanceTrackerEntryPoint()
        {
            if (Process.GetCurrentProcess().TryGetArleadyStartedSimilarProcess(out Process anotherProcess))
            {
                anotherProcess.SetProcessWindowToForeground();
                return;
            }

            if (new BinaryUserDataSaveReadSystem().Read() == null)
            {
                Application.Run(new BinanceTrackerAuthorizationForm());
            }
            else
            {
                Application.Run(new BinanceTrackerForm());
            }
        }
    }
}
