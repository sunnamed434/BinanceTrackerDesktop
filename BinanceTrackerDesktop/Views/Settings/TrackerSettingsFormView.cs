using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.DirectoryFiles.Controls.Images;
using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Localizations.Data;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Themes.Forms;
using BinanceTrackerDesktop.Themes.Recognizers.Windows;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Wallet;
using BinanceTrackerDesktop.User.Wallet.Results;
using BinanceTrackerDesktop.Validators.String;
using BinanceTrackerDesktop.Validators.String.Extension;
using BinanceTrackerDesktop.Views.Settings;

namespace BinanceTrackerDesktop.Forms.Tracker.Settings;

public sealed partial class TrackerSettingsFormView : Form, ISettingsView
{
    private readonly UserWallet userWallet;

    private SettingsController controller;



    public TrackerSettingsFormView(UserWallet userWallet)
    {
        InitializeComponent();

        FormsTheme.Apply(this, Controls, new WindowsSystemThemeRecognizer());

        LocalizationData localizationData = LocalizationData.Read();
        base.Text = localizationData.TrackerSettingsViewName;
        base.FormBorderStyle = FormBorderStyle.FixedSingle;
        base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        base.StartPosition = FormStartPosition.CenterParent;
        base.Icon = ApplicationDirectories.Resources.ImagesFolder.Images.GetDirectoryFile(ImagesDirectoryFilesControl.RegisteredImages.ApplicationIcon).GetIcon();
        base.MaximizeBox = false;
        this.userWallet = userWallet ?? throw new ArgumentNullException(nameof(userWallet));
        this.NewCurrenyLabel.Text = localizationData.NewCurrenyLabel;
        this.ChangeCurrencyButton.Text = localizationData.ChangeCurrencyButtonText;

        this.ChangeCurrencyButton.Click += onChangeCurrencyButtonClicked;
    }



    public void SetController(SettingsController controller)
    {
        this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }



    private async void onChangeCurrencyButtonClicked(object sender, EventArgs e)
    {
        IStringValidator userCurrencyValidator = this.NewCurrenyTextBox.Rules()
            .ContentNotNullOrWhiteSpace();

        LocalizationData localizationData = LocalizationData.Read();
        if (userCurrencyValidator.IsFailed)
        {
            new PopupBuilder()
                .WithTitle(localizationData.ApplicationName)
                .WithMessage(string.Format(localizationData.TrackerSettingsView_FailedChangeCurrency_Message, NewCurrenyTextBox.Text))
                .WillCloseIn(90)
                .ShowMessageBoxIfShouldOnBuild()
                .Build();
            return;
        }

        BinaryUserDataSaveSystem userDataSaveSystem = new BinaryUserDataSaveSystem();
        UserData data = userDataSaveSystem.Read();
        data.Currency = NewCurrenyTextBox.Text;
        userDataSaveSystem.Write(data);

        IUserWalletResult userWalletResult = await userWallet.GetTotalBalanceAsync();
        data.BestBalance = userWalletResult.Value;
        userDataSaveSystem.Write(data);

        new PopupBuilder()
            .WithTitle(localizationData.ApplicationName)
            .WithMessage(string.Format(localizationData.TrackerSettingsView_SuccessChangeCurrency_Message, data.Currency))
            .WillCloseIn(90)
            .ShowMessageBoxIfShouldOnBuild()
            .Build();
    }
}