using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Forms.Tray;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Builder;
using BinanceTrackerDesktop.User.Data.Extension;
using BinanceTrackerDesktop.User.Data.Save;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

namespace BinanceTrackerDesktop.Views.Tracker.Tray.Menu.Items;

public sealed class TrayTrackerMenuNotifications : TrackerMenuBase
{
    public override void OnClick()
    {
        IUserDataSaveSystem saveSystem = new BinaryUserDataSaveSystem();
        UserData userData = saveSystem.Read();
        userData = new UserDataBuilder(userData)
            .AddNotificationsStateBasedOnData(!userData.IsNotificationsEnabled)
            .Build()
            .WriteUserDataThenRead(saveSystem);

        new PopupBuilder()
            .WithTitle(ApplicationEnviroment.GlobalName)
            .WithMessage(userData.IsNotificationsEnabled 
                ? TrayItemsTextContainer.NotificationsEnabled 
                : TrayItemsTextContainer.NotificationsDisabled)
            .WillCloseIn(90)
            .ShowMessageBoxIfShouldOnBuild()
            .Build();

        ToolStripMenuItem.Text = getNotificationsText(userData.IsNotificationsEnabled);
    }

    protected override ToolStripMenuItem InitializeToolStripMenuItem()
    {
        bool isNotificationsEnabled = new BinaryUserDataSaveSystem().Read().IsNotificationsEnabled;
        return new ToolStripMenuItem(getNotificationsText(isNotificationsEnabled));
    }



    private string getNotificationsText(bool isNotificationsEnabled)
    {
        return isNotificationsEnabled 
            ? TrayItemsTextContainer.DisableNotifications 
            : TrayItemsTextContainer.EnableNotifications;
    }
}
