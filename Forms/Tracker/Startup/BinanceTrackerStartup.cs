using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.Authorization;
using BinanceTrackerDesktop.Forms.Tracker.Startup.Extension;
using BinanceTrackerDesktop.Tracker.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.Startup
{
    public class BinanceTrackerStartup
    {
        public async Task InitializeAsync()
        {
            Process process = Process.GetCurrentProcess();

            if (process.TryGetArleadyStartedSimilarProcess(out Process anotherProcess))
            {
                process.UnhideSimilarProcess();
                return;
            }

            if (await new BinanceUserDataReader().ReadDataAsync() != null)
            {
                Application.Run(new BinanceTrackerForm());
            }
            else
            {
                Application.Run(new BinanceTrackerAuthorizationForm());
            }
        }
    }
}
