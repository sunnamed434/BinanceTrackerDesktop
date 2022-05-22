using Binance.Net.Clients;
using BinanceTrackerDesktop.Core.Awaitable.Awaitables;
using BinanceTrackerDesktop.Core.ComponentControl.LabelControl;
using BinanceTrackerDesktop.Core.Components.ButtonControl;
using BinanceTrackerDesktop.Core.Controllers;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Entry;
using BinanceTrackerDesktop.Core.Forms.Authentication;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Balance;
using BinanceTrackerDesktop.Core.Forms.Tracker.UI.Menu;
using BinanceTrackerDesktop.Core.Forms.Tray;
using BinanceTrackerDesktop.Core.Themes.Forms;
using BinanceTrackerDesktop.Core.Themes.Recognizers.Windows;
using BinanceTrackerDesktop.Core.User.Client;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data.Control;
using BinanceTrackerDesktop.Core.User.Data.Value;
using BinanceTrackerDesktop.Core.User.Status.Detector;
using BinanceTrackerDesktop.Core.Views.Tracker;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public sealed partial class TrackerFormView : Form, IAwaitableSingletonObject, ITrackerView, IAwaitableStart, IAwaitableComplete
    {
        private readonly AuthenticatorFormView authenticatorForm;

        private UserClient userClient;

        private IUserStatus userStatus;

        private TrackerController controller;

        private static TrackerFormView instance;



        public TrackerFormView()
        {
            instance = this;

            InitializeComponent();

            FormsTheme.Apply(this, Controls, new WindowsSystemThemeRecognizer());

            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ApplicationDirectories.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;
            this.RefreshTotalBalanceButton.TabStop = false;

            if (UserDataValues.HasAuthenticationData.GetValue())
            {
                authenticatorForm = new AuthenticatorFormView();
                authenticatorForm.FormClosed += onAuthenticationFormClosed;
                authenticatorForm.OnAuthenticationCompletedSuccessfully += onAuthenticationCompletedSuccessfully;
                authenticatorForm.ShowDialog();
                return;
            }

            base.Activated += onFormActivated;
            this.RefreshTotalBalanceButton.MouseClick += onRefreshTotalBalanceButtonClicked;
            this.UserTotalBalanceText.MouseClick += onUserBalancesTextClicked;
            this.UserTotalBalanceLossesText.MouseClick += onUserBalancesTextClicked;
        }

        

        object IAwaitableSingletonObject.Instance => instance;

        public string TotalBalanceText
        {
            get
            {
                return this.UserTotalBalanceText.Text;
            }
            set
            {
                this.UserTotalBalanceText.Text = value;
            }
        }

        public string TotalBalanceLossesText
        {
            get
            {
                return this.UserTotalBalanceLossesText.Text;
            }
            set
            {
                this.UserTotalBalanceLossesText.Text = value;
            }
        }

        public Color TotalBalanceLossesTextColor
        {
            get
            {
                return this.UserTotalBalanceLossesText.ForeColor;
            }
            set
            {
                this.UserTotalBalanceLossesText.ForeColor = value;
            }
        }

        public bool RefreshTotalBalanceButtonEnableState
        {
            get
            {
                return this.RefreshTotalBalanceButton.Enabled;
            }
            set
            {
                this.RefreshTotalBalanceButton.Enabled = value;
            }
        }



        public void SetController(TrackerController controller)
        {
            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        public void SetTextsHidenState()
        {
            TotalBalanceText = BinanceTrackerBalanceTextValues.Hiden;
            TotalBalanceLossesText = BinanceTrackerBalanceTextValues.Hiden;
        }

        public void SetTextsInitializingState()
        {
            TotalBalanceText = BinanceTrackerBalanceTextValues.Initializing;
            TotalBalanceLossesText = BinanceTrackerBalanceTextValues.Initializing;
        }



        void IAwaitableStart.OnStart()
        {
            base.Hide();
        }

        void IAwaitableComplete.OnComplete()
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

        private void onRefreshTotalBalanceButtonClicked(object sender, MouseEventArgs e)
        {
            this.controller.RefreshTotalBalance();
        }

        private void onUserBalancesTextClicked(object sender, MouseEventArgs e)
        {
            this.controller.ToggleTextsState();
        }

        private void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;
            base.FormClosing += onFormClosing;

            new BinanceTrackerTrayForm();

            userClient = new UserClient();
            userStatus = new UserStatusDetector(userClient.SaveDataSystem, userClient.Wallet).GetStatus();
            new BinanceTrackerUserDataSaveControl(userClient.Wallet);

            //new BinanceTrackerUserBalanceControlUI(userStatus,
            /*new ButtonComponentControl[]
            {
                new ButtonComponentControl(this.RefreshTotalBalanceButton),
            },
            new LabelComponentControl[]
            {
                new LabelComponentControl(this.UserTotalBalanceText),
                new LabelComponentControl(this.UserTotalBalanceLossesText),
            });*/

            new BinanceTrackerMenuStripControlUI(this.MenuStrip, new BinanceClient(), userClient.Wallet);
        }
        
        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            e.Cancel = true;

            await BinanceTrackerEntryPoint.AwaitablesProvider.Observer.CallListenersAsync();
        }
    }

    public sealed class BinanceTrackerBalanceTextValues
    {
        public const string Initializing = "-----";

        public const string Hiden = "*****";
    }
}
