using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Authentication;

public sealed class HasAuthenticationDataUserDataValueRepository : UserDataValueRepository<bool>
{
    public HasAuthenticationDataUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem) : base(userDataSaveSystem)
    {
    }



    public override bool GetValue()
    {
        return UserDataSaveSystem.Read().HasAuthenticationData;
    }
}
