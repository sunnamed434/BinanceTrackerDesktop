using Binance.Net;
using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.ComponentControl.LabelControl.API;
using BinanceTrackerDesktop.Core.Components.ButtonControl.API;
using BinanceTrackerDesktop.Core.DirectoryFiles.API;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Balance;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Menu;
using BinanceTrackerDesktop.Core.Forms.Tray;
using BinanceTrackerDesktop.Core.Startup;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.User.Data.Control;
using System;
using System.Windows.Forms;
using static BinanceTrackerDesktop.Core.DirectoryFiles.API.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public partial class BinanceTrackerForm : Form
    {
        private readonly ISafelyComponentControl safelyComponentControl;

        private BinanceStartup startup;

        private IBinanceUserStatus userStatus;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFileAt(RegisteredImages.ApplicationIcon).Icon;
            base.MaximizeBox = false;
            this.RefreshTotalBalanceButton.TabStop = false;

            safelyComponentControl = new SafelyComponentControl()
                .OnStarted(() => base.Hide())
                .OnCompleted(() => Application.Exit());

            new BinanceTrackerTrayForm(safelyComponentControl);

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;
        }



        private void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;

            UserData data = new BinaryUserDataSaveReadSystem().Read();
            startup = new BinanceStartup(data);

            userStatus = new BinanceUserStatusDetector(data, startup.Wallet).GetStatus();
            new BinanceTrackerUserDataSaveControl(safelyComponentControl, startup.Wallet);

            new BinanceTrackerUserBalanceUIControl(safelyComponentControl, userStatus,
            new ButtonComponentControl[]
            {
                new ButtonComponentControl(this.RefreshTotalBalanceButton),
            },
            new LabelComponentControl[]
            {
                new LabelComponentControl(this.UserTotalBalanceText),
                new LabelComponentControl(this.UserTotalBalanceLosesText),
            });

            new BinanceTrackerMenuUIControl(this.MenuStrip, new BinanceClient());
        }

        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            e.Cancel = true;

            await safelyComponentControl.CallListenersAsync();
        }
    }
}
