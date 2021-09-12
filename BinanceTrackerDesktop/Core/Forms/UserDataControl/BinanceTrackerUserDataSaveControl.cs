using BinanceTrackerDesktop.Core.Forms.API;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Wallet;
using BinanceTrackerDesktop.Core.Wallet.API;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.UserDataControl
{
    public class BinanceTrackerUserDataSaveControl
    {
        private readonly IFormSafelyComponentControl safelyComponentContro;

        private readonly BinanceUserWallet wallet;



        public BinanceTrackerUserDataSaveControl(IFormSafelyComponentControl safelyComponentContro, BinanceUserWallet wallet)
        {
            if (safelyComponentContro == null)
                throw new ArgumentNullException(nameof(safelyComponentContro));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            this.safelyComponentContro = safelyComponentContro;
            this.wallet = wallet;

            this.safelyComponentContro.RegisterListener(onCloseCallbackAsync);
        }



        private async Task saveUserDataAsync()
        {
            BinanceUserWalletResult walletResult = await this.wallet.GetTotalBalanceAsync();
            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;

            if (userData.BestBalance < walletResult.Value)
                userData.BestBalance = walletResult.Value;

            await new BinanceUserDataWriter().WriteDataAsync(userData);

            await Task.CompletedTask;
        }



        private async Task onCloseCallbackAsync()
        {
            await saveUserDataAsync();

            await Task.CompletedTask;
        }
    }
}
