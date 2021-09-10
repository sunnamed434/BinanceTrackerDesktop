using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Wallet;
using BinanceTrackerDesktop.Core.Wallet.API;
using System;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Forms.Tracker.Startup.API
{
    public interface IBinanceUserStatus
    {
        BinanceUserData Data { get; } 

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
        public BinanceUserData Data { get; }

        public BinanceUserWallet Wallet { get; }

        


        protected BinanceUserStatusBase(BinanceUserData data, BinanceUserWallet wallet)
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
        public BinanceUserStandartStatus(BinanceUserData data, BinanceUserWallet wallet) : base(data, wallet)
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
            return new BinanceCurrencyValueFormatter().Format(value);
        }
    }

    public class BinanceUserBeginnerStatus : BinanceUserStatusBase
    {
        public BinanceUserBeginnerStatus(BinanceUserData data, BinanceUserWallet wallet) : base(data, wallet)
        {

        }



        public override async Task<IBinanceUserStatusResult> CalculateUserTotalBalanceAsync()
        {
            BinanceUserWalletResult walletResult = await Wallet.GetTotalBalanceAsync();

            return new BinanceUserStatusResult(walletResult.Value);
        }

        public override async Task<IBinanceUserStatusResult> CalculateUserBalanceLossesAsync()
        {
            return await Task.FromResult<IBinanceUserStatusResult>(new BinanceUserStatusResult(decimal.Zero));
        }

        public override string Format(decimal value)
        {
            return new BinanceCurrencyValueFormatter().Format(value);
        }
    }

    public class BinanceUserStatusDetector
    {
        private readonly BinanceUserData data;

        private readonly BinanceUserWallet wallet;



        public BinanceUserStatusDetector(BinanceUserData data, BinanceUserWallet wallet)
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
            if (!data.UserStartedApplicationFirstTime())
                return new BinanceUserStandartStatus(data, wallet);
            else
                return new BinanceUserBeginnerStatus(data, wallet);
        }
    }

    public static class BinanceUserDataExtension
    {
        public static bool UserStartedApplicationFirstTime(this BinanceUserData source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return source.Balance == decimal.Zero;
        }
    }
}
