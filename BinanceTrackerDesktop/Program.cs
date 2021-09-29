using BinanceTrackerDesktop.Core.Client;
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

            new BinanceTrackerEntryPoint();

            await Task.CompletedTask;
        }
    }
}
