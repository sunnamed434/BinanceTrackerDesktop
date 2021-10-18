using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.User.Status.Extension;
using BinanceTrackerDesktop.Core.Wallet;
using BinanceTrackerDesktop.Core.Wallet.API;
using System;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.User.Control
{
    public interface IBinanceUserStatus
    {
        UserData Data { get; } 

        BinanceUserWallet Wallet { get; }



        Task<IBinanceUserStatusResult> CalculateUserTotalBalanceAsync();

        Task<IBinanceUserStatusResult> CalculateUserBalanceLossesAsync();

        string Format(decimal value);
    }

    public interface IBinanceUserStatusResult
    {
        decimal Value { get; }
    }

    public class BinanceUserStatusResult : IBinanceUserStatusResult
    {
        public decimal Value { get; }



        public BinanceUserStatusResult(decimal value)
        {
            Value = value;
        }
    }

    public abstract class BinanceUserStatusBase : IBinanceUserStatus
    {
        public UserData Data { get; }

        public BinanceUserWallet Wallet { get; }



        protected BinanceUserStatusBase(UserData data, BinanceUserWallet wallet)
        {
            Data = data;
            Wallet = wallet;
        }



        public abstract Task<IBinanceUserStatusResult> CalculateUserBalanceLossesAsync();

        public abstract Task<IBinanceUserStatusResult> CalculateUserTotalBalanceAsync();

        public abstract string Format(decimal value);
    }

    public class BinanceUserStandartStatus : BinanceUserStatusBase
    {
        public BinanceUserStandartStatus(UserData data, BinanceUserWallet wallet) : base(data, wallet)
        {

        }



        public override async Task<IBinanceUserStatusResult> CalculateUserTotalBalanceAsync()
        {
            BinanceUserWalletResult result = await Wallet.GetTotalBalanceAsync();

            return new BinanceUserStatusResult(result.Value);
        }

        public override async Task<IBinanceUserStatusResult> CalculateUserBalanceLossesAsync()
        {
            IBinanceUserStatusResult result = await CalculateUserTotalBalanceAsync();

            if (Data.BestBalance > result.Value)
                return new BinanceUserStatusResult(Data.BestBalance - result.Value);
            else
                return new BinanceUserStatusResult(result.Value - Data.BestBalance);
        }

        public override string Format(decimal value)
        {
            return new CurrencyFormatter().Format(value);
        }
    }

    public class BinanceUserBeginnerStatus : BinanceUserStatusBase
    {
        public BinanceUserBeginnerStatus(UserData data, BinanceUserWallet wallet) : base(data, wallet)
        {

        }



        public override async Task<IBinanceUserStatusResult> CalculateUserTotalBalanceAsync()
        {
            BinanceUserWalletResult walletResult = await Wallet.GetTotalBalanceAsync();

            return new BinanceUserStatusResult(walletResult.Value);
        }

        public override async Task<IBinanceUserStatusResult> CalculateUserBalanceLossesAsync()
        {
            return await Task.FromResult<IBinanceUserStatusResult>(new BinanceUserStatusResult(default(decimal)));
        }

        public override string Format(decimal value)
        {
            return new CurrencyFormatter().Format(value);
        }
    }

    public class BinanceUserStatusDetector
    {
        private readonly UserData data;

        private readonly BinanceUserWallet wallet;



        public BinanceUserStatusDetector(UserData data, BinanceUserWallet wallet)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            this.data = data;
            this.wallet = wallet;
        }



        public IBinanceUserStatus GetStatus()
        {
            return this.data.UserStartedApplicationFirstTime() 
                ? new BinanceUserBeginnerStatus(data, wallet) 
                : new BinanceUserStandartStatus(data, wallet);
        }
    }
}
