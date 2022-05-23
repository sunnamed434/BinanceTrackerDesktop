using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.Controllers;
using BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Images;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Core.Themes.Forms;
using BinanceTrackerDesktop.Core.Themes.Recognizers.Windows;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Results;
using BinanceTrackerDesktop.Core.Validators;
using BinanceTrackerDesktop.Core.Validators.String.Extension;
using BinanceTrackerDesktop.Core.Views.Settings;
using System.Text;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.Settings
{
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
}
