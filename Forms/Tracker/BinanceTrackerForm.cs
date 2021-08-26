using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.Startup;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.Tracker.API;
using BinanceTrackerDesktop.Forms.Tracker.MoveListener;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using BinanceTrackerDesktop.Forms.Tracker.Notifications.API;
using BinanceTrackerDesktop.Forms.Tracker.Tray;
using ConsoleBinanceTracker.Core.Wallet.API;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public partial class BinanceTrackerForm : Form, IFormControl
    {
        private BinanceStartup startup;

        private readonly IFormEventListener[] formEventListeners;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            intitializeForm();

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;
            base.Move += onBinanceTrackerFormMove;

            this.BinanceTrackerNotifyIcon.DoubleClick += onBinanceTrackerNotifyIconDoubleClick;
            this.OpenBinanceTrackerToolStripMenuItem.Click += onOpenBinanceTrackerToolStripMenuItemClick;
            this.DisableNotificationsToolStripMenuItem.Click += onDisableNotificationsToolStripMenuItemClick;
            this.QuitBinanceTrackerToolStripMenuItem.Click += onQuitBinanceTrackerToolStripMenuItemClick;

            new BinanceTrackerTray(this, formEventListeners = new FormEventListener[]
            {
                new FormEventListener(),
                new FormEventListener(),
                new FormEventListener(),
                new FormEventListener(),
                new FormEventListener(),
            });

            new BinanceTrackerMoveListener(this, formEventListeners[4], new BinanceTrackerNotificationsControl(new NotificationsControl(BinanceTrackerNotifyIcon)));
        }



        private void intitializeForm()
        {
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.MaximizeBox = false;
        }



        private async void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;

            startup = new BinanceStartup(await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData);

            BinanceUserWalletResult result = await startup.Wallet.GetTotalBalanceAsync();
            UserTotalBalanceText.Text = new BinanceCurrencyValueFormatter().Format(result.Value);
        }

        private async void onRefreshTotalBalanceButtonClick(object sender, EventArgs e)
        {
            var test = await startup.Wallet.GetTotalBalanceAsync();
            UserTotalBalanceText.Text = new BinanceCurrencyValueFormatter().Format(test.Value);
        }

        private void onBinanceTrackerNotifyIconDoubleClick(object sender, EventArgs e)
        {
            formEventListeners[0].TriggerEvent(sender, e);
        }
        
        private void onOpenBinanceTrackerToolStripMenuItemClick(object sender, EventArgs e)
        {
            formEventListeners[1].TriggerEvent(sender, e);
        }

        private void onDisableNotificationsToolStripMenuItemClick(object sender, EventArgs e)
        {
            formEventListeners[2].TriggerEvent(sender, e);
        }

        private void onQuitBinanceTrackerToolStripMenuItemClick(object sender, EventArgs e)
        {
            formEventListeners[3].TriggerEvent(sender, e);
        }

        private void onBinanceTrackerFormMove(object sender, EventArgs e)
        {
            formEventListeners[4].TriggerEvent(sender, e);
        }

        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.Activated -= onFormActivated;
            base.FormClosing -= onFormClosing;
            base.Move -= onBinanceTrackerFormMove;

            this.BinanceTrackerMenuStrip.DoubleClick -= onBinanceTrackerNotifyIconDoubleClick;
            this.OpenBinanceTrackerToolStripMenuItem.Click -= onOpenBinanceTrackerToolStripMenuItemClick;
            this.DisableNotificationsToolStripMenuItem.Click -= onDisableNotificationsToolStripMenuItemClick;
            this.QuitBinanceTrackerToolStripMenuItem.Click -= onQuitBinanceTrackerToolStripMenuItemClick;

            BinanceUserWalletResult wallet = await startup.Wallet.GetTotalBalanceAsync();

            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            userData.Balance = wallet.Value;
            await new BinanceUserDataWriter().WriteDataAsync(userData);
        }
    }
}
