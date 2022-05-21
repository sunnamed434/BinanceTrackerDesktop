using BinanceTrackerDesktop.Core.User.Data.Save;

namespace BinanceTrackerDesktop.Core.User.Data.Value.Repositories.Authentication
{
    public sealed class HasAuthenticationDataUserDataValueRepository : IUserDataValueRepository<bool>
    {
        public HasAuthenticationDataUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem)
        {
            UserDataSaveSystem = userDataSaveSystem ?? throw new ArgumentNullException(nameof(userDataSaveSystem));
        }



        public IUserDataSaveSystem UserDataSaveSystem { get; }



        public bool GetValue()
        {
            return UserDataSaveSystem.Read().HasAuthenticationData;
        }
    }
}
