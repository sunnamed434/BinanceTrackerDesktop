using BinanceTrackerDesktop.Core.Extension;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Wallet;
using BinanceTrackerDesktop.Core.Wallet.API;
using BinanceTrackerDesktop.Forms.API;
using System;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Forms.Tracker.Startup.Control
{
    public class BinanceTrackerApplicationControl
    {
        private readonly IFormControl formControl;

        private readonly IFormSafelyComponentControl safelyComponentContro;

        private readonly BinanceUserWallet wallet;



        public BinanceTrackerApplicationControl(IFormControl formControl, IFormSafelyComponentControl safelyComponentContro, BinanceUserWallet wallet)
        {
            if (formControl == null)
                throw new ArgumentNullException(nameof(formControl));

            if (safelyComponentContro == null)
                throw new ArgumentNullException(nameof(safelyComponentContro));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            this.formControl = formControl;
            this.safelyComponentContro = safelyComponentContro;
            this.wallet = wallet;

            this.safelyComponentContro.RegisterListener(onCloseCallbackAsync);
        }



        private async Task onCloseCallbackAsync()
        {
            this.formControl.Hide();

            BinanceUserWalletResult walletResult = await wallet.GetTotalBalanceAsync();
            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;

            userData
                .With(s => s.Balance = walletResult.Value)
                .With(s => s.BestBalance = walletResult.Value, userData.BestBalance < walletResult.Value);

            await new BinanceUserDataWriter().WriteDataAsync(userData);
        }
    }
}
