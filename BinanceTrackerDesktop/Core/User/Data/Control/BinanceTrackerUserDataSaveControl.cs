using BinanceTrackerDesktop.Core.Components.API;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using System;
using System.Threading.Tasks;

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
            UserData userData = new BinaryUserDataSaveSystem().Read();

            if (userData.BestBalance < walletResult.Value)
                userData.BestBalance = walletResult.Value;

            userData.SaveUserData();

            await Task.CompletedTask;
        }
    }
}
