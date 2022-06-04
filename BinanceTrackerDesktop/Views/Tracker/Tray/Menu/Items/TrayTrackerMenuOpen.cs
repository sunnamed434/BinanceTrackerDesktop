using BinanceTrackerDesktop.Localizations.Data;
using BinanceTrackerDesktop.Views.Tracker.Menu.Base;
using BinanceTrackerDesktop.Window.Helper;

namespace BinanceTrackerDesktop.Views.Tracker.Tray.Menu.Items;

public sealed class TrayTrackerMenuOpen : TrackerMenuBase
{
    private readonly IProcessWindowHelper processWindowHelper;



    public TrayTrackerMenuOpen()
    {
        processWindowHelper = new ProcessWindowHelper();
    }



    public override void OnClick()
    {
        processWindowHelper.SetWindowToForeground();
    }

    protected override ToolStripMenuItem InitializeToolStripMenuItem()
    {
        return new ToolStripMenuItem(LocalizationData.Read().OpenBinanceTracker);
    }
}
