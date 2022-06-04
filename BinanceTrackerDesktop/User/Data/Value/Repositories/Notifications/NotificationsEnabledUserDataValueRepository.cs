using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Notifications
{
    public sealed class NotificationsEnabledUserDataValueRepository : UserDataValueRepository<bool>
    {
        public NotificationsEnabledUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem) : base(userDataSaveSystem)
        {
        }



        public override bool GetValue()
        {
            return UserDataSaveSystem.Read().IsNotificationsEnabled;
        }
    }
}
