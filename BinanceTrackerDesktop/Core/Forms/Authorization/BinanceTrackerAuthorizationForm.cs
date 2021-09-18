using BinanceTrackerDesktop.Core.Authorization;
using BinanceTrackerDesktop.Core.DirectoryFiles.API;
using BinanceTrackerDesktop.Core.User.Data.API;
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
            base.Icon = new ApplicationDirectoryControl().Folders.Resources.Images.GetDirectoryFileAt(DirectoryIcons.ApplicationIcon).Icon;
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

                await new UserData(this.UserKeyTextBox.Text, this.UserSecretTextBox.Text).SaveUserDataAsync();

                base.Hide();
                new BinanceTrackerForm().ShowDialog();
                base.Close();
            }
        }
    }

    public class BinanceAPIKeysCharactersLength
    {
        public const int MaxLengthAPIKey = 64;

        public const int MaxLengthSecretKey = 64;
    }
}
