using BinanceTrackerDesktop.Core.Formatters.Currency;
using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Status.Base;
using BinanceTrackerDesktop.Core.User.Status.Result;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Results;

namespace BinanceTrackerDesktop.Core.User.Status.Standart
{
    public sealed class UserStandartStatus : UserStatusBase
    {
        public UserStandartStatus(IUserDataSaveSystem userDataSaveSystem, UserWallet wallet) : base(userDataSaveSystem, wallet)
        {
        }



        public override async Task<IUserStatusResult> CalculateUserTotalBalanceAsync()
        {
            IUserWalletResult totalBalanceWalletResult = await Wallet.GetTotalBalanceAsync();
            return new UserStatusResult(totalBalanceWalletResult.Value);
        }

        public override async Task<IUserStatusResult> CalculateUserBalanceLossesAsync()
        {
            IUserStatusResult statusResult = await CalculateUserTotalBalanceAsync();

            decimal? userBestBalance = UserDataSaveSystem.Read().BestBalance;
            if (UserDataSaveSystem.Read().BestBalance > (decimal)statusResult.Value)
                return new UserStatusResult(userBestBalance - (decimal)statusResult.Value);
            else
                return new UserStatusResult((decimal)statusResult.Value - userBestBalance);
        }

        public override string Format(decimal? value)
        {
            return FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(value.Value).ToString();
        }
    }
}
