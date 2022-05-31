using BinanceTrackerDesktop.Entry;

namespace BinanceTrackerDesktop;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        new BinanceTrackerEntryPoint();
    }
}