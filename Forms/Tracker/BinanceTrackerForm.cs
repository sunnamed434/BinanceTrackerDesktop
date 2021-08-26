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
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public partial class BinanceTrackerForm : Form, IFormControl
    {
        private BinanceStartup startup;

        private readonly FormEventListenerBase formMoveEventListener;

        private readonly FormEventListenerBase trayDoubleClickEventListener;

        private readonly TrayApplicationOpenClickEventListener applicationOpenClickEventListener;

        private readonly TrayDisableNotificationsClickEventListener disableNotificationsClickEventListener;

        private readonly TrayApplicationQuitClickEventListener applicationQuitClickEventListener;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.MaximizeBox = false;
            base.StartPosition = FormStartPosition.CenterScreen;

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;
            base.Move += onBinanceTrackerFormMove;

            this.BinanceTrackerNotifyIcon.DoubleClick += onBinanceTrackerNotifyIconDoubleClick;
            this.OpenBinanceTrackerToolStripMenuItem.Click += onOpenBinanceTrackerToolStripMenuItemClick;
            this.DisableNotificationsToolStripMenuItem.Click += onDisableNotificationsToolStripMenuItemClick;
            this.QuitBinanceTrackerToolStripMenuItem.Click += onQuitBinanceTrackerToolStripMenuItemClick;

            new BinanceTrackerMoveListener(this, formMoveEventListener = new FormEventListenerBase(), new BinanceTrackerNotificationsControl(new NotificationsControl(BinanceTrackerNotifyIcon)));
            new BinanceTrackerTray(this,
                trayDoubleClickEventListener = new FormEventListenerBase(),
                applicationOpenClickEventListener = new TrayApplicationOpenClickEventListener(),
                disableNotificationsClickEventListener = new TrayDisableNotificationsClickEventListener(),
                applicationQuitClickEventListener = new TrayApplicationQuitClickEventListener());
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
            BinanceUserWalletResult result = await startup.Wallet.GetTotalBalanceAsync();
            UserTotalBalanceText.Text = new BinanceCurrencyValueFormatter().Format(result.Value);
        }

        private void onBinanceTrackerFormMove(object sender, EventArgs e)
        {
            formMoveEventListener.TriggerEvent(sender, e);
        }
        
        private void onOpenBinanceTrackerToolStripMenuItemClick(object sender, EventArgs e)
        {
            applicationOpenClickEventListener.TriggerEvent(sender, e);
        }

        private void onBinanceTrackerNotifyIconDoubleClick(object sender, EventArgs e)
        {
            trayDoubleClickEventListener.TriggerEvent(sender, e);
        }

        private void onDisableNotificationsToolStripMenuItemClick(object sender, EventArgs e)
        {
            disableNotificationsClickEventListener.TriggerEvent(sender, e);
        }

        private void onQuitBinanceTrackerToolStripMenuItemClick(object sender, EventArgs e)
        {
            applicationQuitClickEventListener.TriggerEvent(sender, e);
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
