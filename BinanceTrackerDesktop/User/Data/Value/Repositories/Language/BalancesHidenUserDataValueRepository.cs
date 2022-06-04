using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories.Language;

public sealed class BalancesHidenUserDataValueRepository : UserDataValueRepository<bool>
{
    public BalancesHidenUserDataValueRepository(IUserDataSaveSystem userDataSaveSystem) : base(userDataSaveSystem)
    {
    }



    public override bool GetValue()
    {
        bool? isBalancesHiden = UserDataSaveSystem.Read().IsBalancesHiden;

        return isBalancesHiden.HasValue && isBalancesHiden.Value;
    }
}
