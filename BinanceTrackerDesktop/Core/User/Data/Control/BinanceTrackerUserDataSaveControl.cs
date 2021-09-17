using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.Wallet;
using BinanceTrackerDesktop.Core.Wallet.API;
using System;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.User.Data.Control
{
    public class BinanceTrackerUserDataSaveControl
    {
        private readonly ISafelyComponentControl safelyComponentContro;

        private readonly BinanceUserWallet wallet;



        public BinanceTrackerUserDataSaveControl(ISafelyComponentControl safelyComponentContro, BinanceUserWallet wallet)
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
            BinanceUserWalletResult walletResult = await this.wallet.GetTotalBalanceAsync();
            UserData userData = await new UserDataReader().ReadDataAsync();

            if (userData.BestBalance < walletResult.Value)
                userData.BestBalance = walletResult.Value;

            await userData.SaveUserDataAsync();
        }
    }
}
