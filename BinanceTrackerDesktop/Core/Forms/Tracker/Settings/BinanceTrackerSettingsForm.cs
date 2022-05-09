using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.DirectoryFiles.Control.Images;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using BinanceTrackerDesktop.Core.Validators;
using BinanceTrackerDesktop.Core.Validators.String.Extension;
using System.Text;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.Settings
{
    public sealed partial class BinanceTrackerSettingsForm : Form
    {
        private readonly UserWallet userWallet;



        public BinanceTrackerSettingsForm(UserWallet userWallet)
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(DirectoryImagesControl.RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;

            this.ChangeCurrencyButton.Click += onChangeCurrencyButtonClicked;
            this.userWallet = userWallet;
        }



        private async void onChangeCurrencyButtonClicked(object sender, EventArgs e)
        {
            IStringValidator userCurrencyValidator = this.NewCurrenyTextBox.Rules()
                .ContentNotNullOrEmpty();

            if (userCurrencyValidator.IsFailed)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage(new StringBuilder()
                                 .Append("[=(] Failed to change currency to ")
                                 .Append(NewCurrenyTextBox.Text)
                                 .ToString())
                    .WillCloseIn(90)
                    .ShowMessageBoxIfShouldOnBuild()
                    .Build();
                return;
            }

            BinaryUserDataSaveSystem userDataSaveSystem = new BinaryUserDataSaveSystem();
            UserData data = userDataSaveSystem.Read();
            data.Currency = NewCurrenyTextBox.Text;
            userDataSaveSystem.Write(data);

            UserWalletResult userWalletResult = await userWallet.GetTotalBalanceAsync();
            data.BestBalance = userWalletResult.Value;
            userDataSaveSystem.Write(data);

            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage(new StringBuilder()
                                 .Append("[=)] Successfully changed currency to ")
                                 .Append(data.Currency)
                                 .ToString())
                .WillCloseIn(90)
                .ShowMessageBoxIfShouldOnBuild()
                .Build();
        }
    }
}
