using BinanceTrackerDesktop.Formatters.Currency;
using BinanceTrackerDesktop.Formatters.Utilities;
using BinanceTrackerDesktop.User.Data.Save;
using BinanceTrackerDesktop.User.Status.Base;
using BinanceTrackerDesktop.User.Status.Result;
using BinanceTrackerDesktop.User.Wallet;
using BinanceTrackerDesktop.User.Wallet.Results;

namespace BinanceTrackerDesktop.User.Status.Beginner;

public sealed class UserBeginnerStatus : UserStatusBase
{
    public UserBeginnerStatus(IUserDataSaveSystem userDataSaveSystem, UserWallet wallet) : base(userDataSaveSystem, wallet)
    {
    }



    public override async Task<IUserStatusResult> CalculateUserTotalBalanceAsync()
    {
        IUserWalletResult totalBalanceWalletResult = await Wallet.GetTotalBalanceAsync();
        return new UserStatusResult(totalBalanceWalletResult.Value);
    }

    public override async Task<IUserStatusResult> CalculateUserBalanceLossesAsync()
    {
        return await Task.FromResult<IUserStatusResult>(new UserStatusResult(default(decimal)));
    }

    public override string Format(decimal? value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(value.Value).ToString();
    }
}
