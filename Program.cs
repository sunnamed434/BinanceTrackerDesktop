using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.Authorization;
using BinanceTrackerDesktop.Tracker.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop
{
    public static class Program
    {
        [STAThread]
        private async static Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (await new BinanceUserDataReader().ReadDataAsync() != null)
                Application.Run(new BinanceTrackerForm());
            else
                Application.Run(new BinanceTrackerAuthorizationForm());
        }
    }
}
