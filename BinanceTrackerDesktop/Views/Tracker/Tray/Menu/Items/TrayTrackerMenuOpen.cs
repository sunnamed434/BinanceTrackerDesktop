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



    public override string Label => "Open Binance Tracker";



    public override void OnClick()
    {
        processWindowHelper.SetWindowToForeground();
    }
}
