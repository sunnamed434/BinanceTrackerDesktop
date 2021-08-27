using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.Startup;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.SystemTray;
using BinanceTrackerDesktop.Forms.Tracker.API;
using BinanceTrackerDesktop.Forms.Tracker.MoveListener;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using BinanceTrackerDesktop.Forms.Tracker.Notifications.API;
using ConsoleBinanceTracker.Core.Wallet.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public partial class BinanceTrackerForm : Form, IFormControl
    {
        private BinanceStartup startup;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            intitializeForm();

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;

            new BinanceTrackerMoveListener(this, new BinanceTrackerNotificationsControl(new StableNotificationsControl(new BinanceTrackerSystemTrayForm(this).Notify)));
        }



        private void intitializeForm()
        {
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.MaximizeBox = false;
        }



        private async void refreshUserTotalBalanceAsync()
        {
            BinanceUserWalletResult result = await startup.Wallet.GetTotalBalanceAsync();

            UserTotalBalanceText.Text = new BinanceCurrencyValueFormatter().Format(result.Value);
        }

        private async void calculateUserTotalBalanceLosesAndRefreshTotalBalanceLosesTextAsync()
        {
            BinanceUserWalletResult result = await startup.Wallet.GetTotalBalanceAsync();
            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;

            UserTotalBalanceLosesText.Text = new BinanceCurrencyValueFormatter().Format(result.Value - userData.Balance);
        }



        private async void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;

            startup = new BinanceStartup(await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData);

            refreshUserTotalBalanceAsync();
            calculateUserTotalBalanceLosesAndRefreshTotalBalanceLosesTextAsync();
        }

        private void onRefreshTotalBalanceButtonClick(object sender, EventArgs e)
        {
            refreshUserTotalBalanceAsync();
            calculateUserTotalBalanceLosesAndRefreshTotalBalanceLosesTextAsync();
        }

        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;
            e.Cancel = true;

            BinanceUserWalletResult walletResult = await startup.Wallet.GetTotalBalanceAsync();

            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            userData.Balance = walletResult.Value;
            await new BinanceUserDataWriter().WriteDataAsync(userData);

            base.Close();
        }
    }
}
