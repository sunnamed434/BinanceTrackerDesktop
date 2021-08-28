using BinanceTrackerDesktop.Core.Authorization;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Validation.Extension;
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

            AuthorizeButton.Click += onAuthorizeButtonClicked;
        }



        private void intitializeForm()
        {
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.MaximizeBox = false;
        }



        private async void onAuthorizeButtonClicked(object sender, EventArgs e)
        {
            Validator userKeyValidator = UserKeyTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            Validator userSecretValidator = UserSecretTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            if (userKeyValidator.IsSuccess && userSecretValidator.IsSuccess)
            {
                AuthorizeButton.Click -= onAuthorizeButtonClicked;

                await new BinanceUserDataWriter().WriteDataAsync(new BinanceUserData(UserKeyTextBox.Text, UserSecretTextBox.Text));

                base.Hide();
                new BinanceTrackerForm().ShowDialog();
            }
        }
    }
}
