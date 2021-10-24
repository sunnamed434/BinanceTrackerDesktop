using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Notification.Builder;
using BinanceTrackerDesktop.Core.User.Authentication.Models;
using BinanceTrackerDesktop.Core.Validator.String.Exception;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Authentication
{
    public partial class AuthenticatorForm : Form
    {
        public AuthenticatorForm()
        {
            InitializeComponent();

            this.CheckAuthenticationPINButton.Click += onCheckAuthenticationPINButtonClicked;
        }



        private void onCheckAuthenticationPINButtonClicked(object sender, EventArgs e)
        {
            ValidateResult result = ValidateResult.Failed;

            try
            {
                result = new UserAuthenticatorSystem().ValidateTwoFactorPIN(this.UserSecretTextBot.Text, this.UserPINTextBox.Text);
            }
            catch (FailedStringValidationException)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Cannot to authenticate, check your Secret Key or PIN")
                    .WillCloseIn(90)
                    .Build(true);

                return;
            }

            new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage(result.ToString())
                    .WillCloseIn(90)
                    .Build(true);
        }
    }
}
