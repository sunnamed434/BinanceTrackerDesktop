using BinanceTrackerDesktop.Core.MVC.Controller;
using BinanceTrackerDesktop.Core.MVC.View;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Wallet;
using BinanceTrackerDesktop.Core.User.Wallet.Results;
using BinanceTrackerDesktop.Core.Validators;

namespace BinanceTrackerDesktop.Core.Controllers
{
    public sealed class SettingsController : Controller<SettingsController>
    {
        private readonly IView<SettingsController> view;

        private readonly UserWallet userWallet;



        public SettingsController(IView<SettingsController> view, UserWallet userWallet) : base(view)
        {
            this.view = view;
            this.userWallet = userWallet ?? throw new ArgumentNullException(nameof(userWallet));
        }



        public async void ChangeUserCurrency(string newCurrency)
        {
            new StringValidator(newCurrency)
                .ContentNotNullOrWhiteSpace()
                .ThrowIfFailed(new ArgumentException(newCurrency));

            BinaryUserDataSaveSystem userDataSaveSystem = new BinaryUserDataSaveSystem();
            UserData data = userDataSaveSystem.Read();
            data.Currency = newCurrency;
            userDataSaveSystem.Write(data);

            IUserWalletResult userWalletResult = await userWallet.GetTotalBalanceAsync();
            data.BestBalance = userWalletResult.Value;
            userDataSaveSystem.Write(data);
        }



        
        protected override SettingsController InitializeController()
        {
            return this;
        }
    }
}
