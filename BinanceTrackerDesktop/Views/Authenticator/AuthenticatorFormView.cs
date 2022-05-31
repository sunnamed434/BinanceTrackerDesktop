using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Authentication.TwoFactor.Exceptions;
using BinanceTrackerDesktop.Authentication.TwoFactor.Exceptions.ErrorCode;
using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Models.User.Authentication.Validation;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.User.Authentication.System.Result;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Views.Authenticator;

public sealed partial class AuthenticatorFormView : Form, IAuthenticatorView
{
    public event Action OnAuthenticationCompletedSuccessfully;

    public event Action OnAuthenticationCompletedFailed;

    private AuthenticatorController controller;



    public AuthenticatorFormView()
    {
        InitializeComponent();

        FormBorderStyle = FormBorderStyle.FixedSingle;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        StartPosition = FormStartPosition.CenterScreen;
        Icon = ApplicationDirectories.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
        MaximizeBox = false;

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
