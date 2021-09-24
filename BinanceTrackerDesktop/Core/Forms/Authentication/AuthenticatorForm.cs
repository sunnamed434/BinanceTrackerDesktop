using BinanceTrackerDesktop.Core.User.Authentication.API;
using BinanceTrackerDesktop.Core.Validation.API;
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
                MessageBox.Show("Cannot to authenticate, check your Secret Key or PIN");
                return;
            }

            MessageBox.Show(result.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
