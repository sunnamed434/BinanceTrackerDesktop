using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.DirectoryFiles.Control.Images;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Core.Themes.Detectors;
using BinanceTrackerDesktop.Core.Themes.Provider;
using BinanceTrackerDesktop.Core.Themes.Themable;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Theme.Repositories;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using BinanceTrackerDesktop.Core.Validators;
using BinanceTrackerDesktop.Core.Validators.String.Extension;
using System.Text;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.Settings
{
    public sealed partial class BinanceTrackerSettingsForm : Form
    {
        private readonly UserWallet userWallet;

        private readonly IThemable themable;



        public BinanceTrackerSettingsForm(UserWallet userWallet)
        {
            InitializeComponent();

            themable = this;
            ThemesProvider = new ThemesProvider(new ThemeDetector(new UserThemeRepository(new BinaryUserDataSaveSystem())));
            themable.ApplyTheme();

            base.Text = "Tracker Settings";
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Icon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(DirectoryImagesControl.RegisteredImages.ApplicationIcon).GetIcon();
            base.MaximizeBox = false;
            this.userWallet = userWallet ?? throw new ArgumentNullException(nameof(userWallet));

            this.ChangeCurrencyButton.Click += onChangeCurrencyButtonClicked;
        }



        public IThemesProvider ThemesProvider { get; }



        private async void onChangeCurrencyButtonClicked(object sender, EventArgs e)
        {
            IStringValidator userCurrencyValidator = this.NewCurrenyTextBox.Rules()
                .ContentNotNullOrEmpty();

            if (userCurrencyValidator.IsFailed)
            {
                new PopupBuilder()
                    .WithTitle(ApplicationEnviroment.GlobalName)
                    .WithMessage(new StringBuilder()
                                 .Append("[=(] Failed to change currency to ")
                                 .Append(NewCurrenyTextBox.Text)
                                 .ToString())
                    .WillCloseIn(90)
                    .ShowMessageBoxIfShouldOnBuild()
                    .Build();
                return;
            }

            BinaryUserDataSaveSystem userDataSaveSystem = new BinaryUserDataSaveSystem();
            UserData data = userDataSaveSystem.Read();
            data.Currency = NewCurrenyTextBox.Text;
            userDataSaveSystem.Write(data);

            UserWalletResult userWalletResult = await userWallet.GetTotalBalanceAsync();
            data.BestBalance = userWalletResult.Value;
            userDataSaveSystem.Write(data);

            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage(new StringBuilder()
                                 .Append("[=)] Successfully changed currency to ")
                                 .Append(data.Currency)
                                 .ToString())
                .WillCloseIn(90)
                .ShowMessageBoxIfShouldOnBuild()
                .Build();
        }
    }
}
