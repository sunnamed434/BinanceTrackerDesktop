using BinanceTrackerDesktop.MVC.Controller;
using BinanceTrackerDesktop.MVC.View;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Wallet;
using BinanceTrackerDesktop.User.Wallet.Results;
using BinanceTrackerDesktop.Validators.String;

namespace BinanceTrackerDesktop.Controllers;

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
