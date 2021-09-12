using BinanceTrackerDesktop.Core.Authorization;
using BinanceTrackerDesktop.Core.Files.API;
using BinanceTrackerDesktop.Core.Forms.Authorization.API;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Validation.Extension;
using BinanceTrackerDesktop.Tracker.Forms;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Authorization
{
    public partial class BinanceTrackerAuthorizationForm : Form
    {
        public BinanceTrackerAuthorizationForm()
        {
            InitializeComponent();

            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = new ApplicationDirectoryControl().Directories.Images.GetDirectoryFileAt(DirectoryIcons.ApplicationIcon).Icon;
            base.MaximizeBox = false;

            this.AuthorizeButton.Click += onAuthorizeButtonClicked;
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
