using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.Validators;
using BinanceTrackerDesktop.Core.Validators.String.Extension;
using BinanceTrackerDesktop.Core.Validators.String.Utility;
using BinanceTrackerDesktop.Tracker.Forms;
using System;
using System.Windows.Forms;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.DirectoryImagesControl;

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
            base.Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;

            this.AuthorizeButton.Click += onAuthorizeButtonClicked;
        }



        private void onAuthorizeButtonClicked(object sender, EventArgs e)
        {
            IStringValidator userKeyValidator = this.UserKeyTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            IStringValidator userSecretValidator = this.UserSecretTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            IStringValidator userCurrencyValidator = this.UserCurrenyTextBox.Rules()
                .ContentNotNullOrEmpty();

            if (StringValidatorUtility.IsAllSuccess(userKeyValidator, userSecretValidator, userCurrencyValidator))
            {
                this.AuthorizeButton.Click -= onAuthorizeButtonClicked;

                new UserData(this.UserKeyTextBox.Text, this.UserSecretTextBox.Text, this.UserCurrenyTextBox.Text).SaveUserData();

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
