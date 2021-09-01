using BinanceTrackerDesktop.Core.Extension;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Wallet;
using BinanceTrackerDesktop.Forms.API;
using ConsoleBinanceTracker.Core.Wallet.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.Startup.Control
{
    public class BinanceTrackerApplicationControl
    {
        private readonly IFormControl formControl;

        private readonly BinanceUserWallet wallet;



        public BinanceTrackerApplicationControl(IFormControl formControl, BinanceUserWallet wallet)
        {
            if (formControl == null)
                throw new ArgumentNullException(nameof(formControl));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            this.formControl = formControl;
            this.wallet = wallet;

            formControl.FormClosing += onFormClosing;
        }



        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            formControl.FormClosing -= onFormClosing;

            formControl.Hide();

            e.Cancel = true;

            BinanceUserWalletResult walletResult = await wallet.GetTotalBalanceAsync();
            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;

            userData
                .With(s => s.Balance = walletResult.Value)
                .With(s => s.BestBalance = walletResult.Value, userData.BestBalance < walletResult.Value);

            await new BinanceUserDataWriter().WriteDataAsync(userData);

            Environment.Exit(0);
        }
    }
}
