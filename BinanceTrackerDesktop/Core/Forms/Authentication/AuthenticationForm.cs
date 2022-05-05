using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exception;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exception.ErrorCode;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Notification.Popup.Builder;
using BinanceTrackerDesktop.Core.User.Authentication.Data;
using BinanceTrackerDesktop.Core.User.Authentication.System;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Core.Forms.Authentication
{
    public partial class AuthenticationForm : Form
    {
        private const int QRCodePictureBoxHiddenSizeValue = 0;



        public AuthenticationForm()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;

            this.QRCodePictureBox.Size = new Size(QRCodePictureBoxHiddenSizeValue, QRCodePictureBoxHiddenSizeValue);
            this.GenerateQRCodeButton.Click += onGenerateQRCodeButtonClicked;
        }



        private void onGenerateQRCodeButtonClicked(object? sender, EventArgs e)
        {
            try
            {
                Image image = null;
                if ((image = new UserAuthenticatorSystem().Authenticate(this.AccountTitleTextBox.Text, this.SecretKeyTextBox.Text)) != null)
                {
                    this.QRCodePictureBox.Size = image.Size;
                    this.QRCodePictureBox.Image = image;
                }
            }
            catch (TwoFactorAuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCode.AccountTitle)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Cannot to authenticate, check your Account Title please!")
                    .BuildAsMessageBox();
            }
            catch (TwoFactorAuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCode.Secret)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Cannot to authenticate, check your Secret Key please!")
                    .BuildAsMessageBox();
            }

            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Successfully created QRCode and saved.")
                .BuildAsMessageBox();

            BinaryUserDataSaveSystem saveSystem = new BinaryUserDataSaveSystem();
            if (saveSystem.Read() != null)
            {
                new UserDataBuilder()
                    .ReadExistingUserDataAndCacheAll(saveSystem)
                    .AddTwoFactor(new UserTwoFactorAuthenticationData(this.SecretKeyTextBox.Text))
                    .Build()
                    .WriteUserData(new BinaryUserDataSaveSystem());
                
                return;
            }

            new UserDataBuilder()
                .AddTwoFactor(new UserTwoFactorAuthenticationData(this.SecretKeyTextBox.Text))
                .Build()
                .WriteUserData(new BinaryUserDataSaveSystem());
        }
    }
}
