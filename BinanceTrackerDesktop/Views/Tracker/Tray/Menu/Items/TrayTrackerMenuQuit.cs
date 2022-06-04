using BinanceTrackerDesktop.Entry;
using BinanceTrackerDesktop.Localizations.Data;
using BinanceTrackerDesktop.Views.Tracker.Menu.Base;

namespace BinanceTrackerDesktop.Views.Tracker.Tray.Menu.Items;

public sealed class TrayTrackerMenuQuit : TrackerMenuBase
{
    private readonly NotifyIcon notifyIcon;



    public TrayTrackerMenuQuit(NotifyIcon notifyIcon)
    {
        this.notifyIcon = notifyIcon ?? throw new ArgumentNullException(nameof(notifyIcon));
    }



    public async override void OnClick()
    {
        closeTray();

        await BinanceTrackerEntryPoint.AwaitablesProvider.Observer.CallListenersAsync();
    }

    protected override ToolStripMenuItem InitializeToolStripMenuItem()
    {
        return new ToolStripMenuItem(LocalizationData.Read().QuitBinanceTracker);
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
