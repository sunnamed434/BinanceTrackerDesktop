using Binance.Net.Clients;
using BinanceTrackerDesktop.Awaitable.Awaitables;
using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.Core.User.Data.Control;
using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Entry;
using BinanceTrackerDesktop.Forms.Tray;
using BinanceTrackerDesktop.Localizations.Data;
using BinanceTrackerDesktop.MVC.View;
using BinanceTrackerDesktop.Themes.Forms.Design;
using BinanceTrackerDesktop.User.Client;
using BinanceTrackerDesktop.User.Data.Value;
using BinanceTrackerDesktop.User.Status.API;
using BinanceTrackerDesktop.User.Status.Detector;
using BinanceTrackerDesktop.Views.Authenticator;
using BinanceTrackerDesktop.Views.Tracker;
using BinanceTrackerDesktop.Views.Tracker.Menu;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Tracker.Forms;

public sealed partial class TrackerFormView : DesignableForm, IAwaitableSingletonObject, ITrackerView, IAwaitableStart, IAwaitableComplete
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
        base.ApplyTheme(this, Controls);

        base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        base.StartPosition = FormStartPosition.CenterScreen;
        base.FormBorderStyle = FormBorderStyle.FixedSingle;
        base.Icon = ApplicationDirectories.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
        base.MaximizeBox = false;
        this.RefreshTotalBalanceButton.TabStop = false;
        this.TotalBalanceTooltipText.Text = LocalizationData.Read().TotalBalance;

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



    void IView<TrackerController>.SetController(TrackerController controller)
    {
        this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    void IAwaitableStart.OnStart()
    {
        base.Hide();
    }

    void IAwaitableComplete.OnComplete()
    {
        controller.CloseApplication();
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

        controller.CloseApplication();
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

        new TrackerTrayForm();

        userClient = new UserClient();
        userStatus = new UserStatusDetector(UserClient.SaveDataSystem, UserClient.Wallet).GetStatus();
        new BinanceTrackerUserDataSaveControl(UserClient.Wallet);

        new TrackerMenuStripControlUI(this.MenuStrip);
    }
    
    private async void onFormClosing(object sender, FormClosingEventArgs e)
    {
        base.FormClosing -= onFormClosing;

        e.Cancel = true;

        await BinanceTrackerEntryPoint.AwaitablesProvider.Observer.CallListenersAsync();
    }
}
