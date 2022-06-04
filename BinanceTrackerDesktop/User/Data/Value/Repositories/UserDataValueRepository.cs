using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories;

public abstract class UserDataValueRepository<TValue> : IUserDataValueRepository<TValue>
{
    protected UserDataValueRepository(IUserDataSaveSystem userDataSaveSystem)
    {
        UserDataSaveSystem = userDataSaveSystem ?? throw new ArgumentNullException(nameof(userDataSaveSystem));
    }



    public IUserDataSaveSystem UserDataSaveSystem { get; }




    public abstract TValue GetValue();
}
