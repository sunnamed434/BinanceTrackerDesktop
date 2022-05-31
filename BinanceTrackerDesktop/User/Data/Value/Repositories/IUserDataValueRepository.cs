using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Value.Repositories;

public interface IUserDataValueRepository<TValue>
{
    IUserDataSaveSystem UserDataSaveSystem { get; }



    TValue GetValue();
}
