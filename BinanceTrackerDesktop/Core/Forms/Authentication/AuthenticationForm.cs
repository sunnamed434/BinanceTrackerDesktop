using BinanceTrackerDesktop.Core.User.Authentication.API;
using BinanceTrackerDesktop.Core.Validation.API;
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
                MessageBox.Show("Cannot to authenticate, check your Account Title or Secret Key");
            }
        }
    }
}
