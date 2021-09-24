using BinanceTrackerDesktop.Forms.Tracker.Startup;
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

            new BinanceTrackerStartup();

            await Task.CompletedTask;
        }
    }
}
