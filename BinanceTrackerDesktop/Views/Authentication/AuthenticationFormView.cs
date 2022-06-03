using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Authentication.TwoFactor.Exceptions;
using BinanceTrackerDesktop.Authentication.TwoFactor.Exceptions.ErrorCode;
using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Models.User.Authentication;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.User.Authentication.Data;
using BinanceTrackerDesktop.User.Data.Builder;
using BinanceTrackerDesktop.User.Data.Extension;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.Views.Authentication;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Forms.Authentication;

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
                .BuildToMessageBox();
        }
        catch (TwoFactorAuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCode.Secret)
        {
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Cannot to authenticate, check your Secret Key please!")
                .BuildToMessageBox();
        }

        new PopupBuilder()
            .WithTitle(ApplicationEnviroment.GlobalName)
            .WithMessage("Successfully created QRCode and saved.")
            .BuildToMessageBox();

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