using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.Startup;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.API;
using BinanceTrackerDesktop.Forms.SystemTray;
using BinanceTrackerDesktop.Forms.SystemTray.API;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using BinanceTrackerDesktop.Forms.Tracker.Notifications.API;
using BinanceTrackerDesktop.Forms.Tracker.Startup.API;
using BinanceTrackerDesktop.Forms.Tracker.Startup.Control;
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

        private IBinanceUserStatus userStatus;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            intitializeForm();

            changeUserTotalBalanceText("-----------");
            changeUserTotalBalanceLosesText("-----------");

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;

            this.RefreshTotalBalanceButton.Click += onRefreshTotalBalanceButtonClick;

            new BinanceTrackerNotificationsControl(new StableNotificationsControl((new BinanceTrackerSystemTrayForm(this) as ISystemTrayFormControl)?.NotifyIcon));
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
            IBinanceUserStatusResult totalBalanceResult = await userStatus.CalculateUserTotalBalanceAsync();

            changeUserTotalBalanceText(userStatus.Format(totalBalanceResult.Value));

            await Task.CompletedTask;
        }

        private async void refreshUserBalanceLosses(Action onStartedCallback = null, Action onCompletedCallback = null)
        {
            onStartedCallback?.Invoke();

            BinanceUserData data = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;

            IBinanceUserStatusResult balanceLossesResult = await userStatus.CalculateUserBalanceLossesAsync();
            changeUserTotalBalanceLosesText(userStatus.Format(balanceLossesResult.Value));
            changeUserTotalBalanceLosesTextColor(getColorFromUserTotalBalanceLoses(new BinanceUserBalanceLossesOptions(balanceLossesResult.Value, data)));

            onCompletedCallback?.Invoke();
        }

        private void changeUserTotalBalanceText(string content)
        {
            this.UserTotalBalanceText.Text = content;
        }

        private void changeUserTotalBalanceLosesText(string content)
        {
            this.UserTotalBalanceLosesText.Text = content;
        }

        private void changeUserTotalBalanceLosesTextColor(Color color)
        {
            this.UserTotalBalanceLosesText.ForeColor = color;
        }

        private Color getColorFromUserTotalBalanceLoses(BinanceUserBalanceLossesOptions options)
        {
            return new BinanceUserBalanceLosesColorFormatter().Format(options);
        }



        private async void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;

            BinanceUserData data = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            startup = new BinanceStartup(data);

            userStatus = new BinanceUserStatusDetector(data, startup.Wallet).GetStatus();
            new BinanceTrackerApplicationControl(this, startup.Wallet);

            refreshUserTotalBalanceAsync();
            refreshUserBalanceLosses(() => RefreshTotalBalanceButton.Enabled = false, () => RefreshTotalBalanceButton.Enabled = true);
        }

        private void onRefreshTotalBalanceButtonClick(object sender, EventArgs e)
        {
            refreshUserTotalBalanceAsync();
            refreshUserBalanceLosses(() => RefreshTotalBalanceButton.Enabled = false, () => RefreshTotalBalanceButton.Enabled = true);
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            this.RefreshTotalBalanceButton.Click -= onRefreshTotalBalanceButtonClick;
        }
    }
}
