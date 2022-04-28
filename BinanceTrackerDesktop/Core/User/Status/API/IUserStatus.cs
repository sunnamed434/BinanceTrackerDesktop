using BinanceTrackerDesktop.Core.Formatters.Currency;
using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Status.Extension;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using System;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.User.Control
{
    public interface IUserStatus
    {
        IUserDataSaveSystem UserDataSaveSystem { get; } 

        UserWallet Wallet { get; }



        Task<IUserStatusResult> CalculateUserTotalBalanceAsync();

        Task<IUserStatusResult> CalculateUserBalanceLossesAsync();

        string Format(decimal value);
    }

    public interface IUserStatusResult
    {
        object Value { get; }
    }

    public sealed class UserStatusResult : IUserStatusResult
    {
        public object Value { get; }



        public UserStatusResult(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }
    }

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

    public sealed class UserBeginnerStatus : UserStatusBase
    {
        public UserBeginnerStatus(IUserDataSaveSystem userDataSaveSystem, UserWallet wallet) : base(userDataSaveSystem, wallet)
        {

        }



        public override async Task<IUserStatusResult> CalculateUserTotalBalanceAsync()
        {
            UserWalletResult walletResult = await Wallet.GetTotalBalanceAsync();

            return new UserStatusResult(walletResult.Value);
        }

        public override async Task<IUserStatusResult> CalculateUserBalanceLossesAsync()
        {
            return await Task.FromResult<IUserStatusResult>(new UserStatusResult(default(decimal)));
        }

        public override string Format(decimal value)
        {
            return FormatterUtility<BasedOnUserDataCurrencyFormatter>.Format(value).ToString();
        }
    }

    public sealed class UserStatusDetector
    {
        private readonly IUserDataSaveSystem userDataSaveSystem;

        private readonly UserWallet wallet;



        public UserStatusDetector(IUserDataSaveSystem userDataSaveSystem, UserWallet wallet)
        {
            if (userDataSaveSystem == null)
                throw new ArgumentNullException(nameof(userDataSaveSystem));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            this.userDataSaveSystem = userDataSaveSystem;
            this.wallet = wallet;
        }



        public IUserStatus GetStatus()
        {
            return this.userDataSaveSystem.Read().UserStartedApplicationFirstTime() 
                ? new UserBeginnerStatus(this.userDataSaveSystem, this.wallet) 
                : new UserStandartStatus(this.userDataSaveSystem, this.wallet);
        }
    }
}
