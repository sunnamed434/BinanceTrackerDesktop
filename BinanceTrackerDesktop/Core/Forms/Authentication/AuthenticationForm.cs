using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Popup.Builder;
using BinanceTrackerDesktop.Core.Popup.Extension;
using BinanceTrackerDesktop.Core.User.Authentication.Models;
using BinanceTrackerDesktop.Core.Validator.String.Exception;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Authentication
{
    public partial class AuthenticationForm : Form
    {
        public AuthenticationForm()
        {
            InitializeComponent();

            this.GenerateQRCodeButton.Click += onGenerateQRCodeButtonClicked;
        }



        private void onGenerateQRCodeButtonClicked(object sender, EventArgs e)
        {
            try
            {
                QRCodePictureBox.Image = new UserAuthenticatorSystem().Authenticate(this.AccountTitleTextBox.Text, this.SecretKeyTextBox.Text);
            }
            catch (FailedStringValidationException)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage("Cannot to authenticate, check your Account Title or Secret Key")
                    .WillCloseIn(90)
                    .Build()
                    .Show(true);
            }
        }
    }
}
