using BinanceTrackerDesktop.Core.Components.Safely;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;

namespace BinanceTrackerDesktop.Core.User.Data.Control
{
    public sealed class BinanceTrackerUserDataSaveControl
    {
        private readonly ISafelyComponentControl safelyComponentContro;

        private readonly UserWallet wallet;



        public BinanceTrackerUserDataSaveControl(ISafelyComponentControl safelyComponentContro, UserWallet wallet)
        {
            if (safelyComponentContro == null)
                throw new ArgumentNullException(nameof(safelyComponentContro));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            this.safelyComponentContro = safelyComponentContro;
            this.wallet = wallet;

            this.safelyComponentContro.RegisterListener(onCloseCallbackAsync);
        }



        private async Task onCloseCallbackAsync()
        {
            UserWalletResult walletResult = await this.wallet.GetTotalBalanceAsync();

            IUserDataBuilder userDataBuilder = new UserDataBuilder();
            userDataBuilder
                .ReadExistingUserDataAndCacheAll(new BinaryUserDataSaveSystem());
            UserData userData = userDataBuilder.Build();

            if (userData.BestBalance < walletResult.Value)
                userDataBuilder.AddBestBalance(walletResult.Value);

            userDataBuilder.Build()
                .WriteUserData(userDataBuilder.GetLastUsedSaveSystem());

            await Task.CompletedTask;
        }
    }
}
