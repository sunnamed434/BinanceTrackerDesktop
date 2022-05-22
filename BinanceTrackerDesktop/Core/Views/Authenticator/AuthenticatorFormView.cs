using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exceptions;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exceptions.ErrorCode;
using BinanceTrackerDesktop.Core.Controllers;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Models.Authentication.Validation;
using BinanceTrackerDesktop.Core.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Core.User.Authentication.System.Result;
using BinanceTrackerDesktop.Core.Views.Authenticator;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.Forms.Authentication
{
    public sealed partial class AuthenticatorFormView : Form, IAuthenticatorView
    {
        public event Action OnAuthenticationCompletedSuccessfully;

        public event Action OnAuthenticationCompletedFailed;

        private AuthenticatorController controller;



        public AuthenticatorFormView()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = ApplicationDirectories.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;

            this.CheckAuthenticationPINButton.Click += onCheckAuthenticationPINButtonClicked;
        }



        public void SetController(AuthenticatorController controller)
        {
            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }



        private void onCheckAuthenticationPINButtonClicked(object sender, EventArgs e)
        {
            ValidateResult result = ValidateResult.Failed;

            try
            {
                result = controller.Validate(new UserValidationModel(this.UserSecretTextBot.Text, this.UserPINTextBox.Text));
            }
            catch (TwoFactorAuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCode.Secret)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Cannot to authenticate, check your Secret Key please!")
                    .BuildAsMessageBox();

                OnAuthenticationCompletedFailed?.Invoke();
                return;
            }
            catch (TwoFactorAuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCode.PIN)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Cannot to authenticate, check your pin please!")
                    .BuildAsMessageBox();

                OnAuthenticationCompletedFailed?.Invoke();
                return;
            }

            if (result == ValidateResult.Failed)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Failed to authenticate!")
                    .BuildAsMessageBox();
                OnAuthenticationCompletedFailed?.Invoke();
                return;
            }

            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage(result.ToString())
                .BuildAsMessageBox();

            OnAuthenticationCompletedSuccessfully?.Invoke();
        }
    }
}
