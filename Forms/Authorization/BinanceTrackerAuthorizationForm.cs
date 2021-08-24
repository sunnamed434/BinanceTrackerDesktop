using BinanceTrackerDesktop.Core.Authorization;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Validation.Extension;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Authorization
{
    public partial class BinanceTrackerAuthorizationForm : Form
    {
        public BinanceTrackerAuthorizationForm()
        {
            InitializeComponent();

            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private async void onAuthorizeButtonClick(object sender, EventArgs e)
        {
            Validator userKeyValidator = UserKeyTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            Validator userSecretValidator = UserSecretTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            if (userKeyValidator.IsSuccess && userSecretValidator.IsSuccess)
            {
                await new BinanceUserDataWriter().WriteDataAsync(new BinanceUserData(UserKeyTextBox.Text, UserSecretTextBox.Text, decimal.Zero));

                base.Hide();
                new BinanceTrackerForm().ShowDialog();
            }
        }
    }
}
