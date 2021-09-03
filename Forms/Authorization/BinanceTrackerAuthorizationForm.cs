using BinanceTrackerDesktop.Core.Authorization;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Validation.Extension;
using BinanceTrackerDesktop.Forms.Authorization.API;
using BinanceTrackerDesktop.Tracker.Forms;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Authorization
{
    public partial class BinanceTrackerAuthorizationForm : Form
    {
        public BinanceTrackerAuthorizationForm()
        {
            InitializeComponent();

            intitializeForm();

            this.AuthorizeButton.Click += onAuthorizeButtonClicked;
        }



        private void intitializeForm()
        {
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.MaximizeBox = false;
        }



        private async void onAuthorizeButtonClicked(object sender, EventArgs e)
        {
            Validator userKeyValidator = this.UserKeyTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            Validator userSecretValidator = this.UserSecretTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            if (userKeyValidator.IsSuccess && userSecretValidator.IsSuccess)
            {
                this.AuthorizeButton.Click -= onAuthorizeButtonClicked;

                await new BinanceUserDataWriter().WriteDataAsync(new BinanceUserData(this.UserKeyTextBox.Text, this.UserSecretTextBox.Text));

                base.Hide();
                new BinanceTrackerForm().ShowDialog();
                base.Close();
            }
        }
    }
}
