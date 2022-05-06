using BinanceTrackerDesktop.Core.Formatters.Currency;
using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Status.Base;
using BinanceTrackerDesktop.Core.User.Status.Result;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;

namespace BinanceTrackerDesktop.Core.User.Status.Standart
{
    public sealed class UserStandartStatus : UserStatusBase
    {
        public UserStandartStatus(IUserDataSaveSystem userDataSaveSystem, UserWallet wallet) : base(userDataSaveSystem, wallet)
        {
        }



        public override async Task<IUserStatusResult> CalculateUserTotalBalanceAsync()
        {
            UserWalletResult result = await Wallet.GetTotalBalanceAsync();

            return new UserStatusResult(result.Value);
        }

        public override async Task<IUserStatusResult> CalculateUserBalanceLossesAsync()
        {
            IUserStatusResult result = await CalculateUserTotalBalanceAsync();

            decimal userBestBalance = UserDataSaveSystem.Read().BestBalance;
            if (UserDataSaveSystem.Read().BestBalance > (decimal)result.Value)
                return new UserStatusResult(userBestBalance - (decimal)result.Value);
            else
                return new UserStatusResult((decimal)result.Value - userBestBalance);
        }

        public override string Format(decimal value)
        {
            return FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(value).ToString();
        }
    }
}
