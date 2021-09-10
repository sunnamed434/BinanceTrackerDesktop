using BinanceTrackerDesktop.Core.Control.FormButton.API;
using BinanceTrackerDesktop.Core.Control.FormText.API;
using BinanceTrackerDesktop.Core.Files.API;
using BinanceTrackerDesktop.Core.Startup;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.API;
using BinanceTrackerDesktop.Forms.SystemTray;
using BinanceTrackerDesktop.Forms.Tracker.Startup.API;
using BinanceTrackerDesktop.Forms.Tracker.Startup.Control;
using BinanceTrackerDesktop.Forms.Tracker.UI;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public partial class BinanceTrackerForm : Form, IFormControl, IFormCompletedEvent
    {
        private readonly IFormSafelyComponentControl safelyComponentControl;

        private BinanceStartup startup;

        private IBinanceUserStatus userStatus;

        private IFormEventListener[] textClickEventListeners;

        private IFormEventListener refreshTotalBalanceEventListener;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = new ApplicationDirectoryControl().Directories.Images.GetDirectoryFileAt(DirectoryIcons.ApplicationIcon).Icon;
            base.MaximizeBox = false;

            textClickEventListeners = new IFormEventListener[]
            {
                new FormEventListener(),
                new FormEventListener(),
            };

            safelyComponentControl = new FormSafelyComponentControl(this);
            new BinanceTrackerSystemTrayForm(safelyComponentControl);

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;

            this.RefreshTotalBalanceButton.Click += onRefreshTotalBalanceButtonClick;
            this.UserTotalBalanceText.Click += onUserTotalBalanceTextClicked;
            this.UserTotalBalanceLosesText.Click += onUserTotalBalanceLosesTextClicked;
        }



        private async void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;

            BinanceUserData data = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            startup = new BinanceStartup(data);

            userStatus = new BinanceUserStatusDetector(data, startup.Wallet).GetStatus();
            new BinanceTrackerApplicationControl(this, safelyComponentControl, startup.Wallet);
            new BinanceTrackerUserBalanceUIControl
            (safelyComponentControl, userStatus,
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
            refreshTotalBalanceEventListener.TriggerEvent(e);
        }

        private void onUserTotalBalanceTextClicked(object sender, EventArgs e)
        {
            textClickEventListeners[0].TriggerEvent(e);
        }

        private void onUserTotalBalanceLosesTextClicked(object sender, EventArgs e)
        {
            textClickEventListeners[1].TriggerEvent(e);
        }

        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            this.RefreshTotalBalanceButton.Click -= onRefreshTotalBalanceButtonClick;
            this.UserTotalBalanceText.Click -= onUserTotalBalanceTextClicked;
            this.UserTotalBalanceLosesText.Click -= onUserTotalBalanceLosesTextClicked;

            e.Cancel = true;

            await safelyComponentControl.CallListenersAsync();
        }

        public void OnCompleted()
        {
            Application.Exit();
        }
    }
}
