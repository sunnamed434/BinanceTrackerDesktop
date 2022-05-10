using BinanceTrackerDesktop.Core.Formatters.Currency;
using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Status.Base;
using BinanceTrackerDesktop.Core.User.Status.Result;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;

namespace BinanceTrackerDesktop.Core.User.Status.Beginner
{
    public sealed class UserBeginnerStatus : UserStatusBase
    {
        public UserBeginnerStatus(IUserDataSaveSystem userDataSaveSystem, UserWallet wallet) : base(userDataSaveSystem, wallet)
        {
        }



        public override async Task<IUserStatusResult> CalculateUserTotalBalanceAsync()
        {
            UserWalletResult totalBalanceWalletResult = await Wallet.GetTotalBalanceAsync();
            return new UserStatusResult(totalBalanceWalletResult.Value);
        }

        public override async Task<IUserStatusResult> CalculateUserBalanceLossesAsync()
        {
            return await Task.FromResult<IUserStatusResult>(new UserStatusResult(default(decimal)));
        }

        public override string Format(decimal? value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(value.Value).ToString();
        }
    }
}
