using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.Forms.Tracker.Settings;
using BinanceTrackerDesktop.Localizations.Data;
using BinanceTrackerDesktop.User.Client;
using BinanceTrackerDesktop.Views.Tracker.Menu.Base;

namespace BinanceTrackerDesktop.Views.Tracker.Menu.Items;

public sealed class TrackerMenuSettings : TrackerMenuBase
{
    public override void OnClick()
    {
        TrackerSettingsFormView settingsView = new TrackerSettingsFormView(UserClient.Wallet);
        new SettingsController(settingsView, UserClient.Wallet);
        new TrackerSettingsFormView(UserClient.Wallet).ShowDialog();
    }



    protected override ToolStripMenuItem InitializeToolStripMenuItem()
    {
        return new ToolStripMenuItem(LocalizationData.Read().Settings);
    }
}
