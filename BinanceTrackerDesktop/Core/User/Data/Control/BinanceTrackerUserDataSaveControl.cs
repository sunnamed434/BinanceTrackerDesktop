using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;

namespace BinanceTrackerDesktop.Core.User.Data.Control
{
    public sealed class BinanceTrackerUserDataSaveControl : IAwaitableSingletonObject, IAwaitableComponent
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



        async Task IAwaitableComponent.OnExecute()
        {
            BinaryUserDataSaveSystem saveSystem = new BinaryUserDataSaveSystem();
            IUserDataBuilder userDataBuilder = new UserDataBuilder(saveSystem.Read());

            UserData userData = userDataBuilder.Build();

            UserWalletResult walletTotalBalanceResult = await this.wallet.GetTotalBalanceAsync();
            if (userData.BestBalance.HasValue && userData.BestBalance.Value < walletTotalBalanceResult.Value)
                userDataBuilder.AddBestBalance(walletTotalBalanceResult.Value);

            if (userData.BestBalance.HasValue == false)
                userDataBuilder.AddBestBalance(walletTotalBalanceResult.Value);

            userDataBuilder.SetAsUserStartedApplicationNotFirstTime()
                .Build()
                .WriteUserData(saveSystem);

            await Task.CompletedTask;
        }
    }
}
