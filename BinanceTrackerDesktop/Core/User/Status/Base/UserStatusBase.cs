using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Status.Result;
using BinanceTrackerDesktop.Core.User.Wallet;

namespace BinanceTrackerDesktop.Core.User.Status.Base
{
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

        public abstract string Format(decimal value);
    }
}
