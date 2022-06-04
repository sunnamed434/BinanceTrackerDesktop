using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Notifications;

public sealed class NotificationsDisabledUserDataValueRepository : UserDataValueRepository<bool>
{
    public NotificationsDisabledUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem) : base(userDataSaveSystem)
    {
    }



    public override bool GetValue()
    {
        return UserDataSaveSystem.Read().IsNotificationsDisabled;
    }
}
