using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.Forms.Tracker.Settings;
using BinanceTrackerDesktop.User.Client;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

namespace BinanceTrackerDesktop.Views.Tracker.Menu.Items.Settings;

public sealed class TrackerMenuSettings : TrackerMenuBase
{
    public override string Label => "Settings";

    public override Image Image => null;

    public override ToolStripItem[] DropDownItems => null;



    public override void OnClick()
    {
        TrackerSettingsFormView settingsView = new TrackerSettingsFormView(UserClient.Wallet);
        new SettingsController(settingsView, UserClient.Wallet);
        new TrackerSettingsFormView(UserClient.Wallet).ShowDialog();
    }
}
