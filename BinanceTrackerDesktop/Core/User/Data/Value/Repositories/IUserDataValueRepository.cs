using BinanceTrackerDesktop.Core.User.Data.Save;

namespace BinanceTrackerDesktop.Core.User.Data.Value.Repositories
{
    public interface IUserDataValueRepository<TValue>
    {
        IUserDataSaveSystem UserDataSaveSystem { get; }



        TValue GetValue();
    }
}
