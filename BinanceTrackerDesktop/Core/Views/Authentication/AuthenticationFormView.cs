using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exceptions;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exceptions.ErrorCode;
using BinanceTrackerDesktop.Core.Controllers;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Models.User.Authentication;
using BinanceTrackerDesktop.Core.MVC.View;
using BinanceTrackerDesktop.Core.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Core.User.Authentication.Data;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.Views.Authentication;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.Forms.Authentication
{
    public sealed partial class AuthenticationFormView : Form, IAuthenticationView
    {
        private AuthenticationController controller;

        private const int QRCodePictureBoxHiddenSizeValue = 0;



        public AuthenticationFormView()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = ApplicationDirectories.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;

            this.QRCodePictureBox.Size = new Size(QRCodePictureBoxHiddenSizeValue, QRCodePictureBoxHiddenSizeValue);
            this.GenerateQRCodeButton.Click += onGenerateQRCodeButtonClicked;
        }



        public void SetController(AuthenticationController controller)
        {
            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }



        private void onGenerateQRCodeButtonClicked(object sender, EventArgs e)
        {
            try
            {
                
                Image image = null;
                if ((image = controller.AuthenticateAndGenerateQRCodeImage(new UserAuthenticationModel(this.AccountTitleTextBox.Text, this.SecretKeyTextBox.Text))) != null)
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
                new UserDataBuilder(saveSystem.Read())
                    .AddTwoFactor(new UserTwoFactorAuthenticationData(this.SecretKeyTextBox.Text))
                    .Build()
                    .WriteUserData(saveSystem);
                return;
            }

            new UserDataBuilder()
                .AddTwoFactor(new UserTwoFactorAuthenticationData(this.SecretKeyTextBox.Text))
                .Build()
                .WriteUserData(saveSystem);
        }
    }
}
