using BinanceTrackerDesktop.Core.Forms.Authorization;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.Window.Extension;
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

            UserData userData = await new UserDataReader().ReadDataAsync();
            if (userData == null)
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
