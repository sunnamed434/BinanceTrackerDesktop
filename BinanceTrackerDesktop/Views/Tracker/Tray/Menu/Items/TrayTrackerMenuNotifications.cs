using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Forms.Tray;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Builder;
using BinanceTrackerDesktop.User.Data.Extension;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

namespace BinanceTrackerDesktop.Views.Tracker.Tray.Menu.Items;

public sealed class TrayTrackerMenuNotifications : TrackerMenuBase
{
    public TrayTrackerMenuNotifications()
    {
        bool isNotificationsEnabled = new BinaryUserDataSaveSystem().Read().IsNotificationsEnabled;
        Label = getNotificationsText(isNotificationsEnabled);
    }



    public override string Label { get; }



    public override void OnClick()
    {
        BinaryUserDataSaveSystem saveSystem = new BinaryUserDataSaveSystem();
        IUserDataBuilder userDataBuilder = new UserDataBuilder(saveSystem.Read());

        UserData userData = userDataBuilder.Build();

        userDataBuilder.AddNotificationsStateBasedOnData(!userData.IsNotificationsEnabled);

        userData = userDataBuilder.Build()
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



    private string getNotificationsText(bool isNotificationsEnabled)
    {
        return isNotificationsEnabled 
            ? TrayItemsTextContainer.DisableNotifications 
            : TrayItemsTextContainer.EnableNotifications;
    }
}
