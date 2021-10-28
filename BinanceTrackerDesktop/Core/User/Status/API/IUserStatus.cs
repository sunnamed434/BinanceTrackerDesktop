using BinanceTrackerDesktop.Core.Formatters.Models;
using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Status.Extension;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using System;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.User.Control
{
    public interface IUserStatus
    {
        UserData Data { get; } 

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
        public UserData Data { get; }

        public UserWallet Wallet { get; }



        protected UserStatusBase(UserData data, UserWallet wallet)
        {
            Data = data;
            Wallet = wallet;
        }



        public abstract Task<IUserStatusResult> CalculateUserBalanceLossesAsync();

        public abstract Task<IUserStatusResult> CalculateUserTotalBalanceAsync();

        public abstract string Format(decimal value);
    }

    public sealed class UserStandartStatus : UserStatusBase
    {
        public UserStandartStatus(UserData data, UserWallet wallet) : base(data, wallet)
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

            if (Data.BestBalance > (decimal)result.Value)
                return new UserStatusResult(Data.BestBalance - (decimal)result.Value);
            else
                return new UserStatusResult((decimal)result.Value - Data.BestBalance);
        }

        public override string Format(decimal value)
        {
            return FormatterUtility<CurrencyFormatter>.Format(value).ToString();
        }
    }

    public sealed class UserBeginnerStatus : UserStatusBase
    {
        public UserBeginnerStatus(UserData data, UserWallet wallet) : base(data, wallet)
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
            return FormatterUtility<CurrencyFormatter>.Format(value).ToString();
        }
    }

    public sealed class UserStatusDetector
    {
        private readonly UserData data;

        private readonly UserWallet wallet;



        public UserStatusDetector(UserData data, UserWallet wallet)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            this.data = data;
            this.wallet = wallet;
        }



        public IUserStatus GetStatus()
        {
            return this.data.UserStartedApplicationFirstTime() 
                ? new UserBeginnerStatus(this.data, this.wallet) 
                : new UserStandartStatus(this.data, this.wallet);
        }
    }
}
