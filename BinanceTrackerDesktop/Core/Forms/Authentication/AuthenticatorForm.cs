using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exception;
using BinanceTrackerDesktop.Core.Authentication.TwoFactor.Exception.ErrorCode;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Notification.Popup.Builder;
using BinanceTrackerDesktop.Core.User.Authentication.System;
using BinanceTrackerDesktop.Core.User.Authentication.System.Result;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Core.Forms.Authentication
{
    public partial class AuthenticatorForm : Form
    {
        public event Action OnAuthenticationCompletedSuccessfully;

        public event Action OnAuthenticationCompletedFailed;



        public AuthenticatorForm()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;

            this.CheckAuthenticationPINButton.Click += onCheckAuthenticationPINButtonClicked;
        }



        private void onCheckAuthenticationPINButtonClicked(object? sender, EventArgs e)
        {
            ValidateResult result = ValidateResult.Failed;

            try
            {
                result = new UserAuthenticatorSystem().ValidateTwoFactor(this.UserSecretTextBot.Text, this.UserPINTextBox.Text);
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
