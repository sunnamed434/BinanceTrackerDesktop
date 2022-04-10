using Binance.Net.Clients;
using BinanceTrackerDesktop.Core.ComponentControl.LabelControl;
using BinanceTrackerDesktop.Core.Components.API;
using BinanceTrackerDesktop.Core.Components.ButtonControl;
using BinanceTrackerDesktop.Core.DirectoryFiles;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Balance;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Menu;
using BinanceTrackerDesktop.Core.Forms.Tray;
using BinanceTrackerDesktop.Core.User.Client;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data.Control;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public partial class BinanceTrackerForm : Form
    {
        private readonly ISafelyComponentControl safelyComponentControl;

        private UserClient userClient;

        private IUserStatus userStatus;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = (Icon)new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFileAt(DirectoryImagesControl.RegisteredImages.ApplicationIcon).Result;
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

            userClient = new UserClient();

            userStatus = new UserStatusDetector(userClient.SaveDataSystem, userClient.Wallet).GetStatus();
            new BinanceTrackerUserDataSaveControl(safelyComponentControl, userClient.Wallet);

            new BinanceTrackerUserBalanceControlUI(safelyComponentControl, userStatus,
            new ButtonComponentControl[]
            {
                new ButtonComponentControl(this.RefreshTotalBalanceButton),
            },
            new LabelComponentControl[]
            {
                new LabelComponentControl(this.UserTotalBalanceText),
                new LabelComponentControl(this.UserTotalBalanceLosesText),
            });

            new BinanceTrackerMenuStripControlUI(this.MenuStrip, new BinanceClient(), userClient.Wallet);
        }

        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            e.Cancel = true;

            await safelyComponentControl.CallListenersAsync();
        }
    }
}
