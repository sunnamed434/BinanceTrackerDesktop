using Binance.Net.Clients;
using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.ComponentControl.LabelControl;
using BinanceTrackerDesktop.Core.Components.ButtonControl;
using BinanceTrackerDesktop.Core.Components.Safely;
using BinanceTrackerDesktop.Core.DirectoryFiles.Control.Images;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Forms.Authentication;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Balance;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Menu;
using BinanceTrackerDesktop.Core.Forms.Tray;
using BinanceTrackerDesktop.Core.Notification.Popup.Builder;
using BinanceTrackerDesktop.Core.User.Client;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data.Control;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public partial class BinanceTrackerForm : Form
    {
        private readonly ISafelyComponentControl safelyComponentControl;

        private readonly AuthenticatorForm authenticatorForm;

        private UserClient userClient;

        private IUserStatus userStatus;



        public BinanceTrackerForm()
        {
            InitializeComponent();
            
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(DirectoryImagesControl.RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;
            this.RefreshTotalBalanceButton.TabStop = false;
            
            safelyComponentControl = new SafelyComponentControl()
                .OnStarted(() => base.Hide())
                .OnCompleted(() => Environment.FailFast("Close!"));

            new BinanceTrackerTrayForm(safelyComponentControl);

            authenticatorForm = new AuthenticatorForm();
            authenticatorForm.FormClosed += onAuthenticationFormClosed;
            authenticatorForm.OnAuthenticationCompletedSuccessfully += onAuthenticationCompletedSuccessfully;
            authenticatorForm.ShowDialog();

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;
        }



        private void onAuthenticationCompletedSuccessfully()
        {
            authenticatorForm.OnAuthenticationCompletedSuccessfully -= onAuthenticationCompletedSuccessfully;

            authenticatorForm.Hide();
        }

        private void onAuthenticationFormClosed(object? sender, FormClosedEventArgs e)
        {
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WillCloseIn(90)
                .TryWithCarefully()
                .Build(false);

            authenticatorForm.FormClosed -= onAuthenticationFormClosed;
            Environment.FailFast("Authentication failed!");
        }

        private void onFormActivated(object? sender, EventArgs e)
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

        private async void onFormClosing(object? sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            e.Cancel = true;

            await safelyComponentControl.CallListenersAsync();
        }
    }
}
