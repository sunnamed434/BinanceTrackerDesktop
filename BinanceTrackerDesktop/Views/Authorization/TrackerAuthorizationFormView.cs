﻿using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Forms.Authentication;
using BinanceTrackerDesktop.Models.User.Authorization;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Tracker.Forms;
using BinanceTrackerDesktop.User.Client;
using BinanceTrackerDesktop.User.Status.Detector;
using BinanceTrackerDesktop.Views.Authorization;
using BinanceTrackerDesktop.Views.Authorization.Exceptions;
using BinanceTrackerDesktop.Views.Authorization.Exceptions.ErrorCode;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Forms.Authorization;

public sealed partial class TrackerAuthorizationFormView : Form, IAuthorizationView
{
    private AuthorizationController controller;



    public TrackerAuthorizationFormView()
    {
        InitializeComponent();

        base.FormBorderStyle = FormBorderStyle.FixedSingle;
        base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        base.StartPosition = FormStartPosition.CenterScreen;
        base.Icon = ApplicationDirectories.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
        base.MaximizeBox = false;

        this.UserCurrenyTextBox.Text = "EUR";
        this.UserKeyTextBox.TextAlign = HorizontalAlignment.Center;
        this.UserSecretTextBox.TextAlign = HorizontalAlignment.Center;
        this.UserCurrenyTextBox.TextAlign = HorizontalAlignment.Center;

        this.AuthorizeButton.Click += onAuthorizeButtonClicked;
        this.AddAuthenticatorButton.Click += onAddAuthenticatorButtonClicked;
    }



    public void SetController(AuthorizationController controller)
    {
        this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }



    private void onAuthorizeButtonClicked(object sender, EventArgs e)
    {
        try
        {
            controller.Authorize(new UserAuthorizationModel(this.UserKeyTextBox.Text, this.UserSecretTextBox.Text, this.UserCurrenyTextBox.Text));
        }
        catch (AuthorizationException ex) when (ex.ErrorCode == AuthorizationErrorCode.Key)
        {
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Cannot to authorizate, please check your Key!")
                .BuildAsMessageBox();
            return;
        }
        catch (AuthorizationException ex) when (ex.ErrorCode == AuthorizationErrorCode.Secret)
        {
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Cannot to authorizate, please check your Secret!")
                .BuildAsMessageBox();
            return;
        }
        catch (AuthorizationException ex) when (ex.ErrorCode == AuthorizationErrorCode.Currency)
        {
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Cannot to authorizate, please check your Currency!")
                .BuildAsMessageBox();
            return;
        }

        base.Hide();

        TrackerFormView trackerFormView = new TrackerFormView();

        UserClient userClient = new UserClient();
        new TrackerController(trackerFormView, new UserStatusDetector(userClient.SaveDataSystem, userClient.Wallet).GetStatus());
        trackerFormView.ShowDialog();

        base.Close();

        this.AuthorizeButton.Click -= onAuthorizeButtonClicked;
        this.AddAuthenticatorButton.Click -= onAddAuthenticatorButtonClicked;
    }

    private void onAddAuthenticatorButtonClicked(object sender, EventArgs e)
    {
        AuthenticationFormView authenticationFormView = new AuthenticationFormView();
        new AuthenticationController(authenticationFormView);
        authenticationFormView.ShowDialog();
    }
}

public class BinanceAPIKeysCharactersLength
{
    public const int MaxLengthAPIKey = 64;

    public const int MaxLengthSecretKey = 64;
}
