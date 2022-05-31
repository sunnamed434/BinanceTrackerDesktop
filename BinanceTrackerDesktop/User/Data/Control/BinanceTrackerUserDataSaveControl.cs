using BinanceTrackerDesktop.Awaitable.Awaitables;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Builder;
using BinanceTrackerDesktop.User.Data.Extension;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Wallet;
using BinanceTrackerDesktop.User.Wallet.Results;

namespace BinanceTrackerDesktop.Core.User.Data.Control
{
    public sealed class BinanceTrackerUserDataSaveControl : IAwaitableSingletonObject, IAwaitableAsyncExecute
    {
        private readonly UserWallet wallet;

        private static BinanceTrackerUserDataSaveControl instance;



        public BinanceTrackerUserDataSaveControl(UserWallet wallet)
        {
            if (wallet == null)
            {
                throw new ArgumentNullException(nameof(wallet));
            }

            instance = this;
            this.wallet = wallet;
        }



        object IAwaitableSingletonObject.Instance => instance;



        async Task IAwaitableAsyncExecute.OnExecuteAsync()
        {
            BinaryUserDataSaveSystem saveSystem = new BinaryUserDataSaveSystem();
            IUserDataBuilder userDataBuilder = new UserDataBuilder(saveSystem.Read());

            UserData userData = userDataBuilder
                .Build();

            IUserWalletResult totalBalanceWalletResult = await this.wallet.GetTotalBalanceAsync();
            if (userData.BestBalance.HasValue && userData.BestBalance.Value < totalBalanceWalletResult.Value)
            {
                userDataBuilder.AddBestBalance(totalBalanceWalletResult.Value);
            }

            if (userData.BestBalance.HasValue == false)
            {
                userDataBuilder.AddBestBalance(totalBalanceWalletResult.Value);
            }

            userDataBuilder
                .SetAsUserStartedApplicationNotFirstTime()
                .Build()
                .WriteUserData(saveSystem);
        }
    }
}
