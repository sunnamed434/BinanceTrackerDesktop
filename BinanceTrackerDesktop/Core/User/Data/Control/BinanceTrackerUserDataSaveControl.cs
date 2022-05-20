using BinanceTrackerDesktop.Core.Awaitable.Awaitables;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Results;

namespace BinanceTrackerDesktop.Core.User.Data.Control
{
    public sealed class BinanceTrackerUserDataSaveControl : IAwaitableSingletonObject, IAwaitableAsyncExecute
    {
        private readonly UserWallet wallet;

        private static BinanceTrackerUserDataSaveControl instance;



        public BinanceTrackerUserDataSaveControl(UserWallet wallet)
        {
            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

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
                userDataBuilder.AddBestBalance(totalBalanceWalletResult.Value);

            if (userData.BestBalance.HasValue == false)
                userDataBuilder.AddBestBalance(totalBalanceWalletResult.Value);

            userDataBuilder
                .SetAsUserStartedApplicationNotFirstTime()
                .Build()
                .WriteUserData(saveSystem);
        }
    }
}
