using Binance.Net.Clients;
using BinanceTrackerDesktop.Core.ComponentControl.LabelControl;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component;
using BinanceTrackerDesktop.Core.Components.ButtonControl;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Entry;
using BinanceTrackerDesktop.Core.Forms.Authentication;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Balance;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Menu;
using BinanceTrackerDesktop.Core.Forms.Tray;
using BinanceTrackerDesktop.Core.Themes.Detectors;
using BinanceTrackerDesktop.Core.Themes.Provider;
using BinanceTrackerDesktop.Core.User.Client;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data.Control;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Status.Detector;
using BinanceTrackerDesktop.Core.User.Theme.Repositories;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.Images.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public sealed partial class BinanceTrackerForm : Form, IAwaitableSingletonObject, IAwaitableComponentStart, IAwaitableComponentComplete
    {
        private readonly AuthenticatorForm authenticatorForm;

        private UserClient userClient;

        private IUserStatus userStatus;

        private static BinanceTrackerForm instance;



        public BinanceTrackerForm()
        {
            instance = this;

            InitializeComponent();

            themable = this;
            ThemesProvider = new ThemesProvider(new ThemeDetector(new UserThemeRepository(new BinaryUserDataSaveSystem())));
            themable.ApplyTheme();

            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;
            this.RefreshTotalBalanceButton.TabStop = false;

            if (new BinaryUserDataSaveSystem().Read().HasAuthenticationData)
            {
                authenticatorForm = new AuthenticatorForm();
                authenticatorForm.FormClosed += onAuthenticationFormClosed;
                authenticatorForm.OnAuthenticationCompletedSuccessfully += onAuthenticationCompletedSuccessfully;
                authenticatorForm.ShowDialog();
                return;
            }

            base.Activated += onFormActivated;
        }



        object IAwaitableSingletonObject.Instance => instance;



        void IAwaitableComponentStart.OnStart()
        {
            base.Hide();
        }

        void IAwaitableComponentComplete.OnComplete()
        {
            Application.Exit();
        }



        private void onAuthenticationCompletedSuccessfully()
        {
            authenticatorForm.OnAuthenticationCompletedSuccessfully -= onAuthenticationCompletedSuccessfully;

            onFormActivated(this, null);

            authenticatorForm.Hide();
        }

        private void onAuthenticationFormClosed(object sender, FormClosedEventArgs e)
        {
            authenticatorForm.FormClosed -= onAuthenticationFormClosed;

            Application.Exit();
        }

        private void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;
            base.FormClosing += onFormClosing;

            new BinanceTrackerTrayForm();

            userClient = new UserClient();
            userStatus = new UserStatusDetector(userClient.SaveDataSystem, userClient.Wallet).GetStatus();
            new BinanceTrackerUserDataSaveControl(userClient.Wallet);

            new BinanceTrackerUserBalanceControlUI(userStatus,
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

            await BinanceTrackerEntryPoint.AwaitableComponentsProvider.Observer.CallListenersAsync();
        }
    }
}
