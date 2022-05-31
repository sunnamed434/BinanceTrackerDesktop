using BinanceTrackerDesktop.Formatters.Currency;
using BinanceTrackerDesktop.Formatters.Utilities;
using BinanceTrackerDesktop.User.Data.Save;
using BinanceTrackerDesktop.User.Status.Base;
using BinanceTrackerDesktop.User.Status.Result;
using BinanceTrackerDesktop.User.Wallet;
using BinanceTrackerDesktop.User.Wallet.Results;

namespace BinanceTrackerDesktop.User.Status.Standart;

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
        {
            return new UserStatusResult(userBestBalance - (decimal)statusResult.Value);
        }
        else
        {
            return new UserStatusResult((decimal)statusResult.Value - userBestBalance);
        }
    }

    public override string Format(decimal? value)
    {
        return FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(value.Value).ToString();
    }
}
