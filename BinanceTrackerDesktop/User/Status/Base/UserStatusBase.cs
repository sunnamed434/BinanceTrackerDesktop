using BinanceTrackerDesktop.User.Data.Save;
using BinanceTrackerDesktop.User.Status.API;
using BinanceTrackerDesktop.User.Status.Result;
using BinanceTrackerDesktop.User.Wallet;

namespace BinanceTrackerDesktop.User.Status.Base;

public abstract class UserStatusBase : IUserStatus
{
    public IUserDataSaveSystem UserDataSaveSystem { get; }

    public UserWallet Wallet { get; }



    protected UserStatusBase(IUserDataSaveSystem userDataSaveSystem, UserWallet wallet)
    {
        UserDataSaveSystem = userDataSaveSystem;
        Wallet = wallet;
    }



    public abstract Task<IUserStatusResult> CalculateUserBalanceLossesAsync();

    public abstract Task<IUserStatusResult> CalculateUserTotalBalanceAsync();

    public abstract string Format(decimal? value);
}
