using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.DirectoryFiles.Controls.Images;
using BinanceTrackerDesktop.DirectoryFiles.Directories;
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
using System.Text;

namespace BinanceTrackerDesktop.Forms.Tracker.Settings;

public sealed partial class TrackerSettingsFormView : Form, ISettingsView
{
    private readonly UserWallet userWallet;

    private SettingsController controller;



    public TrackerSettingsFormView(UserWallet userWallet)
    {
        InitializeComponent();

        FormsTheme.Apply(this, Controls, new WindowsSystemThemeRecognizer());

        base.Text = "Tracker Settings";
        base.FormBorderStyle = FormBorderStyle.FixedSingle;
        base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        base.StartPosition = FormStartPosition.CenterParent;
        base.Icon = ApplicationDirectories.Resources.Images.GetDirectoryFile(ImagesDirectoryFilesControl.RegisteredImages.ApplicationIcon).GetIcon();
        base.MaximizeBox = false;
        this.userWallet = userWallet ?? throw new ArgumentNullException(nameof(userWallet));

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

        if (userCurrencyValidator.IsFailed)
        {
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage(new StringBuilder()
                             .Append("[=(] Failed to change currency to ")
                             .Append(NewCurrenyTextBox.Text))
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
            .WithTitle(ApplicationEnviroment.GlobalName)
            .WithMessage(new StringBuilder()
                             .Append("[=)] Successfully changed currency to ")
                             .Append(data.Currency))
            .WillCloseIn(90)
            .ShowMessageBoxIfShouldOnBuild()
            .Build();
    }
}