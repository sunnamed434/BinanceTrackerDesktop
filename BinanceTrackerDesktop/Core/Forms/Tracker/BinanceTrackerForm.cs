using BinanceTrackerDesktop.Core.ComponentControl.FormButton.API;
using BinanceTrackerDesktop.Core.ComponentControl.FormText.API;
using BinanceTrackerDesktop.Core.Files.API;
using BinanceTrackerDesktop.Core.Forms.API;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Balance.API;
using BinanceTrackerDesktop.Core.Forms.Tray;
using BinanceTrackerDesktop.Core.Forms.UserDataControl;
using BinanceTrackerDesktop.Core.Startup;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.UserStatus.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public partial class BinanceTrackerForm : Form, IFormControl
    {
        private readonly IFormSafelyComponentControl safelyComponentControl;

        private BinanceStartup startup;

        private IBinanceUserStatus userStatus;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = new ApplicationDirectoryControl().Directories.Images.GetDirectoryFileAt(DirectoryIcons.ApplicationIcon).Icon;
            base.MaximizeBox = false;
            this.RefreshTotalBalanceButton.TabStop = false;

            safelyComponentControl = new FormSafelyComponentControl()
                .OnStarted(() => base.Hide())
                .OnCompleted(() => Application.Exit());

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;
        }



        private async void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;

            BinanceUserData data = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            startup = new BinanceStartup(data);

            userStatus = new BinanceUserStatusDetector(data, startup.Wallet).GetStatus();
            new BinanceTrackerUserDataSaveControl(safelyComponentControl, startup.Wallet);

            new BinanceTrackerUserBalanceUIControl(safelyComponentControl, userStatus,
            new FormButtonControl[]
            {
                new FormButtonControl(this.RefreshTotalBalanceButton),
            },
            new FormTextControl[]
            {
                new FormTextControl(this.UserTotalBalanceText),
                new FormTextControl(this.UserTotalBalanceLosesText),
            });

            new BinanceTrackerTrayForm(safelyComponentControl);
        }

        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            e.Cancel = true;

            await safelyComponentControl.CallListenersAsync();
        }
    }
}
