using BinanceTrackerDesktop.Entry;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

namespace BinanceTrackerDesktop.Views.Tracker.Tray.Menu.Items;

public sealed class TrayTrackerMenuQuit : TrackerMenuBase
{
    private readonly NotifyIcon notifyIcon;



    public TrayTrackerMenuQuit(NotifyIcon notifyIcon)
    {
        this.notifyIcon = notifyIcon ?? throw new ArgumentNullException(nameof(notifyIcon));
    }



    public override string Label => "Quit Binance Tracker";

    

        
    public async override void OnClick()
    {
        closeTray();

        await BinanceTrackerEntryPoint.AwaitablesProvider.Observer.CallListenersAsync();
    }



    private void closeTray()
    {
        using (this.notifyIcon)
        {
            this.notifyIcon.Visible = false;
            this.notifyIcon.Icon.Dispose();
        }
    }
}
