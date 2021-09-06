using BinanceTrackerDesktop.Core.Controls.FormButton.API;
using BinanceTrackerDesktop.Core.Controls.FormText.API;
using BinanceTrackerDesktop.Core.Startup;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.API;
using BinanceTrackerDesktop.Forms.SystemTray;
using BinanceTrackerDesktop.Forms.SystemTray.API;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using BinanceTrackerDesktop.Forms.Tracker.Notifications.API;
using BinanceTrackerDesktop.Forms.Tracker.Startup.API;
using BinanceTrackerDesktop.Forms.Tracker.Startup.Control;
using BinanceTrackerDesktop.Forms.Tracker.UI;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public partial class BinanceTrackerForm : Form, IFormControl
    {
        public readonly FormSafelyCloseControl SafelyCloseControl;



        private BinanceStartup startup;

        private IBinanceUserStatus userStatus;

        private IFormEventListener[] textClickEventListeners;

        private IFormEventListener refreshTotalBalanceEventListener;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            intitializeForm();

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;

            this.RefreshTotalBalanceButton.Click += onRefreshTotalBalanceButtonClick;
            this.UserTotalBalanceText.Click += onUserTotalBalanceTextClicked;
            this.UserTotalBalanceLosesText.Click += onUserTotalBalanceLosesTextClicked;

            textClickEventListeners = new IFormEventListener[]
            {
                new FormEventListener(),
                new FormEventListener(),
            };

            SafelyCloseControl = new FormSafelyCloseControl();
            new BinanceTrackerNotificationsControl(new StableNotificationsControl((new BinanceTrackerSystemTrayForm(SafelyCloseControl) as ISystemTrayFormControl)?.NotifyIcon));
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

            BinanceUserData data = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            startup = new BinanceStartup(data);

            userStatus = new BinanceUserStatusDetector(data, startup.Wallet).GetStatus();
            new BinanceTrackerApplicationControl(this, SafelyCloseControl, startup.Wallet);
            new BinanceTrackerUserBalanceUIControl
            (SafelyCloseControl, userStatus,
            new FormButtonControl[]
            {
                new FormButtonControl(this.RefreshTotalBalanceButton, refreshTotalBalanceEventListener = new FormEventListener()),
            },
            new FormTextControl[]
            {
                new FormTextControl(this.UserTotalBalanceText, textClickEventListeners[0]),
                new FormTextControl(this.UserTotalBalanceLosesText, textClickEventListeners[1]),
            });
        }

        private void onRefreshTotalBalanceButtonClick(object sender, EventArgs e)
        {
            refreshTotalBalanceEventListener.TriggerEvent(sender, e);
        }

        private void onUserTotalBalanceTextClicked(object sender, EventArgs e)
        {
            textClickEventListeners[0].TriggerEvent(sender, e);
        }

        private void onUserTotalBalanceLosesTextClicked(object sender, EventArgs e)
        {
            textClickEventListeners[1].TriggerEvent(sender, e);
        }

        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            this.RefreshTotalBalanceButton.Click -= onRefreshTotalBalanceButtonClick;
            this.UserTotalBalanceText.Click -= onUserTotalBalanceTextClicked;
            this.UserTotalBalanceLosesText.Click -= onUserTotalBalanceLosesTextClicked;

            e.Cancel = true;

            await SafelyCloseControl.CloseApplicationSafelyAndNotifyListenersAsync();
        }
    }
}
