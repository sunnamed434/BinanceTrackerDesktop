using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Notifications;

public sealed class NotificationsDisabledUserDataValueRepository : IUserDataValueRepository<bool>
{
    public NotificationsDisabledUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem)
    {
        UserDataSaveSystem = userDataSaveSystem ?? throw new ArgumentNullException(nameof(userDataSaveSystem));
    }



    public IUserDataSaveSystem UserDataSaveSystem { get; }



    public bool GetValue()
    {
        return UserDataSaveSystem.Read().IsNotificationsDisabled;
    }
}
