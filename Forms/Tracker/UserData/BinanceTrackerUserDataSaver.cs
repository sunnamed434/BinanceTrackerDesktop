using BinanceTrackerDesktop.Core.Extension;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Wallet;
using BinanceTrackerDesktop.Forms.Tracker.API;
using ConsoleBinanceTracker.Core.Wallet.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.UserData
{
    public class BinanceTrackerUserDataSaver
    {
        private readonly IFormControl formControl;

        private readonly BinanceUserWallet wallet;



        public BinanceTrackerUserDataSaver(IFormControl formControl, BinanceUserWallet wallet)
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

            e.Cancel = true;

            BinanceUserWalletResult walletResult = await wallet.GetTotalBalanceAsync();
            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;

            userData.With(s => s.Balance = walletResult.Value);
            userData.With(s => s.BestBalance = walletResult.Value, walletResult.Value > userData.BestBalance);

            await new BinanceUserDataWriter().WriteDataAsync(userData);

            formControl.Close();
        }
    }
}
