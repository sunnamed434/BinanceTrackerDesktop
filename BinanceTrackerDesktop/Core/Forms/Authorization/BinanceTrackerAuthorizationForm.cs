using BinanceTrackerDesktop.Core.DirectoryFiles;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.Validator;
using BinanceTrackerDesktop.Core.Validator.String.Extension;
using BinanceTrackerDesktop.Core.Validator.String.Utility;
using BinanceTrackerDesktop.Tracker.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
using static BinanceTrackerDesktop.Core.DirectoryFiles.DirectoryImagesControl;

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
            base.Icon = (Icon)new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFileAt(RegisteredImages.ApplicationIcon).Result;
            base.MaximizeBox = false;

            this.AuthorizeButton.Click += onAuthorizeButtonClicked;
        }



        private void onAuthorizeButtonClicked(object sender, EventArgs e)
        {
            StringValidator userKeyValidator = this.UserKeyTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            StringValidator userSecretValidator = this.UserSecretTextBox.Rules()
               .ContentNotNullOrEmpty()
               .MinCharacters(BinanceAPIKeysCharactersLength.MaxLengthSecretKey);

            StringValidator userCurrencyValidator = this.UserCurrenyTextBox.Rules()
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
