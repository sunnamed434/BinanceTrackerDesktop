using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Forms.Authentication;
using BinanceTrackerDesktop.Core.Notification.Popup.Builder;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.Validators;
using BinanceTrackerDesktop.Core.Validators.String.Extension;
using BinanceTrackerDesktop.Tracker.Forms;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.Images.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Core.Forms.Authorization
{
    public partial class BinanceTrackerAuthorizationForm : Form
    {
        public BinanceTrackerAuthorizationForm()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;

            this.AuthorizeButton.Click += onAuthorizeButtonClicked;
            this.AddAuthenticatorButton.Click += onAddAuthenticatorButtonClicked;

            this.UserCurrenyTextBox.Text = "EUR";
            this.UserKeyTextBox.TextAlign = HorizontalAlignment.Center;
            this.UserSecretTextBox.TextAlign = HorizontalAlignment.Center;
            this.UserCurrenyTextBox.TextAlign = HorizontalAlignment.Center;
        }

        

        private void onAuthorizeButtonClicked(object? sender, EventArgs e)
        {
            IStringValidator userKeyValidator = this.UserKeyTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            IStringValidator userSecretValidator = this.UserSecretTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            IStringValidator userCurrencyValidator = this.UserCurrenyTextBox.Rules()
                .ContentNotNullOrEmpty();

            if (userKeyValidator.IsFailed)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Cannot to authorizate, check your Key please!")
                    .BuildAsMessageBox();
                return;
            }

            if (userSecretValidator.IsFailed)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Cannot to authorizate, check your Secret please!")
                    .BuildAsMessageBox();
                return;
            }

            if (userCurrencyValidator.IsFailed)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Cannot to authorizate, check your Currency please!")
                    .BuildAsMessageBox();
                return;
            }

            try
            {
                UserData userData = new BinaryUserDataSaveSystem().Read();
                if (userData != null)
                {
                    if (userData.AuthenticationData != null)
                    {
                        new UserDataBuilder()
                            .AddKey(this.UserKeyTextBox.Text)
                            .AddSecret(this.UserSecretTextBox.Text)
                            .AddCurrency(this.UserCurrenyTextBox.Text)
                            .AddTwoFactor(userData.AuthenticationData)
                            .AddBalancesStateBasedOnData(userData.IsBalancesHiden)
                            .AddBestBalance(userData.BestBalance)
                            .AddNotificationsStateBasedOnData(userData.IsNotificationsEnabled)
                            .Build()
                            .WriteUserData(new BinaryUserDataSaveSystem());
                    }
                }
                else
                {
                    new UserDataBuilder()
                        .AddKey(this.UserKeyTextBox.Text)
                        .AddSecret(this.UserKeyTextBox.Text)
                        .AddCurrency(this.UserCurrenyTextBox.Text)
                        .Build()
                        .WriteUserData(new BinaryUserDataSaveSystem());
                }

                base.Hide();
                new BinanceTrackerForm().ShowDialog();
                base.Close();
            }
            finally
            {
                this.AuthorizeButton.Click -= onAuthorizeButtonClicked;
                this.AddAuthenticatorButton.Click -= onAddAuthenticatorButtonClicked;
            }
        }

        private void onAddAuthenticatorButtonClicked(object? sender, EventArgs e)
        {
            new AuthenticationForm().ShowDialog();
        }
    }

    public class BinanceAPIKeysCharactersLength
    {
        public const int MaxLengthAPIKey = 64;

        public const int MaxLengthSecretKey = 64;
    }
}
