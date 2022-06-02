using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;
using BinanceTrackerDesktop.Window.Helper;

namespace BinanceTrackerDesktop.Views.Tracker.Tray.Menu.Items;

public sealed class TrayTrackerMenuOpen : TrackerMenuBase
{
    private readonly IProcessWindowHelper processWindowHelper;




    public TrayTrackerMenuOpen()
    {
        processWindowHelper = new ProcessWindowHelper();
    }


    public override string Label => "";

    public override Image Image => null;

    public override ToolStripItem[] Items => null;



    public override void OnClick()
    {
        processWindowHelper.SetWindowToForeground();
    }
}
