using BinanceTrackerDesktop.User.Data.Save;
using BinanceTrackerDesktop.User.Status.Result;
using BinanceTrackerDesktop.User.Wallet;

namespace BinanceTrackerDesktop.User.Status.API;

public interface IUserStatus
{
    IUserDataSaveSystem UserDataSaveSystem { get; }

    UserWallet Wallet { get; }



    Task<IUserStatusResult> CalculateUserTotalBalanceAsync();

    Task<IUserStatusResult> CalculateUserBalanceLossesAsync();

    string Format(decimal? value);
}
