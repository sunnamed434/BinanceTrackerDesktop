using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Language;

public sealed class BalancesHidenUserDataValueRepository : IUserDataValueRepository<bool>
{
    public BalancesHidenUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem)
    {
        UserDataSaveSystem = userDataSaveSystem ?? throw new ArgumentNullException(nameof(userDataSaveSystem));
    }


    public IUserDataSaveSystem UserDataSaveSystem { get; }



    public bool GetValue()
    {
        bool? isBalancesHiden = UserDataSaveSystem.Read().IsBalancesHiden;

        return isBalancesHiden.HasValue && isBalancesHiden.Value;
    }
}
