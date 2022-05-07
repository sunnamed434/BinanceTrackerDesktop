using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Status.Result;
using BinanceTrackerDesktop.Core.User.Wallet;

namespace BinanceTrackerDesktop.Core.User.Control
{
    public interface IUserStatus
    {
        IUserDataSaveSystem UserDataSaveSystem { get; } 

        UserWallet Wallet { get; }



        Task<IUserStatusResult> CalculateUserTotalBalanceAsync();

        Task<IUserStatusResult> CalculateUserBalanceLossesAsync();

        string Format(decimal? value);
    }
}
