using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Localizations.Data;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Builder;
using BinanceTrackerDesktop.User.Data.Extension;
using BinanceTrackerDesktop.User.Data.Save;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Data.Value;
using BinanceTrackerDesktop.Views.Tracker.Menu.Base;

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

        LocalizationData localizationData = LocalizationData.Read();
        new PopupBuilder()
            .WithTitle(ApplicationEnviroment.GlobalName)
            .WithMessage(userData.IsNotificationsEnabled 
                ? localizationData.NotificationsEnabled 
                : localizationData.NotificationsDisabled)
            .WillCloseIn(90)
            .ShowMessageBoxIfShouldOnBuild()
            .Build();

        ToolStripMenuItem.Text = getNotificationsText(userData.IsNotificationsEnabled);
    }

    protected override ToolStripMenuItem InitializeToolStripMenuItem()
    {
        return new ToolStripMenuItem(getNotificationsText(UserDataValues.NotificationsEnabled.GetValue()));
    }



    private string getNotificationsText(bool isNotificationsEnabled)
    {
        LocalizationData localizationData = LocalizationData.Read();
        return isNotificationsEnabled 
            ? localizationData.DisableNotifications 
            : localizationData.EnableNotifications;
    }
}
