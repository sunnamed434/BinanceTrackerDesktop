using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.DirectoryFiles.Control;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Notification.Popup.Builder;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using BinanceTrackerDesktop.Core.Validators;
using BinanceTrackerDesktop.Core.Validators.String.Extension;
using System;
using System.Text;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Settings
{
    public partial class BinanceTrackerSettingsForm : Form
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
                    .WithCarefully()
                    .Build(false);
                return;
            }

            BinaryUserDataSaveSystem userDataSaveSystem = new BinaryUserDataSaveSystem();
            UserData data = userDataSaveSystem.Read();
            data.Currency = NewCurrenyTextBox.Text;
            userDataSaveSystem.Save(data);

            UserWalletResult userWalletResult = await userWallet.GetTotalBalanceAsync();
            data.BestBalance = userWalletResult.Value;
            userDataSaveSystem.Save(data);

            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage(new StringBuilder()
                                 .Append("[=)] Successfully changed currency to ")
                                 .Append(data.Currency)
                                 .ToString())
                .WillCloseIn(90)
                .WithCarefully()
                .Build(false);
        }
    }
}
