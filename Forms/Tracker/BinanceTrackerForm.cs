using BinanceTrackerDesktop.Core.Extension;
using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.Startup;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.UserData.API.Extension;
using BinanceTrackerDesktop.Forms.SystemTray;
using BinanceTrackerDesktop.Forms.SystemTray.API;
using BinanceTrackerDesktop.Forms.Tracker.API;
using BinanceTrackerDesktop.Forms.Tracker.MoveListener;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using BinanceTrackerDesktop.Forms.Tracker.Notifications.API;
using BinanceTrackerDesktop.Forms.Tracker.UserData;
using ConsoleBinanceTracker.Core.Wallet.API;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BinanceTrackerDesktop.Core.Formatters.API.BinanceUserBalanceLosesColorFormatter;

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

            this.RefreshTotalBalanceButton.Click += onRefreshTotalBalanceButtonClick;

            new BinanceTrackerMoveListener(this, new BinanceTrackerNotificationsControl(new StableNotificationsControl((new BinanceTrackerSystemTrayForm(this) as ISystemTrayFormControl)?.NotifyIcon)));
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

            changeUserTotalBalanceText(new BinanceCurrencyValueFormatter().Format(result.Value));
        }

        private async void refreshUserBalanceLoses(Action onStartedCallback = null, Action onCompletedCallback = null)
        {
            onStartedCallback?.Invoke();

            BinanceUserWalletResult walletResult = await startup.Wallet.GetTotalBalanceAsync();
            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;

            changeUserTotalBalanceLosesTextColor(getColorFromUserTotalBalanceLoses(new BinanceUserBalanceLosesOptions(walletResult, userData)));
            changeUserTotalBalanceLosesText(new BinanceCurrencyValueFormatter().Format(walletResult.Value - userData.Balance));

            onCompletedCallback?.Invoke();
        }

        private void changeUserTotalBalanceText(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            UserTotalBalanceText.Text = content;
        }

        private void changeUserTotalBalanceLosesText(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            UserTotalBalanceLosesText.Text = content;
        }

        private void changeUserTotalBalanceLosesTextColor(Color color)
        {
            if (color == Color.Empty)
            {
                throw new ArgumentNullException(nameof(color));
            }

            UserTotalBalanceLosesText.ForeColor = color;
        }

        private Color getColorFromUserTotalBalanceLoses(BinanceUserBalanceLosesOptions options)
        {
            return new BinanceUserBalanceLosesColorFormatter().Format(options);
        }



        private async void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;

            startup = new BinanceStartup(await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData);

            new BinanceTrackerUserDataSaver(this, startup.Wallet);

            refreshUserTotalBalanceAsync();
            refreshUserBalanceLoses(() => RefreshTotalBalanceButton.Enabled = false, () => RefreshTotalBalanceButton.Enabled = true);
        }

        private void onRefreshTotalBalanceButtonClick(object sender, EventArgs e)
        {
            refreshUserTotalBalanceAsync();
            refreshUserBalanceLoses(() => RefreshTotalBalanceButton.Enabled = false, () => RefreshTotalBalanceButton.Enabled = true);
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            this.RefreshTotalBalanceButton.Click -= onRefreshTotalBalanceButtonClick;

            base.Hide();
        }
    }
}
